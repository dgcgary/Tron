using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    public class Grid
    {
        private Node[,] nodes;
        private int width;
        private int height;

        public Grid(int width, int height)
        {
            this.width = width;
            this.height = height;
            nodes = new Node[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    nodes[x, y] = new Node { X = x * 15, Y = y * 15 };
                }
            }

            // Conectar nodos
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Node node = nodes[x, y];

                    if (x > 0) node.Left = nodes[x - 1, y];
                    if (x < width - 1) node.Right = nodes[x + 1, y];
                    if (y > 0) node.Up = nodes[x, y - 1];
                    if (y < height - 1) node.Down = nodes[x, y + 1];
                }
            }
        }

        public void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color.Gray, 1))
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Node node = nodes[x, y];

                        if (node.Up != null) g.DrawLine(pen, node.X, node.Y, node.Up.X, node.Up.Y);
                        if (node.Down != null) g.DrawLine(pen, node.X, node.Y, node.Down.X, node.Down.Y);
                        if (node.Left != null) g.DrawLine(pen, node.X, node.Y, node.Left.X, node.Left.Y);
                        if (node.Right != null) g.DrawLine(pen, node.X, node.Y, node.Right.X, node.Right.Y);
                    }
                }
            }
        }
    }

}
