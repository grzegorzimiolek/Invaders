using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Invaders
{
    public enum Type
    {
        Bug,
        Saucer,
        Satellite,
        Spaceship,
        Star,
    }

    class Invader : PictureBox
    {
        private const int HorizontalInterval = 10;
        private const int VerticalInterval = 40;

        private Bitmap image;

        public Point Location { get; private set; }
        public Type InvaderType { get; private set; }

        public Rectangle Area
        {
            get
            {
                return new Rectangle(Location, image.Size);
            }
        }

        public int Score { get; private set; }
        private Direction direction = Direction.Right;
        private int IntervalCount = 0;

        public Invader(Type invaderType, Point location, int score)
        {
            this.InvaderType = invaderType;
            this.Location = location;
            this.Score = score;

            BackColor = System.Drawing.Color.Transparent;
        }

        public void Move(Direction direction)
        {
            IntervalCount++;
            if ((VerticalInterval * 3) == IntervalCount)
            {
                IntervalCount = 0;
                
                if (direction == Direction.Right)
                {
                    Location = new Point(Location.X + VerticalInterval, Location.Y);
                }
                else
                {
                    if (Location.X - VerticalInterval >= 0)
                    {
                        Location = new Point(Location.X - VerticalInterval, Location.Y);
                    }
                }
            }
        }

        public void Draw(Graphics g, int animationCell)
        {
            g.DrawImage(InvaderImage(animationCell/2), Location.X, Location.Y);
        }

        private Bitmap InvaderImage(int animationCell)
        {
            Bitmap bitmap = null;
            switch (InvaderType)
            {
                case Type.Bug:
                    switch(animationCell)
                    {
                        case 0: bitmap = Game.ResizeImage(Properties.Resources.bug1, 45, 51); break;
                        case 1: bitmap = Game.ResizeImage(Properties.Resources.bug2, 45, 51); break;
                        case 2: bitmap = Game.ResizeImage(Properties.Resources.bug3, 45, 51); break;
                        case 3: bitmap = Game.ResizeImage(Properties.Resources.bug4, 45, 51); break;
                    }
                    break;
                case Type.Satellite:
                    switch (animationCell)
                    {
                        case 0: bitmap = Game.ResizeImage(Properties.Resources.satellite1, 50, 49); break;
                        case 1: bitmap = Game.ResizeImage(Properties.Resources.satellite2, 50, 49); break;
                        case 2: bitmap = Game.ResizeImage(Properties.Resources.satellite3, 50, 49); break;
                        case 3: bitmap = Game.ResizeImage(Properties.Resources.satellite4, 50, 49); break;
                    }
                    break;
                case Type.Saucer:
                    switch (animationCell)
                    {
                        case 0: bitmap = Game.ResizeImage(Properties.Resources.flyingsaucer1, 39, 45); break;
                        case 1: bitmap = Game.ResizeImage(Properties.Resources.flyingsaucer2, 39, 45); break;
                        case 2: bitmap = Game.ResizeImage(Properties.Resources.flyingsaucer3, 39, 45); break;
                        case 3: bitmap = Game.ResizeImage(Properties.Resources.flyingsaucer4, 39, 45); break;
                    }
                    break;
                case Type.Star:
                    switch (animationCell)
                    {
                        case 0: bitmap = Game.ResizeImage(Properties.Resources.star1, 51, 51); break;
                        case 1: bitmap = Game.ResizeImage(Properties.Resources.star2, 51, 51); break;
                        case 2: bitmap = Game.ResizeImage(Properties.Resources.star3, 51, 51); break;
                        case 3: bitmap = Game.ResizeImage(Properties.Resources.star4, 51, 51); break;
                    }
                    break;
                case Type.Spaceship:
                    switch (animationCell)
                    {
                        case 0: bitmap = Game.ResizeImage(Properties.Resources.spaceship1, 53, 43); break;
                        case 1: bitmap = Game.ResizeImage(Properties.Resources.spaceship2, 53, 43); break;
                        case 2: bitmap = Game.ResizeImage(Properties.Resources.spaceship3, 53, 43); break;
                        case 3: bitmap = Game.ResizeImage(Properties.Resources.spaceship4, 53, 43); break;
                    }
                    break;
            }

            return bitmap;
        }
    }
}
