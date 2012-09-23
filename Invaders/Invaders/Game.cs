using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Invaders
{
    public enum Direction
    {
        Left,
        Right,
        Top,
        Down,
    }

    class Game
    {
        private Main MainForm;
        
        private Shot shot;

        private Boolean temp = false;

        private Rectangle boundaries;
        private Random random;

        private Direction invaderDirection;
        private List<Invader> invaders;

        private PlayerShip playerShip;
        private List<Shot> playerShots;
        private List<Shot> invaderShots;

        private Star star;

        public Game(Main MainForm)
        {
            this.MainForm = MainForm;

            invaders = new List<Invader>();
            
            for (int i = 0; i < 6; i++)
            {
                int j = 0;
                foreach (Type type in Enum.GetValues(typeof(Type)))
                {   
                    invaders.Add( new Invader(type, new Point(60 * i, j * 60), i * 10));
                    j++;
                }
            }

            this.playerShip = new PlayerShip() { Location = new Point(280, 800) };
            boundaries = new Rectangle(0, 0, this.MainForm.Width, this.MainForm.Height);
            
            random = new Random();
            
            star = new Star(new Point(random.Next(0, boundaries.Width), random.Next(0, boundaries.Height)), new Pen(Color.DarkMagenta), boundaries);
            using (Graphics g = this.MainForm.CreateGraphics())
            {
                star.Draw(g);
            }
        }

        public void MoveSpaceShip(Direction direction)
        {
            this.playerShip.Move(direction);
        }

        public static Bitmap ResizeImage(Bitmap Picture, int width, int height)
        {
            Bitmap resizedPicture = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(resizedPicture))
            {
                graphics.DrawImage(Picture, 0, 0, width, height);
            }
            return resizedPicture;
        }

        public void FireShot()
        {
            if (temp == false)
            {
                shot = new Shot(new Point(this.playerShip.Location.X + this.playerShip.Width / 2, this.playerShip.Location.Y + this.playerShip.Height), Direction.Top, boundaries);
                temp = true;
            }

            using (Graphics g = this.MainForm.CreateGraphics())
            {
                shot.Draw(g);
            }
        }

        public void Go()
        {
            if (temp)
            {
                shot.Move();
                FireShot();
            }
            star.Twinkle(random);

            foreach (Invader invader in invaders)
            {
                invader.Move(Direction.Right);
            }
        }

        public void Draw(Graphics g, int animationCell)
        {
            star.Draw(g);
            playerShip.Draw(g);
            
            foreach (Invader invader in invaders)
            {
                invader.Draw(g, animationCell);
            }
        }
    }
}
