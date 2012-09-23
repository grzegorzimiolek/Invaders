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

        private Bitmap bitmap;
        public Point Location;

        public PlayerShip()
        {
            bitmap = Game.ResizeImage(Properties.Resources.player, 54, 33);
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(bitmap, Location.X, Location.Y / 2);
            
            /* Double buffered
            using (g = Graphics.FromImage(bitmap))
            {
                DrawOneFrame(g);
            }
             */
        }

        private void DrawOneFrame(Graphics g)
        {

            //g.DrawImage(bitmap, Location.X, Location.Y - 80); //Double buffered
        }

        public void Move(Direction direction)
        {
            if (direction == Direction.Right)
            {
                if (Location.X + step + 60 <= 600)
                {
                    Location = new Point(Location.X + step, 800);
                }
            }
            else
            {
                if (Location.X - step >= 0)
                {
                    Location = new Point(Location.X - step, 800);
                }
            }
        }
    }
}
