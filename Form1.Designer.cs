namespace Tron
{
   
    /// Clase parcial para el diseño del formulario principal del juego.
    partial class Form1
    {
       
        /// Variable requerida por el diseñador.       
        private System.ComponentModel.IContainer components = null;

       
        /// Limpia los recursos que se están utilizando.      
        /// name="disposing" true si los recursos administrados deben ser eliminados; false en caso contrario.
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

       
        /// Método requerido para el soporte del diseñador.      
        private void InitializeComponent()
        {
            SuspendLayout();

            // Configuración de las dimensiones de autoescalado
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;

            // Configuración del color de fondo del formulario
            BackColor = Color.Black;

            // Configuración del tamaño del formulario
            ClientSize = new Size(800, 450);

            // Configuración del estilo del borde del formulario
            FormBorderStyle = FormBorderStyle.None;

            // Configuración del nombre del formulario
            Name = "Form1";

            // Configuración del texto del formulario
            Text = "Form1";

            // Configuración del estado de la ventana del formulario
            WindowState = FormWindowState.Maximized;

            // Suscripción al evento de carga del formulario
            Load += Form1_Load;

            ResumeLayout(false);
        }

        #endregion
    }
}
