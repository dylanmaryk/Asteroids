﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    class Bullets
    {
        public Texture2D bulletSprite;
        public Vector2 bulletPos;
        public Vector2 bulletVel;
        public Vector2 bulletPointing;
        
        public Bullets(Texture2D sprite)
        {
            bulletSprite = sprite;
        }




        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bulletSprite, bulletPos, null, Color.White, 0f, bulletPointing, 1f, SpriteEffects.None, 1);
        }

    }
}
