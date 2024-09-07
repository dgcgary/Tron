using System.Collections.Generic;

namespace Tron
{
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
        private int invincibilityTime;
        public bool IsHyperSpeed { get; private set; }
        private int hyperSpeedTime;
        public int originalSpeed;
        public int currentSpeed;
        private System.Windows.Forms.Timer gameTimer;
        public Stack<PowerUp> PowerUpStack { get; private set; }




        public Player(int x, int y, int width, int height, int velocidad, System.Windows.Forms.Timer timer)
        {
            this.Cabeza = new Nodo(x, y);
            Velocidad = velocidad;
            this.direccion = Direccion.Derecha;
            this.Combustible = maxCombustible;
            this.historialPosiciones = new Queue<Nodo>();
            this.IsInvincible = false;
            this.invincibilityTime = 0;
            this.originalSpeed = velocidad;
            this.currentSpeed = originalSpeed;
            this.gameTimer = timer;
            this.PowerUpStack = new Stack<PowerUp>();
        }
    

        public bool EsInvulnerable()
        {
            return IsInvincible;
        }

        public void ActivateShield()
        {
            IsInvincible = true;
            invincibilityTime = 100; // Duración de la invulnerabilidad
        }

        public void ActivateHyperSpeed()
        {
            currentSpeed *= 2; // Suponiendo un multiplicador de velocidad de 2
            gameTimer.Interval = 50; // Ajustar el intervalo del Timer
                                     // Establecer un temporizador para restaurar la velocidad original después de que expire el power-up
            var restoreSpeedTimer = new System.Windows.Forms.Timer();
            restoreSpeedTimer.Interval = 500; // Duración en milisegundos
            restoreSpeedTimer.Tick += (sender, e) =>
            {
                currentSpeed = originalSpeed;
                gameTimer.Interval = 100; // Restaurar el intervalo original del Timer
                restoreSpeedTimer.Stop();
            };
            restoreSpeedTimer.Start();
        }


        public void Update()
        {
            if (IsInvincible)
            {
                invincibilityTime--;
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
                    gameTimer.Interval = 100;
                }
            }
        }


        public bool ColisionaCon(Nodo nodo)
        {
            return Cabeza.X == nodo.X && Cabeza.Y == nodo.Y;
        }

        public void CambiarDireccion(Direccion nuevaDireccion)
        {
            this.direccion = nuevaDireccion;
        }

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

        public void RecargarCombustible(double cantidad)
        {
            Combustible += cantidad;
            if (Combustible > maxCombustible)
            {
                Combustible = maxCombustible;
            }
        }

        public void ReducirCombustible(double cantidad)
        {
            Combustible -= cantidad;
            if (Combustible < 0)
            {
                Combustible = 0;
            }
        }

        public void Crecer()
        {
            AumentarEstela(1); // Aumentar la estela en 1
        }

        public IEnumerable<Nodo> ObtenerHistorialPosiciones()
        {
            return historialPosiciones;
        }

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
