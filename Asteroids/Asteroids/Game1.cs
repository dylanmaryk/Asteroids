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

        private int WIDTH, HEIGHT, rockCount;

        private Ship ship;
        private Rock[] rocks;

        private List<Bullet> bullets = new List<Bullet>();

        private Texture2D welcomeSprite;

        private SpriteFont lives;
        private SpriteFont score;

        private String livesText;
        private String scoreText;

        private KeyboardState oldState;

        private bool welcomeScreenActive;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // graphics.ToggleFullScreen(); // FULL SCREEEEEEEEEN
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            WIDTH = GraphicsDevice.Viewport.Width;
            HEIGHT = GraphicsDevice.Viewport.Height;
            rockCount = 10;

            welcomeScreenActive = true;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // welcomeSprite = Content.Load<Texture2D>(""); // Add welcome screen image

            lives = Content.Load<SpriteFont>("Courier New");
            score = lives;

            livesText = "3";
            scoreText = "0";

            ship = new Ship(Content, WIDTH, HEIGHT);
            rocks = new Rock[rockCount];

            Random random = new Random();

            for (int i = 0; i < rockCount; i++)
            {
                rocks[i] = new Rock(Content, WIDTH, HEIGHT, random);
            }
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public void Shoot()
        {
            Bullet bull = new Bullet(Content.Load<Texture2D>("bulletTest"));
            bull.bulletVel = new Vector2((float)Math.Cos(ship.rot), (float)Math.Sin(ship.rot)) * 5f + ship.shipVel ;
            bull.bulletPos = ship.shipPos + bull.bulletVel * 5;

            if (bullets.Count() < 20)
            {
                bullets.Add(bull);
            }

        }

        protected override void Update(GameTime gameTime)
        {
            if (welcomeScreenActive)
            {
                KeyboardState state = Keyboard.GetState();

                if (state.IsKeyDown(Keys.Enter))
                {
                    welcomeScreenActive = false;
                }
            }
            else
            {
                KeyboardState newState = Keyboard.GetState();

                Edges edges = new Edges();

                ship.Update(edges);

                foreach (Rock rock in rocks)
                {
                    rock.Update(edges);
                }

                if (newState.IsKeyUp(Keys.Space) && oldState.IsKeyDown(Keys.Space))
                {
                    Shoot();
                }

                oldState = newState;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if (welcomeScreenActive)
            {
                // Draw welcome screen
            }
            else
            {
                Vector2 scoreSize = score.MeasureString(scoreText);
                
                foreach (Rock rock in rocks)
                {
                    rock.Draw(spriteBatch);
                }

                foreach (Bullet bull in bullets)
                {
                    bull.Draw(spriteBatch);
                }

                ship.Draw(spriteBatch);

                spriteBatch.DrawString(lives, livesText, new Vector2(20, 20), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(score, scoreText, new Vector2(WIDTH - 20, 20), Color.White, 0, new Vector2(scoreSize.X, 0), 1, SpriteEffects.None, 0);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
