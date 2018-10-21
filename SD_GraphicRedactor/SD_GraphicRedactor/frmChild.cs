using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SD_GraphicRedactor
{
    public partial class frmChild : Form
    {
        private Point startPoint;
        public Image Image;
        public frmChild()
        {
            InitializeComponent();
        }

       

        private void frmChild_Load(object sender, EventArgs e)
        {
            Image = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            pictureBox1.Image = Image;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(Image);
            if (e.Button == MouseButtons.Left)
            {
                g.DrawLine(new Pen(Color.Black, 10), startPoint.X, startPoint.Y, e.X, e.Y);
                startPoint.X = e.X;
                startPoint.Y = e.Y;
                pictureBox1.Refresh();

            }
        }
    }
}
