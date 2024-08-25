namespace Tron
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Form1";
            this.BackColor = System.Drawing.Color.Black; 
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized; 
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; 
            this.ResumeLayout(false);
        }
    }
}
