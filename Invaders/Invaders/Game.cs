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

        private Rectangle windowBoundaries;
        private Rectangle playableBoundaries;
        private Random random = new Random();

        private Direction invaderDirection;
        private List<Invader> invaders;

        private PlayerShip playerShip;
        private List<Shot> playerShots;
        private List<Shot> invaderShots;

        private Star star;

        private int topPadding = 70;
        private int leftRightPadding = 20;

        private int invaderTop = 0;

        public Game(Main MainForm)
        {
            this.MainForm = MainForm;
            windowBoundaries = new Rectangle(0, 0, this.MainForm.Width, this.MainForm.Height);
            playableBoundaries = new Rectangle(0, 0, windowBoundaries.Width - leftRightPadding, windowBoundaries.Height - leftRightPadding);

            invaders = new List<Invader>();
            for (int i = 0; i < 6; i++)
            {
                int j = 0;
                foreach (Type type in Enum.GetValues(typeof(Type)))
                {
                    invaders.Add(new Invader(type, new Point(60 * i + leftRightPadding, j * 60 + topPadding), i * 10));
                    j++;
                }
            }
            invaderDirection = Direction.Right;

            playerShip = new PlayerShip(windowBoundaries, playableBoundaries) { Location = new Point(playableBoundaries.Width / 2, playableBoundaries.Height) };

            star = new Star(new Point(random.Next(0, windowBoundaries.Width), random.Next(0, windowBoundaries.Height)), new Pen(Color.DarkMagenta), windowBoundaries);
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
                shot = new Shot(new Point(this.playerShip.Location.X + this.playerShip.Width / 2, this.playerShip.Location.Y + this.playerShip.Height), Direction.Top, windowBoundaries);
                temp = true;
            }

            using (Graphics g = this.MainForm.CreateGraphics())
            {
                shot.Draw(g);
            }
        }

        public void Go()
        {
            star.Twinkle(random);

            getDirectionForInvaders();
            foreach (Invader invader in invaders)
            {   
                invader.Move(invaderDirection);
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

        private void getDirectionForInvaders()
        {

            var invadersEdge = (from invader in invaders 
                               orderby invader.Location.X descending
                               select invader.Location.X).Take(6);

            var invaderTop = (from invader in invaders
                              orderby invader.Location.Y descending
                              select invader.Location.Y).Take(1);

            if (this.invaderTop == 0)
            {
                this.invaderTop = invaderTop.First();
            }

            Console.WriteLine(this.invaderTop);

            foreach (int x in invadersEdge)
            {
                switch (invaderDirection)
                {
                    case Direction.Right:
                        if (x >= (playableBoundaries.Width - leftRightPadding - 54))
                        {
                            invaderDirection = Direction.Down;
                        }
                        break;
                    case Direction.Left:
                        if (x < (5 * 54) + leftRightPadding)
                        {
                            invaderDirection = Direction.Down;
                        }
                        break;
                    case Direction.Down:
                        if (this.invaderTop == invaderTop.First())
                        {
                            invaderDirection = Direction.Down;
                            this.invaderTop = invaderTop.First();
                        }
                        else
                        {
                            if (x > 700)
                            {
                                invaderDirection = Direction.Left;
                                this.invaderTop = 0;
                            }
                            else
                            {
                                invaderDirection = Direction.Right;
                                this.invaderTop = 0;
                            }
                        }
                        break;
                         
                }
            }
        }
    }
}
