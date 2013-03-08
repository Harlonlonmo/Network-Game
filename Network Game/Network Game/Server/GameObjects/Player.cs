using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Network_Game.Network;

namespace Network_Game.Server.GameObjects
{
    public class Player : GameObject
    {
        private float z = 0;
        private float dz = 0;

        public InputObject Input { get; set; }

        public Player(ServerObject game):base(game)
        {
            
            sprites.Add("ground", new ClientSprite(SpriteIdGenerator.getId(), new Vector2(0, 50), 0f, 1));
            sprites.Add("body", new ClientSprite(SpriteIdGenerator.getId(), Vector2.Zero, 0f, 0));
        }

        public override void Update(GameTime gameTime)
        {
            if (z == 0)
            {
                if (dz != 0)
                {
                    dz = 0;
                }
                if (Input.isButtonDown(InputObject.Button.A))
                {
                    dz = -10;
                }
            }
            if(z != 0 || dz != 0){
                z = Math.Min(z + dz, 0);
                dz += 1;
            }
            sprites["body"].RelativePosition = new Vector2(0, z);
            move(new Vector2(Input.HorizontalAxis, Input.VerticalAxis) * 10);
        }
    }
}
