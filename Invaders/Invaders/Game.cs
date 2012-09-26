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
        private int playerLifes = 3;

        private bool isAlive = true;
        private bool gameOver = false;
        private Timer deadTimer = new Timer();

        public Game(Main MainForm)
        {
            deadTimer.Interval = 3000;
            deadTimer.Tick += new EventHandler(deadTimer_Tick);
            deadTimer.Enabled = false;

            this.MainForm = MainForm;
            windowBoundaries = new Rectangle(0, 0, this.MainForm.Width, this.MainForm.Height);
            playableBoundaries = new Rectangle(0, 0, windowBoundaries.Width - leftRightPadding, windowBoundaries.Height - leftRightPadding);

            invaders = new List<Invader>();
            createInvaders();
            invaderShots = new List<Shot>();

            createPlayerShip();
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
            if (playerShots.Count < 3 && isAlive)
            {
                playerShots.Add(new Shot(new Point(playerShip.Location.X + playerShip.Width / 2, playerShip.Location.Y - playerShip.Height), Direction.Top, windowBoundaries));
            }
        }

        public void Go()
        {
            star.Twinkle(random);

            if (!gameOver)
            {
                moveInvaders();
            }
            
            checkForInvadersCollisions();
            checkForSpaceShipCollisions();
        }

        public void Draw(Graphics g, int animationCell)
        {
            showScore(g);
            showLives(g);
            star.Draw(g);
            if (isAlive)
            {
                playerShip.Draw(g);
            }
            else
            {
                showMessageForPlayer(g);
                deadTimer.Enabled = true;
            }

            if (gameOver)
            {
                showGameOver(g);
            }

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

            for (int i = 0; i < invaderShots.Count(); i++)
            {
                if (invaderShots[i].Move())
                {
                    invaderShots[i].Draw(g);
                }
                else
                {
                    invaderShots.RemoveAt(i);
                }
            }

            foreach (Invader invader in invaders)
            {
                invader.Draw(g, animationCell);
            }

            if (random.Next(50) == 0)
            {
                returnFire();
            }
        }

        private void showLives(Graphics g)
        {
            for (int i = 1; i <= playerLifes; i++)
            {
                g.DrawImage(Properties.Resources.player, new Point(windowBoundaries.Width - (i * 54) - (i * leftRightPadding), 10)); 
            }
        }

        private void showScore(Graphics g)
        {
            g.DrawString("Score: " + playerScore.ToString(), font, Brushes.White, new Point(10, 10));
        }

        private void showGameOver(Graphics g)
        {
            invaders.Clear();
            g.DrawString("Game Over", font, Brushes.White, new Point(windowBoundaries.Width / 2 - 60, windowBoundaries.Height / 2 - 20));
        }

        private void showMessageForPlayer(Graphics g)
        {
            g.DrawString("You're dead :-) " + playerLifes.ToString() + " lives left...", font, Brushes.White, new Point(240, 10));
        }

        private void createPlayerShip()
        {
            playerShip = new PlayerShip(windowBoundaries, playableBoundaries) { Location = new Point(playableBoundaries.Width / 2, playableBoundaries.Height) };
        }

        private void createInvaders()
        {
            invaders.Clear();
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
        }

        private void deadTimer_Tick(object sender, EventArgs e)
        {
            if (!isAlive)
            {
                isAlive = true;
                deadTimer.Enabled = false;
                Reset();
            }
        }

        private void Reset()
        {
            if (!gameOver)
            {
                createInvaders();
                createPlayerShip();
            }
        }

        private void returnFire()
        {
            var invadersBottom = (from invader in invaders
                                orderby invader.Location.Y descending
                                select invader).Take(6);

            int i = 1;
            int selectInvader = random.Next(6);
            foreach (Invader invader in invadersBottom)
            {

                if (i == selectInvader)
                {
                    if (invaderShots.Count() < 2)
                    {
                        invaderShots.Add(new Shot(new Point(invader.Location.X + invader.Width / 2, invader.Location.Y + invader.Height), Direction.Down, windowBoundaries));
                    }
                }
                i++;
            }
        }

        private void checkForSpaceShipCollisions()
        {
            if (isAlive)
            {
                for (int i = 0; i < invaderShots.Count(); i++)
                {
                    if (playerShip.Area.Contains(invaderShots[i].Location))
                    {
                        isAlive = false;
                        if (playerLifes - 1 < 0)
                        {
                            gameOver = true;
                        }
                        else
                        {
                            playerLifes--;
                        }
                        
                    }
                }
            }
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
