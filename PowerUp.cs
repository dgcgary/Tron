/*
 * Clase abstracta que representa un PowerUp en el juego.
 */
public abstract class PowerUp
{
    public int Duration { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }

    /// Constructor protegido para inicializar un PowerUp.
    /// name="duration">Duración del PowerUp en segundos.
    /// name="x">Coordenada X del PowerUp en la cuadrícula.
    /// name="y">Coordenada Y del PowerUp en la cuadrícula.
    protected PowerUp(int duration, int x, int y)
    {
        Duration = duration;
        X = x;
        Y = y;
    }
}

//Clase que representa el escudo
public class ShieldPowerUp : PowerUp
{
    /// Constructor para inicializar un PowerUp de escudo
    /// name="duration">Duración del PowerUp en segundos.
    /// name="x">Coordenada X del PowerUp en la cuadrícula.
    /// name="y">Coordenada Y del PowerUp en la cuadrícula.
    public ShieldPowerUp(int duration, int x, int y) : base(duration, x, y) { }
}

/*
 * Clase que representa la hipervelocidad
 */
public class HyperSpeedPowerUp : PowerUp
{
    /// Multiplicador de velocidad que se aplica al jugador.
    public int SpeedMultiplier { get; private set; }

    /// Constructor para inicializar un PowerUp de hipervelocidad.
    /// name="duration">Duración del PowerUp en segundos.
    /// name="speedMultiplier">Multiplicador de velocidad.
    /// name="x">Coordenada X del PowerUp en la cuadrícula.
    /// name="y">Coordenada Y del PowerUp en la cuadrícula.
    public HyperSpeedPowerUp(int duration, int speedMultiplier, int x, int y) : base(duration, x, y)
    {
        SpeedMultiplier = speedMultiplier;
    }
}
