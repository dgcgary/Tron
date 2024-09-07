namespace Tron
{
    public class Nodo
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Nodo Arriba { get; set; }
        public Nodo Abajo { get; set; }
        public Nodo Izquierda { get; set; }
        public Nodo Derecha { get; set; }

        public Nodo(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
