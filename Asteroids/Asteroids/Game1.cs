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

        Ship ship;
        KeyboardState oldState;
        Rock[] rocks;

        List<Bullets> bullets = new List<Bullets>();
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";            
            //graphics.ToggleFullScreen(); //FULLSCREEEEEEEEEN
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            WIDTH = GraphicsDevice.Viewport.Width;
            HEIGHT = GraphicsDevice.Viewport.Height;
            rockCount = 10;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

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
            Bullets bull = new Bullets(Content.Load<Texture2D>("bulletTest"));
            bull.bulletVel = new Vector2((float)Math.Cos(ship.rot), (float)Math.Sin(ship.rot)) * 5f + ship.shipVel ;
            bull.bulletPos = ship.shipPos + bull.bulletVel * 5;

            if (bullets.Count() < 20)
            {
                bullets.Add(bull);
            }

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState newState;
            newState = Keyboard.GetState();

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
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            foreach (Rock rock in rocks)
            {
                rock.Draw(spriteBatch);
            }
            foreach (Bullets bull in bullets)
            {
                bull.Draw(spriteBatch);
            }
            ship.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
