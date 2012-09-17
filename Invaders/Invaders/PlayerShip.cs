using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Invaders
{
    class PlayerShip : PictureBox
    {
        private Timer animationTimer = new Timer();
        private int Width = 54;
        private int Height = 33;
        private Form mainForm;
        private int step = 5;
        

        public PlayerShip(Form mainForm)
        {
            animationTimer.Tick += new EventHandler(animationTimer_Tick);
            animationTimer.Interval = 10;
            animationTimer.Start();
            BackColor = System.Drawing.Color.Transparent;
            BackgroundImageLayout = ImageLayout.None;
            
            this.mainForm = mainForm;
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            BackgroundImage = Properties.Resources.player;
        }

        public void Move(Direction direction)
        {
            if (direction == Direction.Right)
            {
                Location = new Point(Location.X + step, this.mainForm.Height - 54);
            }
            else
            {
                Location = new Point(Location.X - step, this.mainForm.Height - 54);
            }
        }
    }
}
