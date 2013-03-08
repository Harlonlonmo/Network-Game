using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Lidgren.Network;
using System;
using Network_Game.Client.Navigation;
using Network_Game.Client.Services;
using Network_Game.Server;

namespace Network_Game.Client.GameStates
{
    public class MultiplayerMenu : GameState
    {

        private bool updating;

        private GameMenu menu;

        public MultiplayerMenu(Game1 game)
            : base(game)
        {
            SpriteFont buttonFont = ((ContentLoader<SpriteFont>)Game.Services.GetService(typeof(ContentLoader<SpriteFont>))).get("ButtonFont");
            menu = new GameMenu(Game, GameMenu.Direction.Vertical);
            menu.Buttons.Add(new Button(
                new Rectangle(20, 20, 200, 50), 
                Game, Color.Green, Color.LawnGreen, 
                Button.TEXT_ALIGN_MID, "Search", 
                buttonFont
                ));
            menu.Buttons.Add(new Button(
                new Rectangle(20, 90, 200, 50),
                Game, Color.Green, Color.LawnGreen,
                Button.TEXT_ALIGN_MID, "Create",
                buttonFont
                ));
            menu.Buttons.Add(new Button(
                new Rectangle(20, 160, 200, 50),
                Game, Color.Green, Color.LawnGreen,
                Button.TEXT_ALIGN_MID, "Back",
                buttonFont
                ));
            updating = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (updating)
            {
                menu.Update(gameTime);
                switch (menu.getPressed())
                {
                    case 0:
                        searchForServers();
                        break;
                    case 1:
                        CreateServer();
                        joinServer("127.0.0.1");
                        break;
                    case 2:
                        ((Game1)Game).changeState(new Menu((Game1)Game));
                        break;
                }
                /*NetIncomingMessage msg;
                while ((msg = server.ReadMessage()) != null)
                {
                }*/
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(((ContentLoader<Texture2D>)Game.Services.GetService(typeof(ContentLoader<Texture2D>))).get("Background"),
                new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height), Color.White);
            menu.Draw(spriteBatch, gameTime);
        }

        private void searchForServers()
        {
            IPRequestForm ipRequest = new IPRequestForm();
            ipRequest.ShowInTaskbar = false;
            ipRequest.Show();

            ipRequest.FormClosed += resume;
            ipRequest.connect += joinServer;
            updating = false;
        }

        private void resume(object sender, EventArgs e)
        {
            updating = true;
        }

        private void joinServer(String ip)
        {
            try
            {
                NetPeerConfiguration config = new NetPeerConfiguration("xnagame");
                config.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);


                NetClient Client = new NetClient(config);
                Client.Start();

                Client.Connect(ip, 1337);
                ((Game1)Game).changeState(new Lobby((Game1)Game, Client));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void CreateServer()
        {
            Game.Components.Add(new ServerObject(Game));
        }
    }
}
