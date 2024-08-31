using System.Collections.Generic;

namespace Tron
{
    public class Player
    {
        public Nodo Cabeza { get; private set; }
        public double Combustible { get; set; }
        public double CombustibleMaximo => maxCombustible; // Agregar esta propiedad
        private int velocidad;
        private Direccion direccion;
        private Queue<Nodo> historialPosiciones;
        private const int maxEstela = 3;
        private const double maxCombustible = 100; // Máximo combustible permitido

        public bool ColisionaCon(Nodo nodo)
        {
            return Cabeza.X == nodo.X && Cabeza.Y == nodo.Y;
        }

        public Player(int x, int y, int width, int height, int velocidad)
        {
            this.Cabeza = new Nodo(x, y);
            this.velocidad = velocidad;
            this.direccion = Direccion.Derecha;
            this.Combustible = maxCombustible;
            this.historialPosiciones = new Queue<Nodo>();
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

            // Mover la cabeza del jugador
            switch (direccion)
            {
                case Direccion.Arriba:
                    if (Cabeza.Y > 0) Cabeza.Y -= 1;
                    else return true;
                    break;
                case Direccion.Abajo:
                    if (Cabeza.Y < gridHeight - 1) Cabeza.Y += 1;
                    else return true;
                    break;
                case Direccion.Izquierda:
                    if (Cabeza.X > 0) Cabeza.X -= 1;
                    else return true;
                    break;
                case Direccion.Derecha:
                    if (Cabeza.X < gridWidth - 1) Cabeza.X += 1;
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
