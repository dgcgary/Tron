using System;
using System.Collections.Generic;

namespace Tron
{
    public class Bot
    {
        public Nodo Cabeza { get; private set; }
        public int Velocidad { get; set; }
        private Direccion direccion;
        private Random random;
        private Queue<Nodo> historialPosiciones;
        private const int maxEstela = 3;
        public Color ColorCabeza { get; private set; }
        public Color ColorEstela { get; private set; }


        public bool ColisionaCon(Nodo nodo)
        {
            foreach (var posicion in historialPosiciones)
            {
                if (posicion.X == nodo.X && posicion.Y == nodo.Y)
                {
                    return true;
                }
            }
            return false;
        }

        public Bot(int x, int y, int width, int height, int velocidad, Color colorCabeza, Color colorEstela)

        {
            this.Cabeza = new Nodo(x, y);
            Velocidad = velocidad;
            this.direccion = Direccion.Derecha;
            this.random = new Random();
            this.historialPosiciones = new Queue<Nodo>();
            this.ColorCabeza = colorCabeza;
            this.ColorEstela = colorEstela;

        }

        public void MoverAleatorio(int gridWidth, int gridHeight, int interval)
        {
            // Guardar la posición actual en el historial
            historialPosiciones.Enqueue(new Nodo(Cabeza.X, Cabeza.Y));

            // Limitar el tamaño del historial
            if (historialPosiciones.Count > maxEstela)
            {
                historialPosiciones.Dequeue();
            }

            // Mover la cabeza del bot en relación con su velocidad
            int movimiento = Math.Min(Velocidad, 1); // Limitar el movimiento a 1 píxel por tick

            switch (direccion)
            {
                case Direccion.Arriba:
                    if (Cabeza.Y - movimiento >= 0) Cabeza.Y -= movimiento;
                    break;
                case Direccion.Abajo:
                    if (Cabeza.Y + movimiento < gridHeight) Cabeza.Y += movimiento;
                    break;
                case Direccion.Izquierda:
                    if (Cabeza.X - movimiento >= 0) Cabeza.X -= movimiento;
                    break;
                case Direccion.Derecha:
                    if (Cabeza.X + movimiento < gridWidth) Cabeza.X += movimiento;
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
