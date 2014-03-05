using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    class Ship
    {
        public Texture2D sprite;

        public Vector2 pos;
        public Vector2 vec;

        int screenWidth, screenHeight;

        public Ship(ContentManager content, int WIDTH, int HEIGHT)
        {
            sprite = content.Load<Texture2D>("medspeedster");

            pos = new Vector2((WIDTH - sprite.Width) / 2, (HEIGHT - sprite.Height) / 2);
            vec = new Vector2(0, 0);

            screenWidth = WIDTH;
            screenHeight = HEIGHT;
        }

        public void Update()
        {
            KeyboardState keyboardState;
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                vec.X += 0.1f;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                vec.X -= 0.1f;
            }

            pos.X += vec.X;

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                vec.Y -= 0.1f;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                vec.Y += 0.1f;
            }

            pos.Y += vec.Y;

            //what happens when the ship goes to the left edge
            if (pos.X + sprite.Width < 0)
            {
                pos.X = screenWidth;
            }

            //what happens when the ship goes to the right edge
            if (pos.X > screenWidth)
            {
                pos.X = 0 - sprite.Width;
            }

            //what happens when the ship goes to the top edge
            if (pos.Y + sprite.Height < 0)
            {
                pos.Y = screenHeight;
            }

            //what happens when the ship goes to the bottom edge
            if (pos.Y > screenHeight)
            {
                pos.Y = 0 - sprite.Height;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, pos, Color.White);
        }
    }
}
