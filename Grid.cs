namespace Tron
{
   
    /// Representa la cuadrícula del juego Tron. 
    public class Grid
    {     
        /// Ancho de la cuadrícula en número de nodos.      
        public int Width { get; private set; }
      
        /// Altura de la cuadrícula en número de nodos.      
        public int Height { get; private set; }
       
        /// Tamaño de cada celda de la cuadrícula en píxeles.       
        public int TileSize { get; private set; }
       
        /// Matriz bidimensional que contiene los nodos de la cuadrícula.       
        public Nodo[,] Nodos { get; private set; }

       
        /// Constructor para inicializar la cuadrícula con el ancho, altura y tamaño de celda especificados.     
        ///  name="width" Ancho de la cuadrícula en número de nodos.
        ///  name="height" Altura de la cuadrícula en número de nodos.
        ///  name="tileSize" Tamaño de cada celda de la cuadrícula en píxeles.
        public Grid(int width, int height, int tileSize)
        {
            this.Width = width;
            this.Height = height;
            this.TileSize = tileSize;
            Nodos = new Nodo[width, height];
            CrearCuadricula();
        }

       
        /// Crea la cuadrícula inicializando los nodos y estableciendo sus referencias.      
        private void CrearCuadricula()
        {
            // Crear nodos
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Nodos[x, y] = new Nodo(x, y);
                }
            }

            // Establecer referencias
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (y > 0) Nodos[x, y].Arriba = Nodos[x, y - 1];
                    if (y < Height - 1) Nodos[x, y].Abajo = Nodos[x, y + 1];
                    if (x > 0) Nodos[x, y].Izquierda = Nodos[x - 1, y];
                    if (x < Width - 1) Nodos[x, y].Derecha = Nodos[x + 1, y];
                }
            }
        }

       
        /// Dibuja la cuadrícula en el objeto Graphics proporcionado.     
        ///  name="g" Objeto Graphics donde se dibujará la cuadrícula.
        public void DrawGrid(Graphics g)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    g.DrawRectangle(Pens.Gray, x * TileSize, y * TileSize, TileSize, TileSize);
                }
            }
        }

       
        /// Dibuja al jugador en la cuadrícula.     
        ///  name="g" Objeto Graphics donde se dibujará el jugador.
        ///  name="player" Jugador que se dibujará.
        public void DrawPlayer(Graphics g, Player player)
        {
            // Dibujar la cabeza del jugador con la imagen actual
            g.DrawImage(player.CurrentImage, player.Cabeza.X * TileSize, player.Cabeza.Y * TileSize, TileSize, TileSize);

            // Dibujar la estela del jugador
            foreach (var posicion in player.ObtenerHistorialPosiciones())
            {
                g.FillRectangle(Brushes.Blue, posicion.X * TileSize, posicion.Y * TileSize, TileSize, TileSize);
            }
        }
       
        /// Dibuja al bot en la cuadrícula.      
        ///  name="g" Objeto Graphics donde se dibujará el bot.
        ///  name="bot" Bot que se dibujará.
        public void DrawBot(Graphics g, Bot bot)
        {
            // Dibujar la cabeza del bot con su color específico
            using (Brush brushCabeza = new SolidBrush(bot.ColorCabeza))
            {
                g.FillRectangle(brushCabeza, bot.Cabeza.X * TileSize, bot.Cabeza.Y * TileSize, TileSize, TileSize);
            }

            // Dibujar la estela del bot con su color específico
            using (Brush brushEstela = new SolidBrush(bot.ColorEstela))
            {
                foreach (var posicion in bot.GetEstela())
                {
                    g.FillRectangle(brushEstela, posicion.X * TileSize, posicion.Y * TileSize, TileSize, TileSize);
                }
            }
        }
    }
}

