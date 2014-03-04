using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroids
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D shipSprite;
        private Vector2 shipPos = new Vector2();
        private Vector2 shipV = new Vector2();
        private int WIDTH, HEIGHT;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            WIDTH = GraphicsDevice.Viewport.Width;
            HEIGHT = GraphicsDevice.Viewport.Height;

            shipV = new Vector2(0, 0);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            shipSprite = Content.Load<Texture2D>("medspeedster");
            shipPos = new Vector2((WIDTH - shipSprite.Width) / 2, (HEIGHT - shipSprite.Height) / 2);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            

            KeyboardState keyboardState;
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                shipV.X += 0.1f;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                shipV.X -= 0.1f;
            }
            shipPos.X += shipV.X;

            


            if (keyboardState.IsKeyDown(Keys.Up))
            {
                shipV.Y -= 0.1f;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                shipV.Y += 0.1f;
            }
            shipPos.Y += shipV.Y;

            //what happens when the ship goes to the left edge
            if (shipPos.X + shipSprite.Width < 0)
            {
                shipPos.X = WIDTH;
            }

            //what happens when the ship goes to the right edge
            if (shipPos.X > WIDTH)
            {
                shipPos.X = 0 - shipSprite.Width;
            }

            //what happens when the ship goes to the top edge
            if (shipPos.Y + shipSprite.Height < 0)
            {
                shipPos.Y = HEIGHT;
            }

            //what happens when the ship goes to the bottom edge
            if (shipPos.Y > HEIGHT)
            {
                shipPos.Y = 0 - shipSprite.Height;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(shipSprite, shipPos, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
