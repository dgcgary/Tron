using System;
using System.Collections.Generic;

namespace Tron
{
    public class Bot
    {
        public Nodo Cabeza { get; private set; }
        private int velocidad;
        private Direccion direccion;
        private Random random;
        private Queue<Nodo> historialPosiciones;
        private const int maxEstela = 3;
        public bool ColisionaCon(Nodo nodo)
{
    return Cabeza.X == nodo.X && Cabeza.Y == nodo.Y;
}

        public Bot(int x, int y, int width, int height, int velocidad)
        {
            this.Cabeza = new Nodo(x, y);
            this.velocidad = velocidad;
            this.direccion = Direccion.Derecha;
            this.random = new Random();
            this.historialPosiciones = new Queue<Nodo>();
        }

        public void MoverAleatorio(int gridWidth, int gridHeight, int intervalo)
        {
            // Guardar la posición actual en el historial
            historialPosiciones.Enqueue(new Nodo(Cabeza.X, Cabeza.Y));

            // Limitar el tamaño del historial
            if (historialPosiciones.Count > maxEstela)
            {
                historialPosiciones.Dequeue();
            }

            // Mover la cabeza del bot
            switch (direccion)
            {
                case Direccion.Arriba:
                    if (Cabeza.Y > 0) Cabeza.Y -= 1;
                    break;
                case Direccion.Abajo:
                    if (Cabeza.Y < gridHeight - 1) Cabeza.Y += 1;
                    break;
                case Direccion.Izquierda:
                    if (Cabeza.X > 0) Cabeza.X -= 1;
                    break;
                case Direccion.Derecha:
                    if (Cabeza.X < gridWidth - 1) Cabeza.X += 1;
                    break;
            }
        }

        public void CambiarDireccionAleatoria()
        {
            switch (random.Next(4))
            {
                case 0:
                    direccion = Direccion.Arriba;
                    break;
                case 1:
                    direccion = Direccion.Abajo;
                    break;
                case 2:
                    direccion = Direccion.Izquierda;
                    break;
                case 3:
                    direccion = Direccion.Derecha;
                    break;
            }
        }

        public IEnumerable<Nodo> GetEstela()
        {
            return historialPosiciones;
        }
    }
}
