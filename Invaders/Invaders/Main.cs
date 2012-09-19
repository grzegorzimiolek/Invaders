using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Invaders
{
    public partial class Main : Form
    {
        private Game game;

        public Main()
        {
            InitializeComponent();

            gameTimer.Interval = 10;
            gameTimer.Tick += new EventHandler(gameTimer_Tick);
            gameTimer.Enabled = true;

            game = new Game(this);
            game.DrawBug();
            game.DrawSpaceShip();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            game.Go();
            this.Invalidate();
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    game.FireShot();
                    break;
                case Keys.Right:
                    game.MoveSpaceShip(Direction.Right);
                    break;
                case Keys.Left:
                    game.MoveSpaceShip(Direction.Left);
                    break;

                default: break;
            }
        }

        private void onPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (g = this.CreateGraphics())
            {
                game.Go();
            }
        }
    }
}
