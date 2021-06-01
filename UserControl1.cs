using System;
using System.Drawing;
using System.Windows.Forms;

namespace _5_element
{
    public partial class MyControlElement : UserControl
    {


        //Xscr=Kx(x-min);Yscr=bmp.Height-Ky(y-Ymin)

        //zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz

        Bitmap bmp;
        Graphics gr;
        int cellX, cellY;
        bool choiseC = false;
        Pen penCell;

        //zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz

        bool choiseG = false;
        double x, y, xxx;
        int x1scr, y1scr, x2scr, y2scr;
        double yscr;
        double kx, ky;
        double xbasmin = -1, xbasmax = 1;
        double ybasmin = -1, ybasmax = 1;
        double xmin, xmax;
        double ymin, ymax;
        int degree;
        double[] koef;
        Pen penGraph;
        public double Ahor = 1.1, Av = 1.1, Ak = 1.1;
        double k = 1;

        //zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz

        bool choiseP = false;
        int num = 0;
        double[] Xpoint, Ypoint;
        SolidBrush kistochka;
        public int typepoints = 0;
        public int size = 5;



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

            trackBar2.Left = pictureBox1.Width + 5;
            trackBar2.Top = 0;
            trackBar2.Height = pictureBox1.Height;

            //zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz

            button1.Left = 0;
            button1.Top = 0;
            button1.Width = (trackBar1.Width / 6);

            button2.Left = 0;
            button2.Top = button1.Height;
            button2.Width = (trackBar1.Width / 6);

            button3.Left =0;
            button3.Top = button1.Height + button2.Height;
            button3.Width = (trackBar1.Width/6);

            //zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz

            textBox1.Left = button1.Width + 5;
            textBox1.Top = 0;
            textBox1.Width = button1.Width;

            textBox2.Left = button1.Width + 5;
            textBox2.Top = button1.Height;
            textBox2.Width = button1.Width;

            //zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz

            button2.Enabled = false;
            button3.Enabled = false;
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

            trackBar2.Left = pictureBox1.Width + 5;
            trackBar2.Top = 0;
            trackBar2.Height = pictureBox1.Height;

            //zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz

            button1.Left = 0;
            button1.Top = 0;
            button1.Width = (trackBar1.Width / 6);

            button2.Left = 0;
            button2.Top = button1.Height;
            button2.Width = (trackBar1.Width / 6);

            button3.Left = 0;
            button3.Top = button1.Height + button2.Height;
            button3.Width = (trackBar1.Width / 6);

            //zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz

            textBox1.Left = button1.Width + 5;
            textBox1.Top = 0;
            textBox1.Width = button1.Width;

            textBox2.Left = button1.Width + 5;
            textBox2.Top = button1.Height;
            textBox2.Width = button1.Width;
        }

        private void ReDrawPicture()
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gr = Graphics.FromImage(bmp);
            gr.Clear(pictureBox1.BackColor);
            if (choiseC)
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
            if (choiseG)
            {
                xmin = ((xbasmin + xbasmax) / 2) - ((xbasmax - xbasmin) / 2) * k * Math.Pow(Ahor, trackBar1.Value);
                xmax = ((xbasmin + xbasmax) / 2) + ((xbasmax - xbasmin) / 2) * k * Math.Pow(Ahor, trackBar1.Value);
                ymin = ((ybasmin + ybasmax) / 2) - ((ybasmax - ybasmin) / 2) * k * Math.Pow(Av, trackBar2.Value);
                ymax = ((ybasmin + ybasmax) / 2) + ((ybasmax - ybasmin) / 2) * k * Math.Pow(Av, trackBar2.Value);
                kx = bmp.Width / (xmax - xmin);
                ky = bmp.Height / (ymax - ymin);

                x = xmin;
                y = 0;
                xxx = 1;
                for (int i = degree; i >= 0; i--)
                {
                    y += koef[i] * xxx;
                    xxx *= x;
                }
                x1scr = 0;
                yscr = (bmp.Height - ky * (y - ymin));
                if (yscr < 0)
                {
                    y1scr = -1;
                }
                else
                {
                    if (yscr > bmp.Height)
                    {
                        y1scr = bmp.Height + 1;
                    }
                    else
                    {
                        y1scr = (int)yscr;
                    }
                }
                for (int i = 1; i <= bmp.Width; i++)
                {
                    x2scr = i;
                    x = xmin + x2scr / kx;
                    y = 0;
                    xxx = 1;
                    for(int j = degree; j>= 0; j--) 
                    {
                        y += koef[j] * xxx;
                        xxx *= x;
                    }
                    //y2scr = (int)(bmp.Height - ky * (y - ymin));
                    yscr = (bmp.Height - ky * (y - ymin));
                    if (yscr < 0)
                    {
                        y2scr = -1;
                    }
                    else
                    {
                        if (yscr > bmp.Height)
                        {
                            y2scr = bmp.Height + 1;
                        }
                        else
                        {
                            y2scr = (int)yscr;
                        }
                    }
                    gr.DrawLine(penGraph, x1scr, y1scr, x2scr, y2scr);
                    x1scr = x2scr;
                    y1scr = y2scr;
                }
                if (choiseP)
                {
                    for (int i = 0; i < num; i++)
                    {
                        int Xscr = (int)(kx * (Xpoint[i] - xmin));
                        int Yscr = (int)(bmp.Height - ky * (Ypoint[i] - ymin));
                        switch (typepoints)
                        {
                            case 0:
                                gr.FillEllipse(kistochka, Xscr - size, Yscr - size, 2 * size, 2 * size);
                                break;
                            case 1:
                                gr.FillRectangle(kistochka, Xscr - size, Yscr - size, 2 * size, 2 * size);
                                break;

                        }
                    }

                }
                button3.Enabled = true;
            }
            pictureBox1.Image = bmp;
        }

        //

        public void addPoints(int N, double[] X, double[] Y, Color cl)
        {
            choiseP = true;
            num = N;
            Xpoint = new double[N];
            Ypoint = new double[N];
            kistochka = new SolidBrush(cl);
            for (int i = 0; i < N; i++)
            {
                Xpoint[i] = X[i];
                Ypoint[i] = Y[i];
            }
            ReDrawPicture();
        }
        public void deletePoints()
        {
            choiseP = false;
            kistochka.Dispose();
            Array.Clear(Xpoint,0,num);
            Array.Clear(Ypoint, 0, num);
            ReDrawPicture();
        }
        public void addGraph(int n, double[] A, Color cl, int ww)
        {
            koef = new double[n + 1];
            for (int i = 0; i <= n;i++)
            {
                koef[i] = A[i];
            }
            penGraph = new Pen(cl, ww);
            degree = n;
            choiseG = true;
            ReDrawPicture();
        }
        public void deleteGraph()
        {
            penGraph.Dispose();
            choiseG = false;
            ReDrawPicture();
        }
        public void SetBackColor(Color cl)
        {
            pictureBox1.BackColor = cl;
        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (choiseG)
            {
                label1.BackColor = pictureBox1.BackColor;
                label1.Left = e.X;
                label1.Top = e.Y + 10;
                label1.Text = string.Format("( {0:f2}, {1:f2})", xmin + (e.X / kx), ymin + ((bmp.Height - e.Y) / ky));
                k *= Math.Pow(Ak, e.Delta / 100);
                ReDrawPicture();
            }
        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            label1.Visible = choiseG;//если есть график, то и  метка есть  
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            label1.Visible = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (choiseG)
            {
                label1.BackColor = pictureBox1.BackColor;
                label1.Left = e.X;
                label1.Top = e.Y + 10;
                label1.Text = string.Format("( {0:f2}, {1:f2})", xmin + (e.X / kx), ymin + ((bmp.Height - e.Y) / ky));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string x;
            x = textBox1.Text;
            bool bla = int.TryParse(x, out cellX);
            string y;
            y = textBox2.Text;
            bool bla2 = int.TryParse(y, out cellY);
            if (bla & bla2)
            { 
                cellX = Convert.ToInt32(textBox1.Text);
                cellY = Convert.ToInt32(textBox2.Text);

                if (cellX < 0 | cellY < 0)
                {
                    DialogResult dr = MessageBox.Show("Введено отрицательное число. Использовать модуль?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (dr == DialogResult.Yes)
                    {
                        cellX = Math.Abs(Convert.ToInt32(textBox1.Text));
                        cellY = Math.Abs(Convert.ToInt32(textBox2.Text));
                        choiseC = true;
                        penCell = new Pen(Color.Black, 1);
                        ReDrawPicture();
                        button2.Enabled = true;
                    }
                    else
                    {
                        textBox1.Text = (" ");
                        textBox2.Text = (" ");
                        choiseC = false;
                        penCell.Dispose();
                        ReDrawPicture();
                    }
                }
                else
                {
                    choiseC = true;
                    penCell = new Pen(Color.Black, 1);
                    ReDrawPicture();
                    button2.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Введите число", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = (" ");
                textBox2.Text = (" ");
                choiseC = false;
                ReDrawPicture();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            choiseC = false;
            penCell.Dispose();
            ReDrawPicture();
            textBox1.Text = (" ");
            textBox2.Text = (" ");
            button2.Enabled = false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 0;
            trackBar2.Value = 0;
            k = 1;
            ReDrawPicture();
        }



        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            ReDrawPicture();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            ReDrawPicture();
        }
    }
}
