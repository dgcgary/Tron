using System.Drawing;

namespace Tron
{
    public class Player
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Player(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public void Mover(int dx, int dy)
        {
            this.X += dx;
            this.Y += dy;
        }
    }
}

