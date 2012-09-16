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
            game = new Game(this);
            game.DrawBug();
            game.DrawSpaceShip();
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            Direction direction = (e.KeyCode == Keys.Right) ? Direction.Right : Direction.Left;
            game.MoveSpaceShip(direction);
            Console.WriteLine(e.KeyCode);
        }
    }
}
