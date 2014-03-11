using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/* Don't plagiarize me bro:
 * -Rotation source: http://www.dreamincode.net/forums/topic/83924-xna-user-controlled-object-rotation-2d/
 * -Speed up/brake source: http://www.dreamincode.net/forums/topic/101358-xna-30-game-tutorial-part-3/ 
*/

namespace Asteroids
{
    class Ship
    {
        private Texture2D sprite;
        private float accelerate = 0.4f;
        private float deccelerate = 0.2f;
        private Vector2 pos, vel, center;

        private float rot;

        private int screenWidth, screenHeight;

        public Ship(ContentManager content, int WIDTH, int HEIGHT)
        {
            sprite = content.Load<Texture2D>("shipDebug"); //medspeedster
            rot = 0.0f;
            pos = new Vector2((WIDTH - sprite.Width) / 2, (HEIGHT - sprite.Height) / 2);
            vel = new Vector2(0, 0);
            center = new Vector2(sprite.Width / 2, sprite.Height / 2);

            screenWidth = WIDTH;
            screenHeight = HEIGHT;
        }

        public void SpeedUp()
        {
            vel.X = vel.X + (float)(Math.Cos(rot - Math.PI / 2) * accelerate);
            vel.Y = vel.Y + (float)(Math.Sin(rot - Math.PI / 2) * accelerate);
	 
    	    if (vel.X > 6.0f)
    	    {
    	        vel.X = 6.0f;
    	    }

    	    if (vel.X < -6.0f)
    	    {
    	        vel.X = -6.0f;
    	    }

    	    if (vel.Y > 6.0f)
    	    {
    	        vel.Y = 6.0f;
    	    }

    	    if (vel.Y < -6.0f)
    	    {
    	        vel.Y = -6.0f;
    	    }
        }

        public void SpeedDown()
        {
            if (vel.X < 0)
            {
                vel = new Vector2(vel.X + deccelerate, vel.Y);
            }

            if (vel.X > 0)
            {
                vel = new Vector2(vel.X - deccelerate, vel.Y);
            }

            if (vel.Y < 0)
            {
                vel = new Vector2(vel.X, vel.Y + deccelerate);
            }

            if (vel.Y > 0)
            {
                vel = new Vector2(vel.X, vel.Y - deccelerate);
            }
        }

        public void Update(Edges edges)
        {
            KeyboardState keyboardState;
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                rot += 0.05f;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                rot -= 0.05f;
            }         
            
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                SpeedUp();
            }

            if (keyboardState.IsKeyUp(Keys.Up))
            {
                SpeedDown();
            }

            pos.X += vel.X;
            pos.Y += vel.Y;

            pos = edges.travelEdges(sprite, pos, screenWidth, screenHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, pos, null, Color.White, rot, center, 1.0f, SpriteEffects.None, 0);
        }
    }
}
