using System.Drawing;

namespace Tron
{
    public class Grid
    {
        public int Width { get; }
        public int Height { get; }
        public int TileSize { get; }
        public List<Nodo> Estelas { get; } 


        public Grid(int width, int height, int tileSize)
        {
            this.Width = width;
            this.Height = height;
            this.TileSize = tileSize;
            Estelas = new List<Nodo>(); 
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
            // Dibujar la cabeza del jugador
            g.FillRectangle(Brushes.Red, player.Cabeza.X * TileSize, player.Cabeza.Y * TileSize, TileSize, TileSize);

            // Dibujar la estela del jugador
            foreach (var posicion in player.ObtenerHistorialPosiciones())
            {
                g.FillRectangle(Brushes.Blue, posicion.X * TileSize, posicion.Y * TileSize, TileSize, TileSize);
            }
        }

        public void DrawBot(Graphics g, Bot bot)
        {
            // Dibujar la cabeza del bot
            g.FillRectangle(Brushes.Green, bot.Cabeza.X * TileSize, bot.Cabeza.Y * TileSize, TileSize, TileSize);

            // Dibujar la estela del bot
            foreach (var posicion in bot.GetEstela())
            {
                g.FillRectangle(Brushes.Yellow, posicion.X * TileSize, posicion.Y * TileSize, TileSize, TileSize);
            }
        }
    }
}
