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
        public Texture2D bulletSprite;

        public Vector2 bulletPos, bulletVel, bulletPointing;

        private float rot;
        private float speed;

        public Bullet(ContentManager content, Vector2 startingPosition, Vector2 movement, float angle)
        {
            bulletSprite = content.Load<Texture2D>("bulletTest");

            bulletPos = startingPosition;
            bulletPointing = movement;

            rot = angle;
            speed = 4.0f;
        }

        public void Update(GameTime gt)
        {
            bulletPos += bulletPointing * speed * (float)gt.ElapsedGameTime.TotalMilliseconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bulletSprite, bulletPos, null, Color.White, rot, bulletPointing, 1f, SpriteEffects.None, 1);
        }
    }
}
