using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Network_Game
{
    public class ClientSprite
    {
        public uint ID { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public ushort SpriteID { get; set; }
        public Vector2 RelativePosition { get; set; }

        public ClientSprite()
        {
        }

        public ClientSprite(uint ID, Vector2 RelativePosition, float Rotation, ushort SpriteID)
        {
            this.ID = ID;
            this.RelativePosition = RelativePosition;
            this.Rotation = Rotation;
            this.SpriteID = SpriteID;
        }
    }
}
