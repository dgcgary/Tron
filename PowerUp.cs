/*
 * Clase abstracta que representa un PowerUp en el juego.
 * Un PowerUp es un objeto que otorga habilidades especiales al jugador.
 */
public abstract class PowerUp
{
    /// Duración del PowerUp en segundos.
    public int Duration { get; private set; }

    /// Coordenada X del PowerUp en la cuadrícula.
    public int X { get; private set; }


    /// Coordenada Y del PowerUp en la cuadrícula.
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

/*
 * Clase que representa un PowerUp de escudo.
 * El escudo otorga invulnerabilidad al jugador durante un tiempo limitado.
 */
public class ShieldPowerUp : PowerUp
{
    /// Constructor para inicializar un PowerUp de escudo
    /// name="duration">Duración del PowerUp en segundos.
    /// name="x">Coordenada X del PowerUp en la cuadrícula.
    /// name="y">Coordenada Y del PowerUp en la cuadrícula.
    public ShieldPowerUp(int duration, int x, int y) : base(duration, x, y) { }
}

/*
 * Clase que representa un PowerUp de hipervelocidad.
 * La hipervelocidad aumenta la velocidad del jugador durante un tiempo limitado.
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
