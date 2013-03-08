using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Network_Game.GameStates;
using Network_Game.Services;
using Nettwork_Game.Services;
using Microsoft.Xna.Framework.Audio;

namespace Network_Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ContentLoader<SpriteFont> fontLoader;
        ContentLoader<Texture2D> imageLoader;
        ContentLoader<SoundEffectInstance> soundLoader;

        GameState gameState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            InputController input = new InputController(this);
            Components.Add(input);
            Services.AddService(typeof(InputController), input);

            fontLoader = new ContentLoader<SpriteFont>(this);
            Services.AddService(typeof(ContentLoader<SpriteFont>), fontLoader);

            imageLoader = new ContentLoader<Texture2D>(this);
            Services.AddService(typeof(ContentLoader<Texture2D>), imageLoader);

            soundLoader = new ContentLoader<SoundEffectInstance>(this);
            Services.AddService(typeof(ContentLoader<SoundEffectInstance>), soundLoader);
        }

        protected override void Initialize()
        {
            base.Initialize();
            gameState = new Menu(this);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            imageLoader.load("Background");
            fontLoader.load("ButtonFont");

            imageLoader.load("0");
            imageLoader.load("1");
            
        }

        protected override void Update(GameTime gameTime)
        {
            gameState.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            gameState.Draw(spriteBatch, gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            gameState.OnExiting(sender, args);

            base.OnExiting(sender, args);
        }

        public void changeState(GameState newState)
        {
            gameState = newState;
        }
    }
}
