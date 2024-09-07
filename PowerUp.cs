public abstract class PowerUp
{
    public int Duration { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }

    protected PowerUp(int duration, int x, int y)
    {
        Duration = duration;
        X = x;
        Y = y;
    }
}

public class ShieldPowerUp : PowerUp
{
    public ShieldPowerUp(int duration, int x, int y) : base(duration, x, y) { }
}

public class HyperSpeedPowerUp : PowerUp
{
    public int SpeedMultiplier { get; private set; }

    public HyperSpeedPowerUp(int duration, int speedMultiplier, int x, int y) : base(duration, x, y)
    {
        SpeedMultiplier = speedMultiplier;
    }
}
