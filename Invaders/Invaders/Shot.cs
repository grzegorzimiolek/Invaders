using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Invaders
{
    class Shot
    {
        private const int moveInterval = 40;
        private const int width = 5;
        private const int height = 15;

        public Point Location;

        private Direction direction;
        private Rectangle boundaries;

        private int IntervalCount = 0;
        
        public Shot(Point location, Direction direction, Rectangle boundaries)
        {
            this.Location = location;
            this.direction = direction;
            this.boundaries = boundaries;

            //Location.Y = boundaries.Height;
        }

        public void Draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color.Yellow);
            g.FillRectangle(brush, new Rectangle(Location.X - 25, Location.Y, width, height));
        }

        public bool Move()
        {
            if (Location.Y < 120 && direction == Direction.Top)
            {
                return false;
            }

            if (Location.Y > boundaries.Height && direction == Direction.Down)
            {
                return false;
            }
            

            IntervalCount++;
            if ((moveInterval/4) == IntervalCount)
            {
                IntervalCount = 0;
                if (direction == Direction.Top)
                {
                    Location.Y -= moveInterval;
                }
                else
                {
                    Location.Y += moveInterval;
                }       
            }
            return true;
        }
    }
}
