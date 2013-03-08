using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Network_Game.Network;

namespace Network_Game.Server
{
    public abstract class GameObject
    {
        public Vector2 Position
        {
            get { return position; }
            protected set
            {
                position = value;
                foreach (ClientSprite cs in sprites.Values)
                {
                    cs.Position = cs.RelativePosition + position;
                }
            }
        }

        public ServerObject Game { get; protected set; }

        private Vector2 position;

        public ClientSprite[] Sprites { get { return sprites.Values.ToArray(); } }
        protected Dictionary<String, ClientSprite> sprites;

        public GameObject(ServerObject game)
        {
            Game = game;
            sprites = new Dictionary<String, ClientSprite>();
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public void move(Vector2 delta)
        {
            Position += delta;
        }

    }
}
