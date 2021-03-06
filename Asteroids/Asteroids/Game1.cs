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
using System.Threading;

namespace Asteroids
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        private int WIDTH, HEIGHT, rockCount, livesCount, scoreCount;
        
        private Ship ship;

        private List<Rock> rocks = new List<Rock>();
        private List<Bullet> bullets = new List<Bullet>();

        private Random random;

        private SpriteFont fontSprite;

        private String welcomeText, livesText, scoreText, finalScoreText;

        private Texture2D background1;
        private Texture2D background2;
        private Texture2D backgroundWelcome;

        private Rectangle backgroundRect1;
        private Rectangle backgroundRect2;
        private Rectangle backgroundRectWelcome;

        private SoundEffect shootSound, explodeSound, destroySound;

        private KeyboardState oldState;

        private bool welcomeScreenActive, gameStarted;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            IsMouseVisible = true;

            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;
            // graphics.ToggleFullScreen(); // FULL SCREEEEEEEEEN
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            WIDTH = GraphicsDevice.Viewport.Width;
            HEIGHT = GraphicsDevice.Viewport.Height;
            rockCount = 10;

            welcomeScreenActive = true;
            gameStarted = false;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            livesCount = 10;
            scoreCount = 0;

            fontSprite = Content.Load<SpriteFont>("Courier New");

            welcomeText = "Press Enter to start the game...";
            livesText = "lives: " + livesCount.ToString();
            scoreText = "score: " + scoreCount.ToString();

            background1 = Content.Load<Texture2D>("bg1");
            background2 = Content.Load<Texture2D>("bg2");
            backgroundWelcome = Content.Load<Texture2D>("bgWelcome");

            backgroundRect1 = new Rectangle(0, 0, WIDTH, HEIGHT);
            backgroundRect2 = new Rectangle(-WIDTH, 0, WIDTH, HEIGHT);
            backgroundRectWelcome = backgroundRect1;

            ship = new Ship(Content, WIDTH, HEIGHT);

            random = new Random();

            for (int i = 0; i < rockCount; i++)
            {
                rocks.Add(new Rock(Content, WIDTH, HEIGHT, random, ship.pos));
            }

            shootSound = Content.Load<SoundEffect>("shoot");
            explodeSound = Content.Load<SoundEffect>("explode");
            destroySound = Content.Load<SoundEffect>("destroy");

            SoundEffect music = Content.Load<SoundEffect>("music");

            SoundEffectInstance instanceMusic = music.CreateInstance();
            instanceMusic.IsLooped = true;

            music.Play(0.5f, 0.0f, 0.0f);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void Shoot(Vector2 startingPosition, Vector2 movement, float angle, Vector2 shipSpeed)
        {
            Bullet bull = new Bullet(Content, startingPosition, movement, angle, shipSpeed);
            bull.vel = new Vector2((float)Math.Cos(ship.rot), (float)Math.Sin(ship.rot)) * 5f + ship.vel;
            bull.pos = ship.pos + bull.vel * 5;

            if (bullets.Count() < 20)
            {
                bullets.Add(bull);

                shootSound.Play(0.25f, 0.0f, 0.0f);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            backgroundRect1.X += 1;
            backgroundRect2.X += 1;

            if (backgroundRect1.X == WIDTH)
            {
                backgroundRect1.X = -WIDTH;
            }

            if (backgroundRect2.X == WIDTH)
            {
                backgroundRect2.X = -WIDTH;
            }
            
            if (welcomeScreenActive)
            {
                KeyboardState state = Keyboard.GetState();

                if (state.IsKeyDown(Keys.Enter))
                {
                    welcomeScreenActive = false;
                    gameStarted = true;
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
                        livesCount--;

                        livesText = "lives: " + livesCount.ToString();
                        
                        rocksToRemove.Add(rock);

                        destroySound.Play(0.75f, 0.0f, 0.0f);
                    }

                    List<Bullet> bulletsToRemove = new List<Bullet>();

                    foreach (Bullet bull in bullets)
                    {
                        Rectangle rectBull = new Rectangle((int)bull.pos.X, (int)bull.pos.Y, bull.bulletSprite.Width, bull.bulletSprite.Height);
                        
                        bull.Update(gameTime);

                        if (rectBull.Intersects(rectRock))
                        {
                            scoreCount++;

                            scoreText = "score: " + scoreCount.ToString();

                            bulletsToRemove.Add(bull);

                            rock.rockSprite = Content.Load<Texture2D>("glazed");
                            rock.hitWithBullet = true;

                            explodeSound.Play(0.25f, 0.0f, 0.0f);
                        }

                        if (bull.bulletRemove(WIDTH, HEIGHT))
                        {
                            bulletsToRemove.Add(bull);
                        }
                    }

                    foreach (Bullet bull in bulletsToRemove)
                    {
                        bullets.Remove(bull);
                    }

                    if (rock.hitWithBullet)
                    {
                        rock.timer--;

                        if (rock.timer <= 0)
                        {
                            rocksToRemove.Add(rock);
                        }
                    }
                }

                foreach (Rock rock in rocksToRemove)
                {
                    rocks.Remove(rock);
                    
                    for (int i = 0; i < 2; i++)
                    {
                        rocks.Add(new Rock(Content, WIDTH, HEIGHT, random, ship.pos));
                    }
                }

                if (newState.IsKeyUp(Keys.Space) && oldState.IsKeyDown(Keys.Space))
                {
                    Shoot(ship.barrel, ship.vel, ship.rot, ship.getVel());
                }

                oldState = newState;

                if (livesCount <= 0)
                {
                    welcomeScreenActive = true;

                    finalScoreText = "Your final score: " + scoreText;

                    livesCount = 10;
                    scoreCount = 0;

                    livesText = "lives: " + livesCount.ToString();
                    scoreText = "score: " + scoreCount.ToString();

                    ship = new Ship(Content, WIDTH, HEIGHT);

                    rocks = new List<Rock>();
                    bullets = new List<Bullet>();

                    for (int i = 0; i < rockCount; i++)
                    {
                        rocks.Add(new Rock(Content, WIDTH, HEIGHT, random, ship.pos));
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background1, backgroundRect1, Color.White);
            spriteBatch.Draw(background2, backgroundRect2, Color.White);

            if (welcomeScreenActive)
            {
                spriteBatch.Draw(backgroundWelcome, backgroundRectWelcome, Color.White);
                
                Vector2 welcomeSize = fontSprite.MeasureString(welcomeText);

                spriteBatch.DrawString(fontSprite, welcomeText, new Vector2(WIDTH / 2, HEIGHT / 4 * 3), Color.White, 0, new Vector2(welcomeSize.X / 2, welcomeSize.Y / 2), 1f, SpriteEffects.None, 0);

                if (gameStarted)
                {
                    Vector2 finalScoreSize = fontSprite.MeasureString(finalScoreText);

                    spriteBatch.DrawString(fontSprite, finalScoreText, new Vector2(WIDTH / 2, HEIGHT / 4 * 3 + 50), Color.White, 0, new Vector2(finalScoreSize.X / 2, finalScoreSize.Y / 2), 0.8f, SpriteEffects.None, 0);
                }
            }
            else
            {
                Vector2 scoreSize = fontSprite.MeasureString(scoreText);
                
                foreach (Rock rock in rocks)
                {
                    rock.Draw(spriteBatch);
                }

                foreach (Bullet bull in bullets)
                {
                    bull.Draw(spriteBatch);
                }

                ship.Draw(spriteBatch);

                spriteBatch.DrawString(fontSprite, livesText, new Vector2(20, 20), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(fontSprite, scoreText, new Vector2(WIDTH - 20, 20), Color.White, 0, new Vector2(scoreSize.X, 0), 1, SpriteEffects.None, 0);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
