using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    class Rock
    {
        private Texture2D sprite;

        private Vector2 pos, center;

        private float rot;

        private bool rotRight;

        private int xVel, yVel, screenWidth, screenHeight;

        public Rock(ContentManager content, int WIDTH, int HEIGHT, Random random)
        {
            sprite = content.Load<Texture2D>("asteroidBig01");

            pos = new Vector2(random.Next(0, WIDTH), random.Next(0, HEIGHT));
            center = new Vector2(sprite.Width / 2, sprite.Height / 2);

            if (random.Next(0, 2) == 0)
            {
                rotRight = true;
            }
            else
            {
                rotRight = false;
            }

            xVel = random.Next(-1, 1);
            yVel = random.Next(-1, 1);

            screenWidth = WIDTH;
            screenHeight = HEIGHT;
        }

        public void Update(Edges edges)
        {
            pos.X += xVel;
            pos.Y += yVel;

            if (rotRight)
            {
                rot += 0.1f;
            }
            else
            {
                rot -= 0.1f;
            }

            pos = edges.travelEdges(sprite, pos, screenWidth, screenHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, pos, null, Color.White, rot, center, 1.0f, SpriteEffects.None, 0);
        }
    }
}
