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
 * -Speed up/ brake source: http://www.dreamincode.net/forums/topic/101358-xna-30-game-tutorial-part-3/ 
*/

namespace Asteroids
{
    class Ship
    {
        public Texture2D sprite;
        public Vector2 pos;
        public Vector2 vel;
        public Vector2 center;
        public float rot;

        int screenWidth, screenHeight;

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
            vel += new Vector2(
            (float)(Math.Cos(rot - Math.PI / 2) * 0.1f),
            (float)((Math.Sin(rot - Math.PI / 2) * 0.1f)));
	 
    	    if (vel.X > 9.0f)
    	    {
    	        vel.X = 9.0f;
    	    }
    	    if (vel.X < -9.0f)
    	    {
    	        vel.X = -9.0f;
    	    }
    	    if (vel.Y > 9.0f)
    	    {
    	        vel.Y = 9.0f;
    	    }
    	    if (vel.Y < -9.0f)
    	    {
    	        vel.Y = -9.0f;
    	    }
        }

        public void SpeedDown()
        {
            if (vel.X < 0)
            {
                vel = new Vector2(
                vel.X + 0.02f, vel.Y);
            }

            if (vel.X > 0)
            {
                vel = new Vector2(
                vel.X - 0.02f, vel.Y);
            }

            if (vel.Y < 0)
            {
                vel = new Vector2(
                vel.X, vel.Y + 0.02f);
            }

            if (vel.Y > 0)
            {
                vel = new Vector2(
                vel.X, vel.Y - 0.02f);
            }
        }

        public void Update()
        {
            KeyboardState keyboardState;
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                rot += 0.1f;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                rot -= 0.1f;
            }
            pos.X += vel.X;
            
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                //vel.Y -= 0.1f;
                SpeedUp();
            }
            if (!keyboardState.IsKeyDown(Keys.Up))
            {
                SpeedDown();
            }
            pos.Y += vel.Y;

            //what happens when the ship goes to the left edge
            if (pos.X + sprite.Width / 2 < 0)
            {
                pos.X = screenWidth + sprite.Width / 2;
            }

            //what happens when the ship goes to the right edge
            if (pos.X - sprite.Width / 2 > screenWidth)
            {
                pos.X = 0 - sprite.Width / 2;
            }

            //what happens when the ship goes to the top edge
            if (pos.Y + sprite.Height / 2 < 0)
            {
                pos.Y = screenHeight + sprite.Height / 2;
            }

            //what happens when the ship goes to the bottom edge
            if (pos.Y - sprite.Height / 2 > screenHeight)
            {
                pos.Y = 0 - sprite.Height / 2;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, pos, null, Color.White, rot, center, 1.0f, SpriteEffects.None, 0);
        }
    }
}
