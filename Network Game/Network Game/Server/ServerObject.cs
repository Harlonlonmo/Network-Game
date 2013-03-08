using System;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Network_Game.Network;
using System.Collections.Generic;
using Network_Game.Server.GameObjects;
using Network_Game.Server.ServerStates;

namespace Network_Game.Server
{
    public class ServerObject : GameComponent
    {
        public bool Running { get; set; }

        NetServer server;

        //ServerState state;

        Dictionary<long, Player> Players;

        public ServerObject(Game game)
            : base(game)
        {
            Players = new Dictionary<long, Player>();

            game.Exiting += UnloadContent;
            NetPeerConfiguration config = new NetPeerConfiguration("xnagame");
            config.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            config.EnableMessageType(NetIncomingMessageType.Data);
            config.Port = 1337;

            // create and start server
            server = new NetServer(config);
            server.Start();

            //state = new ServerGame(this);

        }

        public override void Update(GameTime gameTime)
        {
            readFromNettwork();
            sendToNetwork();

            foreach (Player obj in Players.Values)
            {
                obj.Update(gameTime);
            }
            //state.Update(gameTime);

            base.Update(gameTime);
        }

        public void readFromNettwork()
        {
            NetIncomingMessage msg;
            while ((msg = server.ReadMessage()) != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(msg.MessageType);
                Console.ResetColor();
                /*foreach (byte b in msg.Data)
                {
                    Console.Write(b + ", ");
                }
                Console.WriteLine();*/
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryRequest:
                        //
                        // Server received a discovery request from a client; send a discovery response (with no extra data attached)
                        //
                        Console.WriteLine(msg.ReadString());
                        server.SendDiscoveryResponse(null, msg.SenderEndPoint);
                        break;
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                        Console.WriteLine(msg.ReadString());
                        break;
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        //
                        // Just print diagnostic messages to console
                        //
                        Console.WriteLine(msg.ReadString());
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus status = (NetConnectionStatus)msg.ReadByte();
                        if (status == NetConnectionStatus.Connected)
                        {
                            //
                            // A new player just connected!
                            //
                            Console.WriteLine(NetUtility.ToHexString(msg.SenderConnection.RemoteUniqueIdentifier) + " connected!");

                            Players.Add(msg.SenderConnection.RemoteUniqueIdentifier, new Player(this));
                        }

                        break;
                    case NetIncomingMessageType.Data:
                        readData(msg);

                        break;
                }
            }
        }

        public void readData(NetIncomingMessage msg)
        {
            while (msg.PositionInBytes < msg.LengthBytes)
            {
                Console.WriteLine("Data in buffer : " + (msg.LengthBytes - msg.PositionInBytes));
                PacketTypes pa = (PacketTypes)msg.ReadByte();

                Console.WriteLine(pa);
                switch (pa)
                {
                    case PacketTypes.ClientInput:

                        InputObject input = new InputObject();
                        InputParser.readBytes(msg, ref input);

                        Player p = Players[msg.SenderConnection.RemoteUniqueIdentifier];
                        p.Input = input;

                        break;
                    default:
                        Console.WriteLine("overflow : " + msg.ReadByte());
                        break;
                }
            }
        }

        private void sendToNetwork()
        {
            // Yes, it's time to send position updates

            // for each player...
            foreach (NetConnection player in server.Connections)
            {
                NetOutgoingMessage om = server.CreateMessage();
                foreach (GameObject obj in Players.Values)
                {
                    foreach (ClientSprite sprite in obj.Sprites)
                    {
                        ClientSpriteParser.writeBytes(om, sprite);
                    }
                }

                server.SendMessage(om, player, NetDeliveryMethod.Unreliable);
                /*
                // ... send information about every other player (actually including self)
                foreach (NetConnection otherPlayer in server.Connections)
                {
                    // send position update about 'otherPlayer' to 'player'
                    NetOutgoingMessage om = server.CreateMessage();

                    // write who this position is for
                    om.Write(otherPlayer.RemoteUniqueIdentifier);

                    if (otherPlayer.Tag == null)
                        otherPlayer.Tag = new int[2];

                    int[] pos = otherPlayer.Tag as int[];
                    om.Write(pos[0]);
                    om.Write(pos[1]);

                    // send message
                    server.SendMessage(om, player, NetDeliveryMethod.Unreliable);
                }*/
            }
        }

        public void UnloadContent(object sender, EventArgs e)
        {
            server.Shutdown("app exiting");
        }
    }
}
