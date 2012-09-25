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

        private Rectangle windowBoundaries;
        private Rectangle playableBoundaries;
        private Random random = new Random();

        private Direction invaderDirection;
        private List<Invader> invaders;

        private PlayerShip playerShip;
        private List<Shot> playerShots;
        private List<Shot> invaderShots;

        private Star star;

        private Font font = new Font("Arial", 18, FontStyle.Bold);

        private int topPadding = 70;
        private int leftRightPadding = 20;

        private int invaderTop = 0;

        private int playerScore = 0;

        public Game(Main MainForm)
        {
            this.MainForm = MainForm;
            windowBoundaries = new Rectangle(0, 0, this.MainForm.Width, this.MainForm.Height);
            playableBoundaries = new Rectangle(0, 0, windowBoundaries.Width - leftRightPadding, windowBoundaries.Height - leftRightPadding);

            invaders = new List<Invader>();
            for (int i = 0; i < 6; i++)
            {
                int j = 0;
                int score = 0;
                foreach (Type type in Enum.GetValues(typeof(Type)))
                {
                    switch (type)
                    {
                        case Type.Star:
                            score = 10;
                            break;
                        case Type.Spaceship:
                            score = 20;
                            break;
                        case Type.Saucer:
                            score = 30;
                            break;
                        case Type.Bug:
                            score = 40;
                            break;
                        case Type.Satellite:
                            score = 50;
                            break;
                    }

                    invaders.Add(new Invader(type, new Point(60 * i + leftRightPadding, j * 60 + topPadding), score));
                    j++;
                }
            }
            invaderDirection = Direction.Right;
            invaderShots = new List<Shot>();

            playerShip = new PlayerShip(windowBoundaries, playableBoundaries) { Location = new Point(playableBoundaries.Width / 2, playableBoundaries.Height) };
            playerShots = new List<Shot>();

            star = new Star(new Point(random.Next(0, windowBoundaries.Width), random.Next(0, windowBoundaries.Height)), new Pen(Color.DarkMagenta), windowBoundaries);
            using (Graphics g = this.MainForm.CreateGraphics())
            {
                star.Draw(g);
            }
        }

        public void MoveSpaceShip(Direction direction)
        {
            playerShip.Move(direction);
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
            if (playerShots.Count < 3)
            {
                playerShots.Add(new Shot(new Point(playerShip.Location.X + playerShip.Width / 2, playerShip.Location.Y + playerShip.Height), Direction.Top, windowBoundaries));
            }
        }

        public void Go()
        {
            
            star.Twinkle(random);
            moveInvaders();
            checkForInvadersCollisions();
        }

        public void Draw(Graphics g, int animationCell)
        {
            showScore(g);
            star.Draw(g);
            playerShip.Draw(g);

            for (int i = 0; i < playerShots.Count(); i++)
            {
                if (playerShots[i].Move())
                {
                    playerShots[i].Draw(g);
                }
                else
                {
                    playerShots.RemoveAt(i);
                }
            }

            foreach (Invader invader in invaders)
            {
                invader.Draw(g, animationCell);
            }
        }

        private void showScore(Graphics g)
        {
            g.DrawString("Score: " + playerScore.ToString(), font, Brushes.White, new Point(10, 10));
        }

        private void returnFire()
        {

        }

        private void checkForSpaceShipCollisions()
        {

        }

        private void checkForInvadersCollisions()
        {
            for (int i = 0; i < playerShots.Count(); i++)
            {
                var hitInvaders = from invader in invaders
                                  where invader.Area.Contains(playerShots[i].Location)
                                  select invader;

                List<Invader> listToRemove = new List<Invader>();

                foreach (Invader invader in hitInvaders)
                {
                    listToRemove.Add(invader);
                    
                }

                if (listToRemove.Count() > 0)
                {
                    foreach (Invader invader in listToRemove)
                    {
                        playerScore += invader.Score;
                        invaders.Remove(invader);
                        playerShots.RemoveAt(i);
                    }
                }
            }
            
        }

        private void moveInvaders()
        {
            getDirectionForInvaders();
            foreach (Invader invader in invaders)
            {
                invader.Move(invaderDirection);
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
