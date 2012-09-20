using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Invaders
{
    class Star
    {
        public Point point;
        public Pen pen;

        private List<Star> stars;
        private Rectangle boundaries;

        private int count = 300;

        private Random random;

        public Star(Point point, Pen pen, Rectangle boundaries)
        {
            this.point = point;
            this.pen = pen;
            this.boundaries = boundaries;

            random = new Random();
            stars = new List<Star>();
            
        }

        public void Draw(Graphics g)
        {
            if (stars.Count == 0)
            {
                fillListOfStars(this.boundaries, random);
            }
            
            foreach (Star star in stars)
            {
                g.DrawRectangle(star.pen,star.point.X, star.point.Y, 1, 1);
            }
        }

        public void Twinkle(Random random)
        {
            int index;
            for (int i = 0; i < 5; i++)
            {
                index = random.Next(0, count);
                if (stars.ElementAtOrDefault(index) != null)
                {
                    stars.RemoveAt(index);
                }
                
            }

            for (int i = 0; i < 5; i++)
            {
                stars.Add(
                    new Star(
                        new Point(random.Next(0, boundaries.Width), random.Next(0, boundaries.Height)),
                        RandomPen(random),
                        boundaries));
            }
        }

        private void fillListOfStars(Rectangle boundaries, Random random)
        {
            for (int i = 0; i < this.count; i++)
            {   
                stars.Add(
                    new Star(
                        new Point(random.Next(0, boundaries.Width), random.Next(0, boundaries.Height)),
                        RandomPen(random),
                        boundaries));
                 
            }
        }

        private Pen RandomPen(Random random)
        {
            Pen pen = null;

            switch (random.Next(0, 4))
            {
                case 0:
                    pen = new Pen(Color.Red);
                    break;
                case 1:
                    pen = new Pen(Color.Pink);
                    break;
                case 2:
                    pen = new Pen(Color.Yellow);
                    break;
                case 3:
                    pen = new Pen(Color.Coral);
                    break;
                case 4:
                    pen = new Pen(Color.Cyan);
                    break;
            }

            return pen;
        }
    }
}
