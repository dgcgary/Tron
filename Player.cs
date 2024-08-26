using System.Collections.Generic;

namespace Tron
{
    public enum Direccion
    {
        Arriba,
        Abajo,
        Izquierda,
        Derecha
    }

    public class Player
    {
        public Nodo Cabeza { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Velocidad { get; private set; }
        public Direccion DireccionActual { get; private set; }
        private Queue<Nodo> estela; // Cola para manejar la estela
        public const int LongitudEstela = 9; // Longitud de la estela

        public Player(int x, int y, int width, int height, int velocidad)
        {
            this.Cabeza = new Nodo(x, y);
            this.Width = width;
            this.Height = height;
            this.Velocidad = velocidad;
            this.DireccionActual = Direccion.Derecha; // Dirección inicial
            this.estela = new Queue<Nodo>();
        }

        public void CambiarDireccion(Direccion nuevaDireccion)
        {
            this.DireccionActual = nuevaDireccion;
        }

        public void Mover()
        {
            int nuevoX = Cabeza.X;
            int nuevoY = Cabeza.Y;

            switch (DireccionActual)
            {
                case Direccion.Arriba:
                    nuevoY -= 1;
                    break;
                case Direccion.Abajo:
                    nuevoY += 1;
                    break;
                case Direccion.Izquierda:
                    nuevoX -= 1;
                    break;
                case Direccion.Derecha:
                    nuevoX += 1;
                    break;
            }

            Nodo nuevoNodo = new Nodo(nuevoX, nuevoY);
            nuevoNodo.Siguiente = Cabeza;
            Cabeza = nuevoNodo;

            estela.Enqueue(nuevoNodo);
            if (estela.Count > LongitudEstela)
            {
                estela.Dequeue();
            }
        }

        public IEnumerable<Nodo> GetEstela()
        {
            return estela;
        }
    }
}
