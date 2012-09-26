using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Invaders
{
    class PlayerShip : PictureBox
    {
        private int step = 20;

        private int width = 33;
        private int height = 54;

        private Bitmap bitmap;
        public Point Location;

        public Rectangle Area
        {
            get
            {
                return new Rectangle(Location, bitmap.Size);
            }
        }

        private Rectangle windowBoundaries;
        private Rectangle playableBoundaries;

        private int padding = 0;

        public PlayerShip(Rectangle windowBoundaries, Rectangle playableBoundaries)
        {
            this.windowBoundaries = windowBoundaries;
            this.playableBoundaries = playableBoundaries;

            padding = (this.windowBoundaries.Width - this.playableBoundaries.Width) / 2;

            bitmap = Game.ResizeImage(Properties.Resources.player, 54, 33);
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(bitmap, Location.X, Location.Y - height);
        }

        public void Move(Direction direction)
        {
            if (direction == Direction.Right)
            {
                if (Location.X + step + 60 <= (windowBoundaries.Width - padding))
                {
                    Location = new Point(Location.X + step, Location.Y);
                }
            }
            else
            {
                if (Location.X - step >= padding)
                {
                    Location = new Point(Location.X - step, Location.Y);
                }
            }
        }
    }
}
