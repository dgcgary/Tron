using System.Collections.Generic;
using System.Drawing;

namespace Tron
{
    /// Representa al jugador en el juego.
    public class Player
    {
        public Nodo Cabeza { get; private set; }
        public double Combustible { get; set; }      
        public double CombustibleMaximo => maxCombustible;
        public int Velocidad { get; set; }
        private Direccion direccion;
        private Queue<Nodo> historialPosiciones;
        private const int maxEstela = 3;
        private const double maxCombustible = 100;
        public bool IsInvincible { get; private set; }
        public int invincibilityTime;
        public bool IsHyperSpeed { get; private set; }
        private int hyperSpeedTime;
        public int originalSpeed;
        public int currentSpeed;
        private System.Windows.Forms.Timer gameTimer;
        public Stack<PowerUp> PowerUpStack { get; private set; }      
        public Image CurrentImage { get; set; }
        private Image defaultImage;
        private Image shieldImage;
        private Image hyperSpeedImage;
        public System.Windows.Forms.Timer powerUpTimer;
        private Random random;
                
        /// Constructor para inicializar un nuevo jugador.
        // name="x">Coordenada X inicial del jugador.
        // name="y">Coordenada Y inicial del jugador.
        // name="width">Ancho del jugador.
        // name="height">Altura del jugador.
        // name="velocidad">Velocidad inicial del jugador.
        // name="timer">Temporizador del juego.
        public Player(int x, int y, int width, int height, int velocidad, System.Windows.Forms.Timer timer)
        {
            this.Cabeza = new Nodo(x, y);
            Velocidad = velocidad;
            this.direccion = Direccion.Derecha;
            this.Combustible = maxCombustible;
            this.historialPosiciones = new Queue<Nodo>();
            this.IsInvincible = false;
            this.invincibilityTime = invincibilityTime;
            this.originalSpeed = velocidad;
            this.currentSpeed = originalSpeed;
            this.gameTimer = timer;
            this.PowerUpStack = new Stack<PowerUp>();
            defaultImage = Image.FromFile("moto.png"); // Imagen por defecto de la moto
            shieldImage = Image.FromFile("shieldActive.png"); // Imagen del escudo
            hyperSpeedImage = Image.FromFile("rayov.png"); // Imagen del rayo
            CurrentImage = defaultImage;
            random = new Random();

            // Inicializar el temporizador para los poderes
            powerUpTimer = new System.Windows.Forms.Timer();
            powerUpTimer.Interval = random.Next(10, 15) * 1000;
            powerUpTimer.Tick += PowerUpTimer_Tick;
        }

        private void PowerUpTimer_Tick(object sender, EventArgs e)
        {
            // Restaurar la imagen por defecto cuando el poder se acabe
            CurrentImage = defaultImage;
            powerUpTimer.Stop();
        }
                
        /// Verifica si el jugador es invulnerable.
        public bool EsInvulnerable()
        {
            return IsInvincible;
        }
                
        /// Activa el escudo de invulnerabilidad del jugador.       
        public void ActivateShield()
        {
            IsInvincible = true;
            invincibilityTime = powerUpTimer.Interval; // Duración de la invulnerabilidad
            CurrentImage = shieldImage;
            powerUpTimer.Start();
        }
                
        /// Activa la hipervelocidad del jugador.      
        public void ActivateHyperSpeed()
        {
            CurrentImage = hyperSpeedImage;
            powerUpTimer.Start();
            currentSpeed += random.Next(3, 8);
            Velocidad = currentSpeed;
            gameTimer.Interval = 50; // Ajustar el intervalo del Timer

            // Establecer un temporizador para restaurar la velocidad original después de que expire el power-up
            var restoreSpeedTimer = new System.Windows.Forms.Timer();
            restoreSpeedTimer.Interval = powerUpTimer.Interval;
            restoreSpeedTimer.Tick += (sender, e) =>
            {
                currentSpeed = originalSpeed;
                Velocidad = originalSpeed; // Restaurar la velocidad original
                gameTimer.Interval = 100; // Restaurar el intervalo original del Timer
                restoreSpeedTimer.Stop();
            };
            restoreSpeedTimer.Start();
        }

        /// Actualiza el estado del jugador.      
        public void Update()
        {
            if (IsInvincible)
            {
                invincibilityTime -= gameTimer.Interval; // Restar el intervalo del gameTimer
                if (invincibilityTime <= 0)
                {
                    IsInvincible = false;
                }
            }

            if (IsHyperSpeed)
            {
                hyperSpeedTime--;
                if (hyperSpeedTime <= 0)
                {
                    IsHyperSpeed = false;
                    currentSpeed = originalSpeed;
                    Velocidad = originalSpeed; // Restaurar la velocidad original
                    gameTimer.Interval = 100;
                }
            }
        }
                
        /// Verifica si el jugador colisiona con un nodo dado.        
        /// name="nodo">Nodo con el que se verifica la colisión.
        public bool ColisionaCon(Nodo nodo)
        {
            return Cabeza.X == nodo.X && Cabeza.Y == nodo.Y;
        }
                
        /// Cambia la dirección del jugador.   
        /// name="nuevaDireccion">Nueva dirección del jugador.
        public void CambiarDireccion(Direccion nuevaDireccion)
        {
            this.direccion = nuevaDireccion;
        }
                
        /// Mueve al jugador en la cuadrícula.    
        /// name="gridWidth">Ancho de la cuadrícula.
        /// name="gridHeight">Altura de la cuadrícula.
          public bool Mover(int gridWidth, int gridHeight)
        {
            // Guardar la posición actual en el historial
            historialPosiciones.Enqueue(new Nodo(Cabeza.X, Cabeza.Y));

            // Limitar el tamaño del historial
            if (historialPosiciones.Count > maxEstela)
            {
                historialPosiciones.Dequeue();
            }

            // Mover la cabeza del jugador en relación con su velocidad
            int movimiento = Math.Min(currentSpeed, 1); // Limitar el movimiento a 1 píxel por tick

            switch (direccion)
            {
                case Direccion.Arriba:
                    if (Cabeza.Y - movimiento >= 0) Cabeza.Y -= movimiento;
                    else return true;
                    break;
                case Direccion.Abajo:
                    if (Cabeza.Y + movimiento < gridHeight) Cabeza.Y += movimiento;
                    else return true;
                    break;
                case Direccion.Izquierda:
                    if (Cabeza.X - movimiento >= 0) Cabeza.X -= movimiento;
                    else return true;
                    break;
                case Direccion.Derecha:
                    if (Cabeza.X + movimiento < gridWidth) Cabeza.X += movimiento;
                    else return true;
                    break;
            }

            return false;
        }
                
        /// Recarga el combustible del jugador.      
        /// name="cantidad">Cantidad de combustible a recargar.
        public void RecargarCombustible(double cantidad)
        {
            Combustible += cantidad;
            if (Combustible > maxCombustible)
            {
                Combustible = maxCombustible;
            }
        }
        
        /// Reduce el combustible del jugador.       
        /// name="cantidad">Cantidad de combustible a reducir.
        public void ReducirCombustible(double cantidad)
        {
            Combustible -= cantidad;
            if (Combustible < 0)
            {
                Combustible = 0;
            }
        }
                
        /// Aumenta la estela del jugador.      
        public void Crecer()
        {
            AumentarEstela(1); // Aumentar la estela en 1
        }
                
        /// Obtiene el historial de posiciones del jugador.        
        public IEnumerable<Nodo> ObtenerHistorialPosiciones()
        {
            return historialPosiciones;
        }
                
        /// Aumenta la estela del jugador en un valor dado.      
        /// name="valor">Valor en el que se aumenta la estela.
        public void AumentarEstela(int valor)
        {
            if (historialPosiciones.Count > 0)
            {
                Nodo ultimaPosicion = historialPosiciones.ToArray()[historialPosiciones.Count - 1];
                for (int i = 0; i < valor; i++)
                {
                    historialPosiciones.Enqueue(new Nodo(ultimaPosicion.X, ultimaPosicion.Y));
                }
            }
        }
    }
}
