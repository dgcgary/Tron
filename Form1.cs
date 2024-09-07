using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Tron
{
    public partial class Form1 : Form
    {
        private Player jugador;
        private List<Bot> bots;
        private Grid grid;
        private Random random;
        private System.Windows.Forms.Timer timer; // Especifica el espacio de nombres correcto
        private System.Windows.Forms.Timer botDirectionTimer; // Nuevo temporizador para cambiar la dirección de los bots
        private Label lblCombustible; // Label para mostrar el combustible
        private List<Nodo> celdasCombustible; // Lista de celdas de combustible
        private Image imagenCombustible; // Imagen de la celda de combustible
        private List<Nodo> growItems;
        private Image imagenGrowItem;
        private List<Nodo> bombaItems;
        private Image imagenBombaItem;
        private Queue<Nodo> itemQueue; // Cola para los items
        private System.Windows.Forms.Timer itemTimer; // Timer para aplicar items con delay
        private Image imagenShield;
        private Image imagenHyperSpeed;
        private Label lblVelocidad;
        private List<PowerUp> powerUps;
        private Label lblPowerUpCount;
        private Label lblTopPowerUp;





        public Form1()
        {
            InitializeComponent();
            random = new Random();
            // Inicializar el grid
            int tileSize = 40; // Define el tamaño de los cuadros a 40x40
            int gridWidth = 1920 / tileSize;
            int gridHeight = 1080 / tileSize;
            grid = new Grid(gridWidth, gridHeight, tileSize);

            // Inicializar la lista de celdas de crecimiento de estela
            growItems = new List<Nodo>();
            GenerarGrowItems();
            string rutaImagenGrowItem = System.IO.Path.Combine(Application.StartupPath, "grow.png");
            imagenGrowItem = Image.FromFile(rutaImagenGrowItem);

            // Inicializar la lista de items de bombas
            bombaItems = new List<Nodo>();
            GenerarBombaItems();
            string rutaImagenBombaItem = System.IO.Path.Combine(Application.StartupPath, "bomba.png");
            imagenBombaItem = Image.FromFile(rutaImagenBombaItem);

            // Inicializar la lista de celdas de combustible
            celdasCombustible = new List<Nodo>();
            GenerarCeldasCombustible();
            string rutaImagenCombustible = System.IO.Path.Combine(Application.StartupPath, "gasolina.png");
            imagenCombustible = Image.FromFile(rutaImagenCombustible);

            // Inicializar la cola de items
            itemQueue = new Queue<Nodo>();

            // Configurar el Timer para aplicar items con delay
            itemTimer = new System.Windows.Forms.Timer();
            itemTimer.Interval = 1000; // 1 segundo
            itemTimer.Tick += new EventHandler(ItemTimer_Tick);
            itemTimer.Start();



            // Inicializar el stack de power-ups
            powerUps = new List<PowerUp>();
            GenerarPowerUps();
            string rutaImagenShield = System.IO.Path.Combine(Application.StartupPath, "shield.png");
            imagenShield = Image.FromFile(rutaImagenShield);
            string rutaImagenHyper = System.IO.Path.Combine(Application.StartupPath, "hyper.png");
            imagenHyperSpeed = Image.FromFile(rutaImagenHyper);


            // Crear y configurar el Label para mostrar el conteo de power-ups
            lblPowerUpCount = new Label();
            lblPowerUpCount.Location = new Point(10, 50); // Ajusta la posición según sea necesario
            lblPowerUpCount.Size = new Size(200, 20);
            lblPowerUpCount.Text = "Power-ups: 0";
            lblPowerUpCount.ForeColor = Color.White;
            lblPowerUpCount.BackColor = Color.Black;
            this.Controls.Add(lblPowerUpCount);

            // Crear y configurar el Label para mostrar el power-up en el top del stack
            lblTopPowerUp = new Label();
            lblTopPowerUp.Location = new Point(10, 90); // Ajusta la posición según sea necesario
            lblTopPowerUp.Size = new Size(200, 20);
            lblTopPowerUp.Text = "Top Power-Up: None";
            lblTopPowerUp.ForeColor = Color.White;
            lblTopPowerUp.BackColor = Color.Black;
            this.Controls.Add(lblTopPowerUp);




            this.DoubleBuffered = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.Paint += new PaintEventHandler(Form1_Paint);

            // Crear bots
            bots = new List<Bot>();
            for (int i = 0; i < 5; i++)
            {
                bots.Add(new Bot(random.Next(0, 20), random.Next(0, 20), 20, 20, random.Next(1, 11)));
            }

            // Configurar el Timer para el movimiento
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 100; // Ajusta el intervalo a 100 ms para hacer que las motos se muevan más suavemente
            timer.Tick += Timer_Tick;
            timer.Start();


            // Inicializar el jugador
            jugador = new Player(5, 5, 20, 20, random.Next(1, 11), timer);


            //Inicializar el label para el valor de la velocidad
            lblVelocidad = new Label();
            lblVelocidad.Location = new Point(10, 110);
            lblVelocidad.Size = new Size(200, 20);
            lblVelocidad.ForeColor = Color.White;
            lblVelocidad.BackColor = Color.Black;
            lblVelocidad.Text = $"Velocidad: {jugador.Velocidad}";
            this.Controls.Add(lblVelocidad);


            // Configurar el Timer para cambiar la dirección de los bots
            botDirectionTimer = new System.Windows.Forms.Timer();
            botDirectionTimer.Interval = 500; // Cambiar dirección cada 500 ms
            botDirectionTimer.Tick += new EventHandler(BotDirectionTimer_Tick);
            botDirectionTimer.Start();

            // Establecer el color de fondo 
            this.BackColor = Color.Black;

            // Crear y configurar el Label para mostrar el combustible
            lblCombustible = new Label();
            lblCombustible.AutoSize = true;
            lblCombustible.ForeColor = Color.White;
            lblCombustible.Location = new Point(10, 10);
            lblCombustible.Font = new Font("Arial", 16, FontStyle.Bold);
            this.Controls.Add(lblCombustible);
        }


        private void UpdatePowerUpCount()
        {
            lblPowerUpCount.Text = $"Power-Ups: {jugador.PowerUpStack.Count}";
            if (jugador.PowerUpStack.Count > 0)
            {
                var topPowerUp = jugador.PowerUpStack.Peek();
                lblTopPowerUp.Text = $"Top Power-Up: {topPowerUp.GetType().Name}";
            }
            else
            {
                lblTopPowerUp.Text = "Top Power-Up: None";
            }
        }




        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && jugador.PowerUpStack.Count > 0)
            {
                var powerUp = jugador.PowerUpStack.Pop();
                    if (powerUp is ShieldPowerUp)
                    {
                        jugador.ActivateShield();
                    }
                    else if (powerUp is HyperSpeedPowerUp)
                    {
                        jugador.ActivateHyperSpeed();
                    }
                    UpdatePowerUpCount();
            }
            

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

            // Dibujar celdas de combustible
            foreach (var celda in celdasCombustible)
            {
                e.Graphics.DrawImage(imagenCombustible, celda.X * grid.TileSize, celda.Y * grid.TileSize, grid.TileSize, grid.TileSize);
            }

            // Dibujar items de crecimiento
            foreach (var growItem in growItems)
            {
                e.Graphics.DrawImage(imagenGrowItem, growItem.X * grid.TileSize, growItem.Y * grid.TileSize, grid.TileSize, grid.TileSize);
            }

            // Dibujar items de eliminación
            foreach (var eliminarItem in bombaItems)
            {
                e.Graphics.DrawImage(imagenBombaItem, eliminarItem.X * grid.TileSize, eliminarItem.Y * grid.TileSize, grid.TileSize, grid.TileSize);
            }

            // Dibujar poder de escudo e hipervelocidad
            foreach (var powerUp in powerUps)
            {
                if (powerUp is ShieldPowerUp)
                {
                    e.Graphics.DrawImage(imagenShield, powerUp.X * grid.TileSize, powerUp.Y * grid.TileSize, grid.TileSize, grid.TileSize);
                }
                else if (powerUp is HyperSpeedPowerUp)
                {
                    e.Graphics.DrawImage(imagenHyperSpeed, powerUp.X * grid.TileSize, powerUp.Y * grid.TileSize, grid.TileSize, grid.TileSize);
                }
            }
        }




        private void Timer_Tick(object? sender, EventArgs e)
        {
            // Actualizar el estado del jugador
            jugador.Update();

            // Invalidate las posiciones de la estela del jugador antes de moverlo
            foreach (var posicion in jugador.ObtenerHistorialPosiciones())
            {
                var rectEstela = new Rectangle(posicion.X * grid.TileSize, posicion.Y * grid.TileSize, grid.TileSize, grid.TileSize);
                this.Invalidate(rectEstela);
            }

            // Guardar la posición anterior del jugador
            var posicionAnteriorJugador = new Rectangle(jugador.Cabeza.X * grid.TileSize, jugador.Cabeza.Y * grid.TileSize, grid.TileSize, grid.TileSize);

            bool jugadorEstrellado = jugador.Mover(grid.Width, grid.Height);

            // Invalidate la posición anterior del jugador
            this.Invalidate(posicionAnteriorJugador);

            // Invalidate la nueva posición del jugador
            var nuevaPosicionJugador = new Rectangle(jugador.Cabeza.X * grid.TileSize, jugador.Cabeza.Y * grid.TileSize, grid.TileSize, grid.TileSize);
            this.Invalidate(nuevaPosicionJugador);

            List<Bot> botsAEliminar = new List<Bot>();

            foreach (var bot in bots)
            {
                // Invalidate las posiciones de la estela del bot antes de moverlo
                foreach (var posicion in bot.GetEstela())
                {
                    var rectEstelaBot = new Rectangle(posicion.X * grid.TileSize, posicion.Y * grid.TileSize, grid.TileSize, grid.TileSize);
                    this.Invalidate(rectEstelaBot);
                }

                // Guardar la posición anterior del bot
                var posicionAnteriorBot = new Rectangle(bot.Cabeza.X * grid.TileSize, bot.Cabeza.Y * grid.TileSize, grid.TileSize, grid.TileSize);

                bot.MoverAleatorio(grid.Width, grid.Height, timer.Interval);

                // Invalidate la posición anterior del bot
                this.Invalidate(posicionAnteriorBot);

                // Invalidate la nueva posición del bot
                var nuevaPosicionBot = new Rectangle(bot.Cabeza.X * grid.TileSize, bot.Cabeza.Y * grid.TileSize, grid.TileSize, grid.TileSize);
                this.Invalidate(nuevaPosicionBot);

                // Verificar colisiones entre el jugador y los bots solo si el escudo no está activo
                if (!jugador.EsInvulnerable() && jugador.ColisionaCon(bot.Cabeza))
                {
                    timer.Stop();
                    MessageBox.Show("¡Colisión! Ambos jugadores han muerto.");
                    return;
                }

                // Verificar colisiones con las estelas del jugador solo si el bot toca la estela con la cabeza
                foreach (var posicion in jugador.ObtenerHistorialPosiciones())
                {
                    if (bot.Cabeza.X == posicion.X && bot.Cabeza.Y == posicion.Y)
                    {
                        botsAEliminar.Add(bot);
                        break;
                    }
                }

                // Verificar colisiones con las estelas de otros bots
                foreach (var otroBot in bots)
                {
                    if (bot != otroBot)
                    {
                        foreach (var posicion in otroBot.GetEstela())
                        {
                            if (bot.Cabeza.X == posicion.X && bot.Cabeza.Y == posicion.Y)
                            {
                                botsAEliminar.Add(bot);
                                break;
                            }
                        }
                    }
                }
            }

            // Eliminar los bots que han chocado
            foreach (var bot in botsAEliminar)
            {
                bots.Remove(bot);
            }

            // Verificar colisiones con las estelas de los bots solo si el escudo no está activo
            if (!jugador.EsInvulnerable())
            {
                foreach (var bot in bots)
                {
                    foreach (var posicion in bot.GetEstela())
                    {
                        if (jugador.Cabeza.X == posicion.X && jugador.Cabeza.Y == posicion.Y)
                        {
                            timer.Stop();
                            MessageBox.Show("¡Colisión con la estela! El jugador ha muerto.");
                            return;
                        }
                    }
                }
            }

            // Verificar si el jugador se ha estrellado
            if (jugadorEstrellado && !jugador.EsInvulnerable())
            {
                timer.Stop();
                MessageBox.Show("El jugador se ha estrellado!");
                return;
            }

            // Verificar si el combustible se ha agotado
            if (jugador.Combustible <= 0)
            {
                timer.Stop();
                MessageBox.Show("La moto se quedó sin combustible!");
                return;
            }

            // Verificar si el jugador recoge una celda de combustible
            for (int i = celdasCombustible.Count - 1; i >= 0; i--)
            {
                if (jugador.Cabeza.X == celdasCombustible[i].X && jugador.Cabeza.Y == celdasCombustible[i].Y)
                {
                    double cantidadCombustible = random.Next(10, 21); // Cantidad aleatoria de combustible entre 10 y 20
                    jugador.RecargarCombustible(cantidadCombustible);
                    celdasCombustible.RemoveAt(i);
                }
            }

            // Reducir el combustible del jugador
            jugador.ReducirCombustible(0.1);

            // Actualizar el Label del combustible
            lblCombustible.Text = $"Combustible: {jugador.Combustible:F1}";

            for (int i = growItems.Count - 1; i >= 0; i--)
            {
                if (jugador.Cabeza.X == growItems[i].X && jugador.Cabeza.Y == growItems[i].Y)
                {
                    jugador.AumentarEstela(random.Next(1, 11)); // Valor aleatorio entre 1 y 10
                    growItems.RemoveAt(i);
                }
            }

            // Verificar colisión de bots con ítems de crecimiento
            for (int i = growItems.Count - 1; i >= 0; i--)
            {
                foreach (var bot in bots)
                {
                    if (bot.Cabeza.X == growItems[i].X && bot.Cabeza.Y == growItems[i].Y)
                    {
                        bot.AumentarEstela(random.Next(1, 11)); // Valor aleatorio entre 1 y 10
                        growItems.RemoveAt(i);
                        break; // Salir del bucle de bots para evitar modificar la lista mientras se itera
                    }
                }
            }

            // Verificar si el jugador recoge un power-up
            for (int i = powerUps.Count - 1; i >= 0; i--)
            {
                if (jugador.Cabeza.X == powerUps[i].X && jugador.Cabeza.Y == powerUps[i].Y)
                {
                    jugador.PowerUpStack.Push(powerUps[i]);
                    var powerUpPos = new Rectangle(powerUps[i].X * grid.TileSize, powerUps[i].Y * grid.TileSize, grid.TileSize, grid.TileSize);
                    powerUps.RemoveAt(i);
                    UpdatePowerUpCount();
                    this.Invalidate(powerUpPos);
                }
            }

            // Verificar colisiones con ítems bomba solo si el escudo no está activo
            for (int i = bombaItems.Count - 1; i >= 0; i--)
            {
                if (!jugador.EsInvulnerable() && jugador.Cabeza.X == bombaItems[i].X && jugador.Cabeza.Y == bombaItems[i].Y)
                {
                    timer.Stop();
                    MessageBox.Show("¡El jugador ha explotado!");
                    return;
                }

                foreach (var bot in bots)
                {
                    if (bot.Cabeza.X == bombaItems[i].X && bot.Cabeza.Y == bombaItems[i].Y)
                    {
                        bots.Remove(bot);
                        bombaItems.RemoveAt(i);
                        break;
                    }
                }
            }
        }



        private void BotDirectionTimer_Tick(object? sender, EventArgs e)
        {
            foreach (var bot in bots)
            {
                bot.CambiarDireccionAleatoria();
            }
        }

        private void GenerarCeldasCombustible()
        {
            int cantidadCeldas = random.Next(5, 11); // Generar entre 5 y 10 celdas de combustible
            for (int i = 0; i < cantidadCeldas; i++)
            {
                int x = random.Next(0, grid.Width);
                int y = random.Next(0, grid.Height);
                celdasCombustible.Add(new Nodo(x, y));
            }
        }

        private void GenerarGrowItems()
        {
            int cantidadItems = random.Next(5, 11); // Generar entre 5 y 10 items de crecimiento
            for (int i = 0; i < cantidadItems; i++)
            {
                int x = random.Next(0, grid.Width);
                int y = random.Next(0, grid.Height);
                growItems.Add(new Nodo(x, y));
            }
        }

        private void GenerarBombaItems()
        {
            int cantidadItems = random.Next(5, 11); // Generar entre 5 y 10 items de eliminación
            for (int i = 0; i < cantidadItems; i++)
            {
                int x = random.Next(0, grid.Width);
                int y = random.Next(0, grid.Height);
                bombaItems.Add(new Nodo(x, y));
            }
        }

        private void GenerarPowerUps()
        {
            int cantidadPowerUps = random.Next(10, 21); // Generar entre 10 y 20 power-ups
            for (int i = 0; i < cantidadPowerUps; i++)
            {
                int x = random.Next(0, grid.Width);
                int y = random.Next(0, grid.Height);
                if (random.Next(2) == 0)
                {
                    powerUps.Add(new ShieldPowerUp(100, x, y));
                }
                else
                {
                    powerUps.Add(new HyperSpeedPowerUp(100, 2, x, y));
                }
            }
        }

        private void ItemTimer_Tick(object? sender, EventArgs e)
        {
            if (itemQueue.Count > 0)
            {
                Nodo item = itemQueue.Dequeue();

                // Priorizar celdas de combustible
                if (celdasCombustible.Contains(item))
                {
                    if (jugador.Combustible < jugador.CombustibleMaximo)
                    {
                        jugador.Combustible += random.Next(10, 20); // Incrementar el combustible
                        celdasCombustible.Remove(item);
                    }
                    else
                    {
                        itemQueue.Enqueue(item); // Reinsertar en la cola si el combustible está lleno
                    }
                }
                else if (growItems.Contains(item))
                {
                    jugador.Crecer(); // Aplicar item de crecimiento
                    growItems.Remove(item);
                }
                else if (bombaItems.Contains(item))
                {
                    timer.Stop();
                    MessageBox.Show("El jugador ha tocado una bomba y ha sido eliminado!");
                    return;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}