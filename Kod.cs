using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_7
{
    public partial class Form1 : Form
    {
        Bitmap bitmap;
        int width;
        int height;

        int[] histR = new int[256];
        int[] histG = new int[256];
        int[] histB = new int[256];

        int[] histKoncowy = new int[256];

        bool read = false;

        double[,] macierzGauss;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog Opfile = new OpenFileDialog();
            if (DialogResult.OK == Opfile.ShowDialog())
            {
                pictureBox1.Image = new Bitmap(Opfile.FileName);
                bitmap = (Bitmap)pictureBox1.Image;

                width = pictureBox1.Image.Width;
                height = pictureBox1.Image.Height;

                Array.Clear(histR, 0, histR.Length);
                Array.Clear(histG, 0, histG.Length);
                Array.Clear(histB, 0, histB.Length);


                histogram();

                read = true;
                panel1.Invalidate();
                panel3.Invalidate();

                pictureBox2.Image = bitmap;
                pictureBox4.Image = bitmap;
            }
        }


        private void histogram()
        {
            int width = pictureBox1.Image.Width;
            int height = pictureBox1.Image.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color cA = bitmap.GetPixel(x, y);

                    int r = cA.R;
                    int g = cA.G;
                    int b = cA.B;

                    histR[r]++;
                    histG[g]++;
                    histB[b]++;
                }
            }

            for (int i = 0; i < histKoncowy.Length; i++)
            {
                histKoncowy[i] = histR[i] + histB[i] + histG[i];
            }
        }

        private void histogram2(Bitmap bitmapEdited)
        {
            int width = pictureBox1.Image.Width;
            int height = pictureBox1.Image.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color cA = bitmapEdited.GetPixel(x, y);

                    int r = cA.R;
                    int g = cA.G;
                    int b = cA.B;

                    histR[r]++;
                    histG[g]++;
                    histB[b]++;
                }
            }

            for (int i = 0; i < histKoncowy.Length; i++)
            {
                histKoncowy[i] = histR[i] + histB[i] + histG[i];
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (read == true)
            {
                Graphics graphR = e.Graphics;

                for (int i = 0; i < 256; i++)
                {
                    float r = histKoncowy[i];

                    r = r / (pictureBox1.Image.Height * pictureBox1.Image.Width);
                    r *= 1700;

                    graphR.DrawLine(new Pen(Color.Black), i, panel1.Height, i, panel1.Height - r);
                }
            }
        }


        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            if (read == true)
            {
                Graphics graphR = e.Graphics;

                for (int i = 0; i < 256; i++)
                {
                    float r = histKoncowy[i];

                    r = r / (pictureBox1.Image.Height * pictureBox1.Image.Width);
                    r *= 1700;

                    graphR.DrawLine(new Pen(Color.Black), i, panel1.Height, i, panel1.Height - r);
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            {
                Bitmap bitmapEdited = (Bitmap)bitmap.Clone();
                Bitmap bitmapCopy = (Bitmap)bitmap.Clone();

                int width = pictureBox1.Image.Width;
                int height = pictureBox1.Image.Height;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color cA = bitmapCopy.GetPixel(x, y);

                        double r = (double)cA.R;
                        double g = (double)cA.G;
                        double b = (double)cA.B;

                        int c = 102;
                        int d = 230;


                        r = (r - trackBar1.Value) * (d - c) / (trackBar2.Value - trackBar1.Value) + c;
                        g = (g - trackBar1.Value) * (d - c) / (trackBar2.Value - trackBar1.Value) + c;
                        b = (b - trackBar1.Value) * (d - c) / (trackBar2.Value - trackBar1.Value) + c;


                        if (r < 0)
                        {
                            r = 0;
                        }
                        if (g < 0)
                        {
                            g = 0;
                        }
                        if (b < 0)
                        {
                            b = 0;
                        }
                        if (r > 255)
                        {
                            r = 255;
                        }
                        if (g > 255)
                        {
                            g = 255;
                        }
                        if (b > 255)
                        {
                            b = 255;
                        }


                        bitmapEdited.SetPixel(x, y, Color.FromArgb((int)r, (int)g, (int)b));
                    }
                }

                pictureBox1.Image = bitmapEdited;

                Array.Clear(histR, 0, histR.Length);
                Array.Clear(histG, 0, histG.Length);
                Array.Clear(histB, 0, histB.Length);


                histogram2(bitmapEdited);

                read = true;
                panel3.Invalidate();
            }



        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Bitmap bitmapEdited = (Bitmap)bitmap.Clone();
            Bitmap bitmapCopy = (Bitmap)bitmap.Clone();


            int width = pictureBox1.Image.Width;
            int height = pictureBox1.Image.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color cA = bitmapCopy.GetPixel(x, y);

                    double r = (double)cA.R;
                    double g = (double)cA.G;
                    double b = (double)cA.B;

                    int c = 102;
                    int d = 230;


                    r = (r - trackBar1.Value) * (d - c) / (trackBar2.Value - trackBar1.Value) + c;
                    g = (g - trackBar1.Value) * (d - c) / (trackBar2.Value - trackBar1.Value) + c;
                    b = (b - trackBar1.Value) * (d - c) / (trackBar2.Value - trackBar1.Value) + c;

                    if (r < 0)
                    {
                        r = 0;
                    }
                    if (g < 0)
                    {
                        g = 0;
                    }
                    if (b < 0)
                    {
                        b = 0;
                    }
                    if (r > 255)
                    {
                        r = 255;
                    }
                    if (g > 255)
                    {
                        g = 255;
                    }
                    if (b > 255)
                    {
                        b = 255;
                    }

                    bitmapEdited.SetPixel(x, y, Color.FromArgb((int)r, (int)g, (int)b));
                }
            }

            pictureBox1.Image = bitmapEdited;


            Array.Clear(histR, 0, histR.Length);
            Array.Clear(histG, 0, histG.Length);
            Array.Clear(histB, 0, histB.Length);


            histogram2(bitmapEdited);

            read = true;
            panel3.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bitmapEdited = (Bitmap)bitmap.Clone();
            Bitmap bitmapCopy = (Bitmap)bitmap.Clone();

            double pomocR = 0, pomocG = 0, pomocB = 0;

            int width = pictureBox1.Image.Width;
            int height = pictureBox1.Image.Height;

            double[,] macierz = new double[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };


            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {

                    pomocR = 0; pomocG = 0; pomocB = 0;

                    for (int k = -1; k <= 1; k++)
                    {
                        for (int l = -1; l <= 1; l++)
                        {
                            int x1 = i + k;
                            int y1 = j + l;

                            if (x1 < 0)
                            {
                                x1 = 0;
                            }
                            if (x1 >= bitmap.Height)
                            {
                                x1 = bitmap.Height - 1;
                            }
                            if (y1 < 0)
                            {
                                y1 = 0;
                            }
                            if (y1 >= bitmap.Width)
                            {
                                y1 = bitmap.Width - 1;
                            }
                            Color p = bitmapCopy.GetPixel(y1, x1);

                            double r = (double)p.R;
                            double g = (double)p.G;
                            double b = (double)p.B;

                            pomocR += r * macierz[k + 1, l + 1];
                            pomocG += g * macierz[k + 1, l + 1];
                            pomocB += b * macierz[k + 1, l + 1];

                        }
                    }

                    pomocR = pomocR / 9;
                    pomocG = pomocG / 9;
                    pomocB = pomocB / 9;


                    if (pomocR < 0)
                    {
                        pomocR = 0;
                    }
                    if (pomocG < 0)
                    {
                        pomocG = 0;
                    }
                    if (pomocB < 0)
                    {
                        pomocB = 0;
                    }
                    if (pomocR > 255)
                    {
                        pomocR = 255;
                    }
                    if (pomocG > 255)
                    {
                        pomocG = 255;
                    }
                    if (pomocB > 255)
                    {
                        pomocB = 255;
                    }

                    bitmapEdited.SetPixel(j, i, Color.FromArgb((int)pomocR, (int)pomocG, (int)pomocB));
                }
            }

            pictureBox2.Image = bitmapEdited;
        }

        private void button3_Click(object sender, EventArgs e)          // r = 15 i σ = 10 
        {
            int kk = 7;
            int rr = 15;
            int sigma = 10;
            double suma = 0;

            macierzGauss = new double[rr, rr];

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    macierzGauss[i, j] = (1 / (2 * Math.PI * Math.Pow(sigma, 2))) * Math.Pow(Math.E, -(Math.Pow(i - kk, 2) + Math.Pow(j - kk, 2)) / (2 * Math.Pow(sigma, 2)));
                    suma = suma + macierzGauss[i, j];

                    Debug.WriteLine(macierzGauss[i,j]);
                }
            }

            Bitmap bitmapEdited = (Bitmap)bitmap.Clone();
            Bitmap bitmapCopy = (Bitmap)bitmap.Clone();

            double pomocR = 0, pomocG = 0, pomocB = 0;

            int width = pictureBox1.Image.Width;
            int height = pictureBox1.Image.Height;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {

                    pomocR = 0; pomocG = 0; pomocB = 0;

                    for (int k = -7; k <= 7; k++)
                    {
                        for (int l = -7; l <= 7; l++)
                        {
                            int x1 = i + k;
                            int y1 = j + l;

                            if (x1 < 0)
                            {
                                x1 = 0;
                            }
                            if (x1 >= bitmap.Height)
                            {
                                x1 = bitmap.Height - 1;
                            }
                            if (y1 < 0)
                            {
                                y1 = 0;
                            }
                            if (y1 >= bitmap.Width)
                            {
                                y1 = bitmap.Width - 1;
                            }
                            Color p = bitmapCopy.GetPixel(y1, x1);

                            double r = (double)p.R;
                            double g = (double)p.G;
                            double b = (double)p.B;

                            pomocR += r * macierzGauss[k + kk, l + kk];
                            pomocG += g * macierzGauss[k + kk, l + kk];
                            pomocB += b * macierzGauss[k + kk, l + kk];

                        }
                    }

                    pomocR = pomocR / suma;
                    pomocG = pomocG / suma;
                    pomocB = pomocB / suma;


                    if (pomocR < 0)
                    {
                        pomocR = 0;
                    }
                    if (pomocG < 0)
                    {
                        pomocG = 0;
                    }
                    if (pomocB < 0)
                    {
                        pomocB = 0;
                    }
                    if (pomocR > 255)
                    {
                        pomocR = 255;
                    }
                    if (pomocG > 255)
                    {
                        pomocG = 255;
                    }
                    if (pomocB > 255)
                    {
                        pomocB = 255;
                    }

                    bitmapEdited.SetPixel(j, i, Color.FromArgb((int)pomocR, (int)pomocG, (int)pomocB));
                }
            }

            pictureBox4.Image = bitmapEdited;
        }
    }
}
