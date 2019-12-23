using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMAGE_PROCCESSING
{
    public partial class Form2 : Form
    {
        Bitmap input_img, output_img;
        Color input_clr;
        int R = 0, G = 0, B = 0;
        int width, height;
        Image img;

        public Form2(Image img)
        {
            this.img = img;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.pictureBox1.Image = img;
            this.MainMenu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.InitialDirectory = @"E:\AYSEMM";
            od.Filter = "resim dosyaları|*.bmp;*.jpg;*.png";
            od.Multiselect = false;
            od.FilterIndex = 2;
            if (od.ShowDialog() == DialogResult.OK)
            {
                this.ayarla(Image.FromFile(od.FileName));
                this.label1.Text = "Lütfen değerleri girdikten sonra menüden seçenekleri seçiniz.";
            }
        }

        public void ayarla(Image resim)
        {
            pictureBox1.Image = resim;
        }

        public void degistir(Bitmap resim)
        {
            pictureBox2.Image = resim;
        }

        public Bitmap getPic()
        {
            return new Bitmap(pictureBox1.Image);
        }

        public void MainMenu()
        {
            MenuStrip m = new MenuStrip();
            ToolStripMenuItem olcekleme = new ToolStripMenuItem("ÖLÇEKLEME");
            ToolStripMenuItem dondurme = new ToolStripMenuItem("DÖNDÜRME");
            ToolStripMenuItem oteleme = new ToolStripMenuItem("ÖTELEME");
            ToolStripMenuItem yatay_egme = new ToolStripMenuItem("YATAY EĞME");
            ToolStripMenuItem dikey_egme = new ToolStripMenuItem("DİKEY EĞME");

            //MenuStript options
            m.Name = "MenuMain";
            m.Dock = DockStyle.Top;
            m.Items.Add(olcekleme);
            m.Items.Add(dondurme);
            m.Items.Add(oteleme);
            m.Items.Add(yatay_egme);
            m.Items.Add(dikey_egme);

            //form controls add
            this.Controls.Add(m);

            //Click menu control
            olcekleme.Click += olcekleme_Click;
            dondurme.Click += dondurme_Click;
            oteleme.Click += oteleme_Click;
            yatay_egme.Click += yatay_egme_Click;
            dikey_egme.Click += dikey_egme_Click;            
        }

        private void olcekleme_Click(object sender, EventArgs e)
        {
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            try
            {
                int cx = Convert.ToInt16(textBox1.Text);
                int cy = Convert.ToInt16(textBox2.Text);
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width * cx, height * cy);
                int x2, y2;


                int ortalama;
                int toplam = 0;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        input_clr = input_img.GetPixel(i, j);
                        toplam += input_clr.R + input_clr.G + input_clr.B;
                    }
                }
                ortalama = Convert.ToInt32(toplam / (width * height * 3));

                for (int x1 = 0; x1 < width; x1++)
                {
                    for (int y1 = 0; y1 < height; y1++)
                    {
                        x2 = x1 * cx;
                        y2 = y1 * cy;

                        if (x2 >= 0 && x2 <= width * cx && y2 >= 0 && y2 <= height * cy)
                        {
                            R = input_img.GetPixel(x1, y1).R;
                            G = input_img.GetPixel(x1, y1).G;
                            B = input_img.GetPixel(x1, y1).B;
                            output_img.SetPixel(x2, y2, Color.FromArgb(R, G, B));
                        }
                        else
                        {
                            output_img.SetPixel(x2, y2, Color.FromArgb(ortalama, ortalama, ortalama));
                        }
                    }
                }
                this.degistir(output_img);
                
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz veya Girdiğiniz Değerleri Kontrol Ediniz!");
            }
        }

        private void dondurme_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width, height);
                int tetta = Convert.ToInt32(textBox4.Text);
                double aci = tetta * 2 * Math.PI / 360;
                int x2, y2;

                for (int x1 = 0; x1 < width; x1++)
                {
                    for (int y1 = 0; y1 < height; y1++)
                    {
                        x2 = Convert.ToInt32(x1 * Math.Cos(aci) - y1 * Math.Sin(aci));
                        y2 = Convert.ToInt32(x1 * Math.Sin(aci) + y1 * Math.Cos(aci));
                        if (x2 >= 0 && x2 < width && y2 >= 0 && y2 < height)
                        {
                            R = input_img.GetPixel(x1, y1).R;
                            G = input_img.GetPixel(x1, y1).G;
                            B = input_img.GetPixel(x1, y1).B;
                            output_img.SetPixel(x2, y2, Color.FromArgb(R, G, B));
                        }
                    }
                }
                this.degistir(output_img);
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz veya Girdiğiniz Değerleri Kontrol Ediniz!");
            }

        }

        private void oteleme_Click(object sender, EventArgs e)
        {
            try
            {
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                int tx = Convert.ToInt32(textBox5.Text);
                int ty = Convert.ToInt32(textBox6.Text);
                output_img = new Bitmap(width + tx, height + ty);
                int x2, y2;

                for (int x1 = 0; x1 < width; x1++)
                {
                    for (int y1 = 0; y1 < height; y1++)
                    {
                        x2 = x1 + tx;
                        y2 = y1 + tx;
                        if (x2 >= 0 && x2 < width+tx && y2 >= 0 && y2 < height+ty)
                        {
                            R = input_img.GetPixel(x1, y1).R;
                            G = input_img.GetPixel(x1, y1).G;
                            B = input_img.GetPixel(x1, y1).B;
                            output_img.SetPixel(x2, y2, Color.FromArgb(R, G, B));
                        }
                    }
                }
                this.degistir(output_img);
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz veya Girdiğiniz Değerleri Kontrol Ediniz!");
            }
        }

        private void yatay_egme_Click(object sender, EventArgs e)
        {
            try
            {
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                int sh = Convert.ToInt32(textBox7.Text);
                double aci = sh * 2 * Math.PI / 360;
                output_img = new Bitmap(width, height+sh);
                int x2, y2;

                for (int x1 = 0; x1 < width; x1++)
                {
                    for (int y1 = 0; y1 < height; y1++)
                    {
                        x2 = x1;
                        y2 = Convert.ToInt32(aci * x1 + y1);
                        if (x2 >= 0 && x2 < width && y2 >= 0 && y2 < height + sh)
                        {
                            R = input_img.GetPixel(x1, y1).R;
                            G = input_img.GetPixel(x1, y1).G;
                            B = input_img.GetPixel(x1, y1).B;
                            output_img.SetPixel(x2, y2, Color.FromArgb(R, G, B));
                        }
                    }
                }
                this.degistir(output_img);
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz veya Girdiğiniz Değerleri Kontrol Ediniz!");
            }
        }

        private void dikey_egme_Click(object sender, EventArgs e)
        {
            try
            {
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                int sv = Convert.ToInt32(textBox8.Text);
                double aci = sv * 2 * Math.PI / 360;
                output_img = new Bitmap(width + sv, height);
                int x2, y2;

                for (int x1 = 0; x1 < width; x1++)
                {
                    for (int y1 = 0; y1 < height; y1++)
                    {
                        x2 = Convert.ToInt32(aci * y1 + x1);
                        y2 = y1;
                        if (x2 >= 0 && x2 < width + sv && y2 >= 0 && y2 < height)
                        {
                            R = input_img.GetPixel(x1, y1).R;
                            G = input_img.GetPixel(x1, y1).G;
                            B = input_img.GetPixel(x1, y1).B;
                            output_img.SetPixel(x2, y2, Color.FromArgb(R, G, B));
                        }
                    }
                }
                this.degistir(output_img);
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz veya Girdiğiniz Değerleri Kontrol Ediniz!");
            }
        }
    }
}