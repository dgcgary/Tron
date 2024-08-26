using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Tron
{
    public partial class Form1 : Form
    {
        private Player jugador;
        private List<Bot> bots;
        private Grid grid;
        private Random random;
        private System.Windows.Forms.Timer timer; // Especifica el espacio de nombres correcto

        public Form1()
        {
            InitializeComponent();
            random = new Random();
            int velocidad = random.Next(1, 11); // Velocidad aleatoria entre 1 y 10
            int tileSize = 50; // Define el tamaño de los cuadros
            int gridWidth = 1920 / tileSize;
            int gridHeight = 1080 / tileSize;
            this.jugador = new Player(gridWidth / 2, gridHeight / 2, tileSize, tileSize, velocidad);
            this.grid = new Grid(gridWidth, gridHeight, tileSize); // Ajusta el tamaño de la cuadrícula según sea necesario
            this.DoubleBuffered = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.Paint += new PaintEventHandler(Form1_Paint);

            // Crear bots
            bots = new List<Bot>();
            for (int i = 0; i < 4; i++)
            {
                int botVelocidad = random.Next(1, 11);
                Bot bot = new Bot(random.Next(0, gridWidth - 1), random.Next(0, gridHeight - 1), tileSize, tileSize, botVelocidad);
                bots.Add(bot);
            }

            // Configurar el Timer
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 40; // Ajusta el intervalo según sea necesario
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();

            // Establecer el color de fondo 
            this.BackColor = Color.Black;
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
            grid.DrawGrid(e.Graphics);
            grid.DrawPlayer(e.Graphics, jugador);
            foreach (var bot in bots)
            {
                grid.DrawBot(e.Graphics, bot);
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            jugador.Mover();
            foreach (var bot in bots)
            {
                bot.MoverAleatorio(this.ClientSize.Width, this.ClientSize.Height, timer.Interval);
            }

            // Verificar límites del formulario para el jugador
            if (jugador.Cabeza.X < 0 || jugador.Cabeza.X >= this.ClientSize.Width / grid.TileSize ||
                jugador.Cabeza.Y < 0 || jugador.Cabeza.Y >= this.ClientSize.Height / grid.TileSize)
            {
                // Detener el juego o manejar la colisión
                timer.Stop();
                MessageBox.Show("El jugador se ha estrellado!");
            }

            this.Invalidate(); // Redibuja el formulario
        }
    }
}
