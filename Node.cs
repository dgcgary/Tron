namespace Tron
{
    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Node Up { get; set; }
        public Node Down { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}