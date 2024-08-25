using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tron
{
    public partial class Form1 : Form
    {
        private Player jugador;
        private Grid grid;
        private const int Movimiento = 20;

        public Form1()
        {
            InitializeComponent();
            this.jugador = new Player(20, 20, 20, 20);
            this.grid = new Grid(96, 54);
            this.DoubleBuffered = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            int newX = jugador.X;
            int newY = jugador.Y;

            switch (e.KeyCode)
            {
                case Keys.Up:
                    newY -= Movimiento;
                    break;
                case Keys.Down:
                    newY += Movimiento;
                    break;
                case Keys.Left:
                    newX -= Movimiento;
                    break;
                case Keys.Right:
                    newX += Movimiento;
                    break;
            }

            
            if (newX >= 0 && newX < this.ClientSize.Width - jugador.Width &&
                newY >= 0 && newY < this.ClientSize.Height - jugador.Height)
            {
                jugador.Mover(newX - jugador.X, newY - jugador.Y);
            }

            this.Invalidate(); 
        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            grid.Draw(e.Graphics, jugador);
        }
    }
}

