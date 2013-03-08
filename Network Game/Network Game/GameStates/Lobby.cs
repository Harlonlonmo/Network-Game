using Network_Game.Services;
using Nettwork_Game.Navigation;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Lidgren.Network;

namespace Network_Game.GameStates
{
    public class Lobby : GameState
    {

        NetClient Client;

        public Lobby(Game1 game, NetClient Client)
            : base(game)
        {
            this.Client = Client;
            SpriteFont buttonFont = ((ContentLoader<SpriteFont>)Game.Services.GetService(typeof(ContentLoader<SpriteFont>))).get("ButtonFont");
            menu = new GameMenu(Game, GameMenu.Direction.Vertical);
            menu.Buttons.Add(new Button(
                new Rectangle(20, 20, 200, 50),
                Game, Color.Green, Color.LawnGreen,
                Button.TEXT_ALIGN_MID, "Play",
                buttonFont
                ));
            menu.Buttons.Add(new Button(
                new Rectangle(20, 90, 200, 50),
                Game, Color.Green, Color.LawnGreen,
                Button.TEXT_ALIGN_MID, "Exit",
                buttonFont
                ));

        }

        public override void Update(GameTime gameTime)
        {
            menu.Update(gameTime);
            switch (menu.getPressed())
            {
                case 0:
                    ((Game1)Game).changeState(new InGame((Game1)Game, Client));
                    break;
                case 1:
                    ((Game1)Game).changeState(new Menu((Game1)Game));
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(((ContentLoader<Texture2D>)Game.Services.GetService(typeof(ContentLoader<Texture2D>))).get("Background"),
                new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height), Color.White);
            menu.Draw(spriteBatch, gameTime);
        }

        public GameMenu menu { get; set; }
    }
}
