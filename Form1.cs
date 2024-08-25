using System;
using System.Drawing;
using System.Windows.Forms;


namespace Tron
{
    public partial class Form1 : Form
    {
        private Grid grid;
        private Player player;

        public Form1()
        {
            InitializeComponent();
            grid = new Grid(96, 52); // Adjusted grid size to fit the form
            player = new Player(grid.GetStartNode()); // Initialize player
            this.BackColor = Color.Black;
            this.WindowState = FormWindowState.Maximized;
            this.KeyDown += Form1_KeyDown;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            grid.Draw(e.Graphics, player); // Pass player to Draw method
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    player.MoveUp(grid);
                    Invalidate(new Rectangle(player.X, player.Y, 20, 20)); // Invalidar solo la región del jugador
                    break;
                case Keys.Down:
                    player.MoveDown(grid);
                    Invalidate(new Rectangle(player.X, player.Y, 20, 20)); // Invalidar solo la región del jugador
                    break;
                case Keys.Left:
                    player.MoveLeft(grid);
                    Invalidate(new Rectangle(player.X, player.Y, 20, 20)); // Invalidar solo la región del jugador
                    break;
                case Keys.Right:
                    player.MoveRight(grid);
                    Invalidate(new Rectangle(player.X, player.Y, 20, 20)); // Invalidar solo la región del jugador
                    break;
            }
        }
    }
}