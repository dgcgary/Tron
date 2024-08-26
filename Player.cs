using System.Drawing;

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
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Velocidad { get; private set; }
        public Direccion DireccionActual { get; private set; }

        public Player(int x, int y, int width, int height, int velocidad)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Velocidad = velocidad;
            this.DireccionActual = Direccion.Derecha; // Dirección inicial
        }

        public void CambiarDireccion(Direccion nuevaDireccion)
        {
            this.DireccionActual = nuevaDireccion;
        }

        public void Mover()
        {
            switch (DireccionActual)
            {
                case Direccion.Arriba:
                    this.Y -= Velocidad;
                    break;
                case Direccion.Abajo:
                    this.Y += Velocidad;
                    break;
                case Direccion.Izquierda:
                    this.X -= Velocidad;
                    break;
                case Direccion.Derecha:
                    this.X += Velocidad;
                    break;
            }
        }
    }
}

