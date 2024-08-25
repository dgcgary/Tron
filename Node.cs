using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    public class Nodo
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Nodo Siguiente { get; set; }

        public Nodo(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Siguiente = null;
        }
    }
}
