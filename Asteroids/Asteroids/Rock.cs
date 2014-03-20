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
        private Vector2 center;

        private float rot;

        private bool rotRight;

        private int xVel, yVel, screenWidth, screenHeight;

        public Texture2D rockSprite;

        public Vector2 pos;

        public bool hitWithBullet;

        public int timer;

        public Rock(ContentManager content, int WIDTH, int HEIGHT, Random random)
        {
            // rockSprite = content.Load<Texture2D>("meteorSmall");
            rockSprite = content.Load<Texture2D>("donut");

            do
            {
                pos = new Vector2(random.Next(0, WIDTH), random.Next(0, HEIGHT));
            }
            while (pos.X > WIDTH / 10 && pos.X < WIDTH * 9 && pos.Y > HEIGHT / 10 && pos.Y < HEIGHT * 9);

            center = new Vector2(rockSprite.Width / 2, rockSprite.Height / 2);

            if (random.Next(0, 2) == 0)
            {
                rotRight = true;
            }
            else
            {
                rotRight = false;
            }

            xVel = random.Next(-2, 2);
            yVel = random.Next(-2, 2);
            
            screenWidth = WIDTH;
            screenHeight = HEIGHT;

            hitWithBullet = false;

            timer = 10;
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

            pos = edges.travelEdges(rockSprite, pos, screenWidth, screenHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(rockSprite, pos, null, Color.White, rot, center, 1.0f, SpriteEffects.None, 0);
        }
    }
}
