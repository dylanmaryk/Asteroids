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
        // Ship stuff

        private Texture2D shipDrawSprite;
        private Texture2D shipSprite;
        private Texture2D shipBoostSprite;

        private float accelerate = 0.4f;
        private float deccelerate = 0.1f;

        public Vector2 shipPos, shipVel, center;

        public float rot;

        // Bullet stuff

        private List<Bullet> bullets = new List<Bullet>();
        
        private int screenWidth, screenHeight;

        public Ship(ContentManager content, int WIDTH, int HEIGHT)
        {
            shipSprite = content.Load<Texture2D>("player");
            shipDrawSprite = content.Load<Texture2D>("player");
            shipBoostSprite = content.Load<Texture2D>("playerBoost");
            rot = 0.0f;

            shipPos = new Vector2((WIDTH - shipSprite.Width) / 2, (HEIGHT - shipSprite.Height) / 2);
            shipVel = new Vector2(0, 0);
            center = new Vector2(shipSprite.Width / 2, shipSprite.Height / 2);

            screenWidth = WIDTH;
            screenHeight = HEIGHT;
        }

        public void Accelerate()
        {
            shipVel.X = shipVel.X + (float)(Math.Cos(rot - Math.PI / 2) * accelerate);
            shipVel.Y = shipVel.Y + (float)(Math.Sin(rot - Math.PI / 2) * accelerate);
	 
    	    if (shipVel.X > 6.0f)
    	    {
    	        shipVel.X = 6.0f;
    	    }

    	    if (shipVel.X < -6.0f)
    	    {
    	        shipVel.X = -6.0f;
    	    }

    	    if (shipVel.Y > 6.0f)
    	    {
    	        shipVel.Y = 6.0f;
    	    }

    	    if (shipVel.Y < -6.0f)
    	    {
    	        shipVel.Y = -6.0f;
    	    }
        }

        public void Decelerate()
        {
            if (shipVel.X < 0)
            {
                shipVel.X = shipVel.X + deccelerate;
            }

            if (shipVel.X > 0)
            {
                shipVel.X = shipVel.X - deccelerate;
            }

            if (shipVel.Y < 0)
            {
                shipVel.Y = shipVel.Y + deccelerate;
            }

            if (shipVel.Y > 0)
            {
                shipVel.Y = shipVel.Y - deccelerate;
            }
        }

        public void Update(Edges edges)
        {
            KeyboardState newState, oldState;
            newState = Keyboard.GetState();

            shipPos.X += shipVel.X;
            shipPos.Y += shipVel.Y;
            shipPos = edges.travelEdges(shipSprite, shipPos, screenWidth, screenHeight);

            if (newState.IsKeyDown(Keys.Right))
            {
                rot += 0.03f;
            }

            if (newState.IsKeyDown(Keys.Left))
            {
                rot -= 0.03f;
            }         
            
            if (newState.IsKeyDown(Keys.Up))
            {
                shipDrawSprite = shipBoostSprite;
                Accelerate();
            }

            if (newState.IsKeyUp(Keys.Up))
            {
                shipDrawSprite = shipSprite;
                Decelerate();
            }

            // Pause to check variables
            if (newState.IsKeyDown(Keys.S))
            {
                int argh = 0;
            }

            oldState = newState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(shipDrawSprite, shipPos, null, Color.White, rot, center, 1.0f, SpriteEffects.None, 0);
        }
    }
}
