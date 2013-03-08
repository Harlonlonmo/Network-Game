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

        private SpriteIDs[] anim1;
        private float animTimer;
        private int animFrame;

        public InputObject Input { get; set; }

        public Player(ServerObject game):base(game)
        {
            anim1 = new SpriteIDs[3]{
                SpriteIDs.Player1_anim1_frame1,
                SpriteIDs.Player1_anim1_frame2,
                SpriteIDs.Player1_anim1_frame3
            };
            sprites.Add("ground", new ClientSprite(SpriteIdGenerator.getId(), new Vector2(0, 50), 0f, SpriteIDs.platform1));
            sprites.Add("body", new ClientSprite(SpriteIdGenerator.getId(), Vector2.Zero, 0f, SpriteIDs.Player1_anim1_frame1));
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


            float avstandTilSpiller = 50;
            Vector2 siktePosisjon = new Vector2((float)Math.Cos(Input.AmingAngle), (float)Math.Sin(Input.AmingAngle)) * avstandTilSpiller + Position;


            animTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (animTimer > 0.25f)
            {
                animFrame++;
                animFrame %= anim1.Length;
                animTimer -= 0.25f;
            }
            sprites["body"].SpriteID = anim1[animFrame];
        }
    }
}
