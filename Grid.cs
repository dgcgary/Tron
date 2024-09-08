namespace Tron
{
    public class Grid
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int TileSize { get; private set; }
        public Nodo[,] Nodos { get; private set; }

        public Grid(int width, int height, int tileSize)
        {
            this.Width = width;
            this.Height = height;
            this.TileSize = tileSize;
            Nodos = new Nodo[width, height];
            CrearCuadricula();
        }

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
