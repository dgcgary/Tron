using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    public class Player
    {
        public Node CurrentNode { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Player(Node startingNode)
        {
            CurrentNode = startingNode;
            X = startingNode.X;
            Y = startingNode.Y;
        }

        public void MoveUp(Grid grid)
        {
            if (CurrentNode.Up != null)
            {
                CurrentNode = CurrentNode.Up;
                X = CurrentNode.X;
                Y = CurrentNode.Y;
            }
        }

        public void MoveDown(Grid grid)
        {
            if (CurrentNode.Down != null)
            {
                CurrentNode = CurrentNode.Down;
                X = CurrentNode.X;
                Y = CurrentNode.Y;
            }
        }

        public void MoveLeft(Grid grid)
        {
            if (CurrentNode.Left != null)
            {
                CurrentNode = CurrentNode.Left;
                X = CurrentNode.X;
                Y = CurrentNode.Y;
            }
        }

        public void MoveRight(Grid grid)
        {
            if (CurrentNode.Right != null)
            {
                CurrentNode = CurrentNode.Right;
                X = CurrentNode.X;
                Y = CurrentNode.Y;
            }
        }
    }
}
