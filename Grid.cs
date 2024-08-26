using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tron
{

    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Node? Up { get; set; }
        public Node? Down { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }
    }

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
                    nodes[x, y] = new Node { X = x * 20, Y = y * 20 };
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

        public Node GetStartNode()
        {
            return nodes[0, 0];
        }

        public void Draw(Graphics g, Player player)
        {
            using (Pen pen = new Pen(Color.Gray, 1))
            {
                for (int x = 0; x < width; x++)
                {
                    // Draw vertical lines
                    g.DrawLine(pen, x * 20, 0, x * 20, height * 20);
                }

                for (int y = 0; y < height; y++)
                {
                    // Draw horizontal lines
                    g.DrawLine(pen, 0, y * 20, width * 20, y * 20);
                }

                //Draw player
                using (Brush brush = new SolidBrush(Color.Red))
                {
                    g.FillRectangle(brush, player.X, player.Y, 20, 20);
                }

            }
        }
    }

}
