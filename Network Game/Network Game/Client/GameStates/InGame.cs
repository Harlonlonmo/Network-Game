using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Lidgren.Network;
using Network_Game.Network;
using Network_Game.Client.Services;

namespace Network_Game.Client.GameStates
{
    class InGame : GameState
    {

        NetClient server;

        Dictionary<uint, ClientSprite> sprites;

        public InGame(Game game, NetClient server)
            : base(game)
        {
            this.server = server;
            ((InputController)Game.Services.GetService(typeof(InputController))).UpdateIO = true;

            sprites = new Dictionary<uint, ClientSprite>();
        }

        public override void Update(GameTime gameTime)
        {
            InputController input = (InputController)Game.Services.GetService(typeof(InputController));
            
                //
                // If there's input; send it to server
                //
                NetOutgoingMessage om = server.CreateMessage();
                InputParser.writeBytes(om, input.InputObject);
                server.SendMessage(om, NetDeliveryMethod.Unreliable);
            

            // read messages
            NetIncomingMessage msg;
            while ((msg = server.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryResponse:
                        // just connect to first server discovered
                        server.Connect(msg.SenderEndPoint);
                        break;
                    case NetIncomingMessageType.Data:
                        // server sent a position update
                        readData(msg);
                        break;
                }
            }
        }

        private void readData(NetIncomingMessage msg)
        {
            while (msg.PositionInBytes < msg.LengthBytes)
            {
                switch ((PacketTypes)msg.ReadByte())
                {
                    case PacketTypes.ClientSprite:
                        ClientSprite cs = new ClientSprite();
                        ClientSpriteParser.readBytes(msg, cs);
                        if (sprites.ContainsKey(cs.ID))
                        {
                            sprites[cs.ID] = cs;
                        }
                        else
                        {
                            sprites.Add(cs.ID, cs);
                        }
                        break;
                    default:
                        msg.ReadBytes(msg.ReadByte());
                        break;
                }
            }
        }

        /// <summary>
        /// Draws the graphics
        /// NOTE: expects spriteBatch to have begun
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            spriteBatch.Draw(((ContentLoader<Texture2D>)Game.Services.GetService(typeof(ContentLoader<Texture2D>))).get("Background"),
                new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height), Color.White);
            // draw all sprites
            foreach (ClientSprite cs in sprites.Values)
            {
                Texture2D texture = ((ContentLoader<Texture2D>)Game.Services.GetService(typeof(ContentLoader<Texture2D>))).get((ushort)cs.SpriteID+"");
                spriteBatch.Draw(texture, cs.Position, Color.White);
            }


        }

        public override void OnExiting(object sender, EventArgs args)
        {
            server.Shutdown("bye");
        }
    }
}
