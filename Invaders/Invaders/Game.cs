using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

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
        private PlayerShip playerShip;

        public Game(Main MainForm)
        {
            this.MainForm = MainForm;
            this.bug = new InvaderControlTest(this.MainForm) { Location = new Point(0, 0) };
            this.playerShip = new PlayerShip(this.MainForm) { Location = new Point(0, this.MainForm.Height - 54) };
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
    }
}
