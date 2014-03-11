using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    class Edges
    {
        public Vector2 travelEdges(Texture2D sprite, Vector2 pos, int screenWidth, int screenHeight)
        {
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

            return pos;
        }
    }
}
