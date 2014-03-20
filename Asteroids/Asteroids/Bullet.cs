using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    class Bullet
    {
        private Vector2 bulletPointing;
        private float rot, speed;
        public Vector2 pos, vel;
        public Texture2D bulletSprite;

        public Bullet(ContentManager content, Vector2 startingPosition, Vector2 movement, float angle)
        {
            // bulletSprite = content.Load<Texture2D>("bulletTest");
            bulletSprite = content.Load<Texture2D>("bullet");

            pos = startingPosition;
            bulletPointing = movement;

            rot = angle - (float)MathHelper.PiOver2;
            speed = 1.0f;
        }

        public void Update(GameTime gt)
        {
            //pos += bulletPointing * speed * (float)gt.ElapsedGameTime.TotalMilliseconds;
            pos += new Vector2((float)Math.Cos(rot) * speed, (float)Math.Sin(rot) * speed);
        }

        public Boolean bulletRemove(int WIDTH, int HEIGHT)
        {
            if (pos.X + bulletSprite.Width / 2 < 0 || pos.X - bulletSprite.Width / 2 > WIDTH || pos.Y + bulletSprite.Height / 2 < 0 || pos.Y - bulletSprite.Height / 2 > HEIGHT)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bulletSprite, pos, null, Color.White, rot, bulletPointing, 1f, SpriteEffects.None, 1);
        }
    }
}
