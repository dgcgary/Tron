using System.Windows.Forms;
using Tron;

namespace TronGame
{
    /// Clase principal del programa que contiene el punto de entrada.
    static class Program
    {
        [STAThread]
        /// Punto de entrada principal para la aplicación.
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}