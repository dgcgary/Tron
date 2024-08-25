using System;

namespace Tron
{
    public class Bot : Player
    {
        private Random random;
        private int tiempoCambioDireccion;
        private int tiempoTranscurrido;

        public Bot(int x, int y, int width, int height, int velocidad) : base(x, y, width, height, velocidad)
        {
            random = new Random();
            tiempoCambioDireccion = 1000; // Cambiar dirección cada 1 segundo (en milisegundos)
            tiempoTranscurrido = 0;
        }

        public void MoverAleatorio(int screenWidth, int screenHeight, int intervalo)
        {
            tiempoTranscurrido += intervalo;

            if (tiempoTranscurrido >= tiempoCambioDireccion)
            {
                CambiarDireccionAleatoria();
                tiempoTranscurrido = 0;
            }

            // Mover el bot
            Mover();

            // Verificar límites de la pantalla y cambiar dirección si es necesario
            if (Cabeza.X < 0)
            {
                CambiarDireccion(Direccion.Derecha);
            }
            else if (Cabeza.X >= screenWidth / Width)
            {
                CambiarDireccion(Direccion.Izquierda);
            }
            else if (Cabeza.Y < 0)
            {
                CambiarDireccion(Direccion.Abajo);
            }
            else if (Cabeza.Y >= screenHeight / Height)
            {
                CambiarDireccion(Direccion.Arriba);
            }
        }

        private void CambiarDireccionAleatoria()
        {
            Direccion nuevaDireccion;
            do
            {
                int direccionAleatoria = random.Next(0, 4);
                nuevaDireccion = direccionAleatoria switch
                {
                    0 => Direccion.Arriba,
                    1 => Direccion.Abajo,
                    2 => Direccion.Izquierda,
                    3 => Direccion.Derecha,
                    _ => Direccion.Arriba
                };
            } while (EsDireccionOpuesta(nuevaDireccion));

            CambiarDireccion(nuevaDireccion);
        }

        private bool EsDireccionOpuesta(Direccion nuevaDireccion)
        {
            return (DireccionActual == Direccion.Arriba && nuevaDireccion == Direccion.Abajo) ||
                   (DireccionActual == Direccion.Abajo && nuevaDireccion == Direccion.Arriba) ||
                   (DireccionActual == Direccion.Izquierda && nuevaDireccion == Direccion.Derecha) ||
                   (DireccionActual == Direccion.Derecha && nuevaDireccion == Direccion.Izquierda);
        }
    }
}
