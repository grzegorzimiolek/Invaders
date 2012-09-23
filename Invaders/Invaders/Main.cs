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
        private int animationCell = 0;

        public Main()
        {
            InitializeComponent();

            gameTimer.Interval = 10;
            gameTimer.Tick += new EventHandler(gameTimer_Tick);
            gameTimer.Enabled = true;

            animationTimer.Interval = 100;
            animationTimer.Tick += new EventHandler(animationTimer_Tick);
            animationTimer.Enabled = true;

            game = new Game(this);
            //game.DrawSpaceShip();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            game.Go();

            foreach (Keys key in keysPressed)
            {
                if (key == Keys.Left)
                {
                    game.MoveSpaceShip(Direction.Left);
                }
                else if (key == Keys.Right)
                {
                    game.MoveSpaceShip(Direction.Right);
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
            game.Draw(g, animationCell);
            game.Go();
        }

        /**
         * Fix PlayerShip flicker
         * http://social.msdn.microsoft.com/forums/en-US/winforms/thread/aaed00ce-4bc9-424e-8c05-c30213171c2c/
         */
        protected override CreateParams CreateParams { 
            get 
            { 
                CreateParams cp = base.CreateParams; 
                cp.ExStyle |= 0x02000000;
                cp.ExStyle |= 0x00080000;
                return cp; 
            } 
        } 

        private void onKeyUp(object sender, KeyEventArgs e)
        {
            if (keysPressed.Contains(e.KeyCode))
            {
                keysPressed.Remove(e.KeyCode);
            }
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            animationCell++;
            if (animationCell > 7)
            {
                animationCell = 0;
            }
        }
    }
}
