using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tron
{
    public partial class Form1 : Form
    {
        private Player jugador;
        private Grid grid;
        private Random random;
        private System.Windows.Forms.Timer timer; // Especifica el espacio de nombres correcto

        public Form1()
        {
            InitializeComponent();
            random = new Random();
            int velocidad = random.Next(1, 11); // Velocidad aleatoria entre 1 y 10
            this.jugador = new Player(20, 20, 20, 20, velocidad);
            this.grid = new Grid(96, 54); // Ajusta el tamaño de la cuadrícula según sea necesario
            this.DoubleBuffered = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.Paint += new PaintEventHandler(Form1_Paint);

            // Configurar el Timer
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1; // Ajusta el intervalo según sea necesario
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    jugador.CambiarDireccion(Direccion.Arriba);
                    break;
                case Keys.Down:
                    jugador.CambiarDireccion(Direccion.Abajo);
                    break;
                case Keys.Left:
                    jugador.CambiarDireccion(Direccion.Izquierda);
                    break;
                case Keys.Right:
                    jugador.CambiarDireccion(Direccion.Derecha);
                    break;
            }
        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            grid.Draw(e.Graphics, jugador);
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            jugador.Mover();
            // Verificar límites del formulario
            if (jugador.X < 0 || jugador.X >= this.ClientSize.Width - jugador.Width ||
                jugador.Y < 0 || jugador.Y >= this.ClientSize.Height - jugador.Height)
            {
                // Detener el juego o manejar la colisión
                timer.Stop();
                MessageBox.Show("El jugador se ha salido de los límites!");
            }
            this.Invalidate(); // Redibuja el formulario
        }
    }
}

