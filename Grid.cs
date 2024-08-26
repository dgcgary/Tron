using System.Drawing;

namespace Tron
{
    public class Grid
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int TileSize { get; private set; }

        public Grid(int width, int height, int tileSize)
        {
            this.Width = width;
            this.Height = height;
            this.TileSize = tileSize;
        }

        public Point GetTilePosition(int x, int y)
        {
            int tileX = x / TileSize;
            int tileY = y / TileSize;
            return new Point(tileX, tileY);
        }

        public Point GetPixelPosition(int tileX, int tileY)
        {
            int x = tileX * TileSize;
            int y = tileY * TileSize;
            return new Point(x, y);
        }

        public void DrawGrid(Graphics g)
        {
            Pen pen = new Pen(Color.Gray);
            for (int x = 0; x <= Width; x++)
            {
                g.DrawLine(pen, x * TileSize, 0, x * TileSize, Height * TileSize);
            }
            for (int y = 0; y <= Height; y++)
            {
                g.DrawLine(pen, 0, y * TileSize, Width * TileSize, y * TileSize);
            }
        }

        public void DrawPlayer(Graphics g, Player player)
        {
            foreach (var nodo in player.GetEstela())
            {
                Point pos = GetPixelPosition(nodo.X, nodo.Y);
                g.FillRectangle(Brushes.Blue, pos.X, pos.Y, TileSize, TileSize);
            }

            Point cabezaPos = GetPixelPosition(player.Cabeza.X, player.Cabeza.Y);
            g.FillRectangle(Brushes.Red, cabezaPos.X, cabezaPos.Y, TileSize, TileSize);
        }

        public void DrawBot(Graphics g, Bot bot)
        {
            foreach (var nodo in bot.GetEstela())
            {
                Point pos = GetPixelPosition(nodo.X, nodo.Y);
                g.FillRectangle(Brushes.Green, pos.X, pos.Y, TileSize, TileSize);
            }

            Point cabezaPos = GetPixelPosition(bot.Cabeza.X, bot.Cabeza.Y);
            g.FillRectangle(Brushes.Yellow, cabezaPos.X, cabezaPos.Y, TileSize, TileSize);
        }
    }
}
