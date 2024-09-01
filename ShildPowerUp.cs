public class ShieldPowerUp
{
    public int Duration { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }

    public ShieldPowerUp(int duration, int x, int y)
    {
        Duration = duration;
        X = x;
        Y = y;
    }
}
