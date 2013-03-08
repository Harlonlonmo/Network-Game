using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Network_Game.Client.Services
{
    public class ContentLoader<T>
    {
        public Game Game { get; protected set; }
        
        private Dictionary<string, T> content;

        public ContentLoader(Game game)
        {
            Game = game;
            content = new Dictionary<string, T>();
        }

        public T get(String key)
        {
            return content[key];
        }

        public void load(String key)
        {
            content.Add(key, Game.Content.Load<T>(key));
        }
    }
}
