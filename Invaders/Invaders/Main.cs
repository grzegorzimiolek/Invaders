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
        private List<Keys> keysPressed = new List<Keys>();

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

            foreach (Keys key in keysPressed)
            {
                if (key == Keys.Left)
                {
                    game.MoveSpaceShip(Direction.Left);
                    //return;
                }
                else if (key == Keys.Right)
                {
                    game.MoveSpaceShip(Direction.Right);
                    //return;
                }
            }

            this.Invalidate();
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                Application.Exit();
            }

            if (e.KeyCode == Keys.Space)
            {
                game.FireShot();
            }

            if (keysPressed.Contains(e.KeyCode))
            {
                keysPressed.Remove(e.KeyCode);
            }

            keysPressed.Add(e.KeyCode);
        }

        private void onPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (g = this.CreateGraphics())
            {
                game.Go();
                game.Draw(g);
            }
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
            if (keysPressed.Contains(e.KeyCode))
            {
                keysPressed.Remove(e.KeyCode);
            }
        }
    }
}
