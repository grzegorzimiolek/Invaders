using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Invaders
{
    class InvaderControlTest : PictureBox
    {
        private Timer animationTimer = new Timer();
        private int Width = 45;
        private int Height = 51;
        private Direction direction = Direction.Right;
        private Form mainForm;
        private int step = 5;

        public InvaderControlTest(Form form)
        {
            animationTimer.Tick += new EventHandler(animationTimer_Tick);
            animationTimer.Interval = 50;
            animationTimer.Start();
            BackColor = System.Drawing.Color.Transparent;
            BackgroundImageLayout = ImageLayout.None;

            this.mainForm = form;

            ResizeCells();
        }

        private int cell = 0;
        public void animationTimer_Tick(object sender, EventArgs e)
        {
            MoveBug();
            cell++;
            switch(cell)
            {
                case 1: BackgroundImage = cells[0]; break;
                case 2: BackgroundImage = cells[1]; break;
                case 3: BackgroundImage = cells[2]; break;
                case 4: BackgroundImage = cells[3]; break;
                case 5: BackgroundImage = cells[2]; break;
                default:
                    BackgroundImage = cells[0];
                    cell = 0;
                    break;
            }
        }

        private Bitmap[] cells = new Bitmap[4];
        private void ResizeCells()
        {
            cells[0] = Game.ResizeImage(Properties.Resources.bug1, Width, Height);
            cells[1] = Game.ResizeImage(Properties.Resources.bug2, Width, Height);
            cells[2] = Game.ResizeImage(Properties.Resources.bug3, Width, Height);
            cells[3] = Game.ResizeImage(Properties.Resources.bug4, Width, Height);
        }

        private void MoveBug()
        {
            if (Location.X <= this.mainForm.Width && this.direction == Direction.Right)
            {
                this.direction = Direction.Right;
                if (Location.X == this.mainForm.Width - (this.Width + this.step))
                {
                    this.direction = Direction.Left;
                }
                else
                {
                    Location = new Point(Location.X + this.step, 0);
                }
                
                if (Location.X == this.mainForm.Width)
                {
                    
                }
            }

            if (Location.X >= 0 && this.direction == Direction.Left)
            {
                this.direction = Direction.Left;
                Location = new Point(Location.X - this.step, 0);
                if (Location.X == 0)
                {
                    this.direction = Direction.Right;
                }
            }
   
        }
    }
}
