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
        private InvaderControlTest bug;
        
        private Shot shot;

        private Boolean temp = false;

        private Rectangle boundaries;
        private Random random;

        private Direction invaderDirection;
        private List<Invader> invaders;

        private PlayerShip playerShip;
        private List<Shot> playerShots;
        private List<Shot> invaderShots;

        private Stars stars;

        public Game(Main MainForm)
        {
            this.MainForm = MainForm;
            this.bug = new InvaderControlTest(this.MainForm) { Location = new Point(0, 0) };
            this.playerShip = new PlayerShip(this.MainForm) { Location = new Point(0, this.MainForm.Height - 54) };
            boundaries = new Rectangle(0, 0, this.MainForm.Width, this.MainForm.Height);
        }

        public void DrawBug()
        {
            MainForm.Controls.Add(this.bug);
        }

        public void DrawSpaceShip()
        {
            MainForm.Controls.Add(this.playerShip);
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
        }


        public void Draw(Graphics g)
        {

        }
    }
}
