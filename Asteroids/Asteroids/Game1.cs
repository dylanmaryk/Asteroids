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

        private List<Rock> rocks = new List<Rock>();
        private List<Bullet> bullets = new List<Bullet>();

        private SpriteFont welcomeSprite, livesSprite, scoreSprite;

        private String welcomeText, livesText, scoreText;

        private KeyboardState oldState;

        private bool welcomeScreenActive;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            IsMouseVisible = true;

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

            welcomeSprite = Content.Load<SpriteFont>("Courier New");
            livesSprite = welcomeSprite;
            scoreSprite = welcomeSprite;

            welcomeText = "Press Enter to start the game...";
            livesText = "3";
            scoreText = "0";

            ship = new Ship(Content, WIDTH, HEIGHT);

            Random random = new Random();

            for (int i = 0; i < rockCount; i++)
            {
                rocks.Add(new Rock(Content, WIDTH, HEIGHT, random));
            }
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public void Shoot()
        {
            Bullet bull = new Bullet(Content, ship.pos, ship.vel, ship.rot);
            bull.vel = new Vector2((float)Math.Cos(ship.rot), (float)Math.Sin(ship.rot)) * 5f + ship.vel;
            bull.pos = ship.pos + bull.vel * 5;

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

                List<Rock> rocksToRemove = new List<Rock>();

                foreach (Rock rock in rocks)
                {
                    Rectangle rectShip = new Rectangle((int)ship.pos.X, (int)ship.pos.Y, ship.shipSprite.Width, ship.shipSprite.Height);
                    Rectangle rectRock = new Rectangle((int)rock.pos.X, (int)rock.pos.Y, rock.rockSprite.Width, rock.rockSprite.Height);

                    rock.Update(edges);

                    if (rectRock.Intersects(rectShip))
                    {
                        int livesInt = Convert.ToInt32(livesText);
                        livesInt--;

                        livesText = livesInt.ToString();
                        
                        rocksToRemove.Add(rock);
                    }

                    List<Bullet> bulletsToRemove = new List<Bullet>();

                    foreach (Bullet bull in bullets)
                    {
                        Rectangle rectBull = new Rectangle((int)bull.pos.X, (int)bull.pos.Y, bull.bulletSprite.Width, bull.bulletSprite.Height);

                        bull.Update(gameTime);

                        if (rectBull.Intersects(rectRock))
                        {
                            int scoreInt = Convert.ToInt32(scoreText);
                            scoreInt++;

                            scoreText = scoreInt.ToString();

                            bulletsToRemove.Add(bull);

                            rock.rockSprite = Content.Load<Texture2D>("glazed");
                            
                            // Remove rock after 1 second
                        }
                    }

                    foreach (Bullet bull in bulletsToRemove)
                    {
                        bullets.Remove(bull);
                    }
                }

                foreach (Rock rock in rocksToRemove)
                {
                    rocks.Remove(rock);
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
                Vector2 welcomeSize = welcomeSprite.MeasureString(welcomeText);

                spriteBatch.DrawString(welcomeSprite, welcomeText, new Vector2(WIDTH / 2, HEIGHT / 2), Color.White, 0, new Vector2(welcomeSize.X / 2, 0), 0.5f, SpriteEffects.None, 0);
            }
            else
            {
                Vector2 scoreSize = scoreSprite.MeasureString(scoreText);
                
                foreach (Rock rock in rocks)
                {
                    rock.Draw(spriteBatch);
                }

                foreach (Bullet bull in bullets)
                {
                    bull.Draw(spriteBatch);
                }

                ship.Draw(spriteBatch);

                spriteBatch.DrawString(livesSprite, livesText, new Vector2(20, 20), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(scoreSprite, scoreText, new Vector2(WIDTH - 20, 20), Color.White, 0, new Vector2(scoreSize.X, 0), 1, SpriteEffects.None, 0);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
