using System;
using System.Drawing;
using System.Windows.Forms;

namespace _5_element
{
    public partial class MyControlElement : UserControl
    {
        Bitmap bmp;
        Graphics gr;
        int cellX, cellY;
        bool choise = false;
        Pen penCell;
        public MyControlElement()
        {
            InitializeComponent();
        }

        private void MyControlElement_Load(object sender, EventArgs e)
        {
            pictureBox1.Left = 0;
            pictureBox1.Top = 0;
            pictureBox1.Width = this.Width - 50;
            pictureBox1.Height = this.Height - 50;

            //zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz

            trackBar1.Left = 0;
            trackBar1.Top = pictureBox1.Height + 5;
            trackBar1.Width = pictureBox1.Width;

            //zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz

            trackBar2.Left = pictureBox1.Width + 5;
            trackBar2.Top = 0;
            trackBar2.Height = pictureBox1.Height;
        }

        private void MyControlElement_Resize(object sender, EventArgs e)
        {
            pictureBox1.Left = 0;
            pictureBox1.Top = 0;
            pictureBox1.Width = this.Width - 50;
            pictureBox1.Height = this.Height - 50;

            //zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz

            trackBar1.Left = 0;
            trackBar1.Top = pictureBox1.Height + 5;
            trackBar1.Width = pictureBox1.Width;

            //zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz

            trackBar2.Left = pictureBox1.Width + 5;
            trackBar2.Top = 0;
            trackBar2.Height = pictureBox1.Height;
        }

        private void ReDrawPicture()
        {
            gr.Clear(pictureBox1.BackColor);
            if (choise)
            {
                for (int i = 0; i <= cellX; i++)
                {
                    gr.DrawLine(penCell, (int)(bmp.Width * i / cellX), 0, (int)(bmp.Width * i / cellX), bmp.Height);
                }
                for (int i = 0; i <= cellY; i++)
                {
                    gr.DrawLine(penCell, 0, (int)(bmp.Height * i / cellY), bmp.Width, (int)(bmp.Height * i / cellY));
                }
            }





            pictureBox1.Image = bmp;
        }
        public void InitGraph()
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gr = Graphics.FromImage(bmp);
        }
        public void SetBackColor(Color cl)
        {
            pictureBox1.BackColor = cl;
        }
        public void Addcell(int x, int y, Color cl, int W)
        {
            choise = true;
            penCell = new Pen(cl, W);
            cellX = x;
            cellY = y;
            ReDrawPicture();
        }
        public void DeleteCell()
        {
            choise = false;
            penCell.Dispose();
            ReDrawPicture();
        }
    }
}
