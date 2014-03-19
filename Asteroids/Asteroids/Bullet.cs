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
        float rot;
        private ContentManager Content;
        private Vector2 vector21;
        private Vector2 vector22;
        private float Speed = 4.0f;

        public Bullet(ContentManager content, Vector2 startingPosition, Vector2 movement, float angle)
        {
            bulletSprite = content.Load<Texture2D>("bulletTest");
            bulletPos = startingPosition;
            bulletPointing = movement;
            rot = angle;
        }

        public void Update(GameTime gt)
        {
            bulletPos += bulletPointing * Speed * (float)gt.ElapsedGameTime.TotalMilliseconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bulletSprite, bulletPos, null, Color.White, rot, bulletPointing, 1f, SpriteEffects.None, 1);
        }
    }
}
