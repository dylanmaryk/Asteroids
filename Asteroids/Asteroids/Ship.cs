using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private Texture2D shipDrawSprite, shipBoostSprite;

        private float accelerate = 0.06f;
        private float deccelerate = 0.03f;

        public Vector2 center, barrel;

        public Texture2D shipSprite;

        public float rot;

        public Vector2 pos, vel;
        
        private int screenWidth, screenHeight;

        private SoundEffect rocketSound;

        private SoundEffectInstance instanceRocket;

        public Ship(ContentManager content, int WIDTH, int HEIGHT)
        {
            // shipSprite = content.Load<Texture2D>("player");
            // shipDrawSprite = ShipSprite;
            // shipBoostSprite = content.Load<Texture2D>("playerBoost");
            shipSprite = content.Load<Texture2D>("gun");
            shipDrawSprite = shipSprite;
            shipBoostSprite = content.Load<Texture2D>("gunMoving");

            rot = 0.0f;

            pos = new Vector2((WIDTH - shipSprite.Width) / 2, (HEIGHT - shipSprite.Height) / 2);
            vel = new Vector2(0, 0);

            center = new Vector2(shipSprite.Width / 2, shipSprite.Height / 2);
            barrel = new Vector2(10 , 10);

            screenWidth = WIDTH;
            screenHeight = HEIGHT;

            rocketSound = content.Load<SoundEffect>("rocket");

            instanceRocket = rocketSound.CreateInstance();
            instanceRocket.Volume = 0.30f;
        }

        public void Accelerate()
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

            instanceRocket.Play();
        }

        public void Decelerate()
        {
            if (vel.X < 0)
            {
                vel.X = vel.X + deccelerate;
            }

            if (vel.X > 0)
            {
                vel.X = vel.X - deccelerate;
            }

            if (vel.Y < 0)
            {
                vel.Y = vel.Y + deccelerate;
            }

            if (vel.Y > 0)
            {
                vel.Y = vel.Y - deccelerate;
            }

            instanceRocket.Pause();
        }

        public void Update(Edges edges)
        {
            KeyboardState newState, oldState;
            newState = Keyboard.GetState();

            pos.X += vel.X;
            pos.Y += vel.Y;
            pos = edges.travelEdges(shipSprite, pos, screenWidth, screenHeight);

            if (newState.IsKeyDown(Keys.Right))
            {
                rot += 0.1f;
            }

            if (newState.IsKeyDown(Keys.Left))
            {
                rot -= 0.1f;
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

            oldState = newState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(shipDrawSprite, pos, null, Color.White, rot, center, 1.0f, SpriteEffects.None, 0);
        }
    }
}
