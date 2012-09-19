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

        public Point Location { get; private set; }

        private Direction direction;
        private Rectangle boundaries;

        private int x;
        private int y;

        private int IntervalCount = 0;
        
        public Shot(Point location, Direction direction, Rectangle boundaries)
        {
            this.Location = location;
            this.direction = direction;
            this.boundaries = boundaries;
            this.x = boundaries.Width;
            this.y = boundaries.Height;
        }

        public void Draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color.Yellow);
            g.FillRectangle(brush, new Rectangle(this.Location.X, this.y, width, height));
        }

        public bool Move()
        {
            IntervalCount++;
            if (moveInterval == IntervalCount)
            {
                IntervalCount = 0;
                this.y -= moveInterval;
                return true;
            }
            return false;
        }
    }
}
