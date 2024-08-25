using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tron
{
    public partial class Form1 : Form
    {
        private Grid grid;

        public Form1()
        {
            InitializeComponent();
            grid = new Grid(1920, 1980);
            this.BackColor= Color.Black;
            this.WindowState = FormWindowState.Maximized;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            grid.Draw(e.Graphics);
        }
    }
}