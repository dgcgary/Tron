using System;
using System.Collections.Generic;

namespace Tron
{
    
    /// Representa un bot en el juego Tron.    
    public class Bot
    {        
        /// Nodo que representa la cabeza del bot.        
        public Nodo Cabeza { get; private set; }

        
        /// Velocidad del bot.        
        public int Velocidad { get; set; }

        
        /// Dirección actual del movimiento del bot.        
        private Direccion direccion;

        
        /// Generador de números aleatorios para cambiar la dirección del bot.        
        private Random random;

        
        /// Cola que almacena el historial de posiciones del bot.        
        private Queue<Nodo> historialPosiciones;

        
        /// Tamaño máximo de la estela del bot.        
        private const int maxEstela = 3;

        
        /// Color de la cabeza del bot.        
        public Color ColorCabeza { get; private set; }

        
        /// Color de la estela del bot.        
        public Color ColorEstela { get; private set; }

        
        /// Verifica si el bot colisiona con un nodo dado.        
        /// name="nodo" Nodo con el que se verifica la colisión.
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

        
        /// Constructor para inicializar un nuevo bot.        
        ///  name="x" Coordenada X inicial del bot.
        ///  name="y" Coordenada Y inicial del bot.
        ///  name="width" Ancho del bot.
        ///  name="height" Altura del bot.
        ///  name="velocidad" Velocidad inicial del bot.
        ///  name="colorCabeza" Color de la cabeza del bot.
        ///  name="colorEstela" Color de la estela del bot.
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

        
        /// Mueve el bot en una dirección aleatoria dentro de los límites de la cuadrícula.
        ///  name="gridWidth" Ancho de la cuadrícula.
        ///  name="gridHeight" Altura de la cuadrícula.
        ///  name="interval" Intervalo de tiempo para el movimiento.
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

        
        /// Cambia la dirección del bot a una dirección aleatoria.      
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

        
        /// Obtiene la estela del bot.        
        public IEnumerable<Nodo> GetEstela()
        {
            return historialPosiciones;
        }

        
        /// Aumenta la estela del bot en un valor dado.        
        ///  name="valor" Valor en el que se aumenta la estela.
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

