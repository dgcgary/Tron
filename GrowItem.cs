using Tron;

public class GrowItem
{
    public Nodo Posicion { get; private set; }
    public int Valor { get; private set; }

    public GrowItem(int x, int y, int valor)
    {
        Posicion = new Nodo(x, y);
        Valor = valor;
    }

    public void Draw(Graphics g, int tileSize)
    {
        g.FillRectangle(Brushes.White, Posicion.X * tileSize, Posicion.Y * tileSize, tileSize, tileSize);
    }
}