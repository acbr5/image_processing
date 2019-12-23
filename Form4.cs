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
    public partial class Form4 : Form
    {
        Bitmap input_img, input_img2, output_img;
        int R = 0, G = 0, B = 0;
        int width, height;
        Image img;

        public Form4(Image img)
        {
            this.img = img;
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.MainMenu();
            this.pictureBox1.Image = img;
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
                this.ayarla1(Image.FromFile(od.FileName));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.InitialDirectory = @"E:\AYSEMM";
            od.Filter = "resim dosyaları|*.bmp;*.jpg;*.png";
            od.Multiselect = false;
            od.FilterIndex = 2;
            if (od.ShowDialog() == DialogResult.OK)
            {
                this.ayarla2(Image.FromFile(od.FileName));
            }
        }

        public void ayarla1(Image resim)
        {
            pictureBox1.Image = resim;
        }

        public void ayarla2(Image resim)
        {
            pictureBox2.Image = resim;
        }

        public void degistir(Bitmap resim)
        {
            pictureBox3.Image = resim;
        }

        public Bitmap getPic()
        {
            return new Bitmap(pictureBox1.Image);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public Bitmap getPic2()
        {
            return new Bitmap(pictureBox2.Image);
        }

        public void MainMenu()
        {
            MenuStrip m = new MenuStrip();
            ToolStripMenuItem komsubulma = new ToolStripMenuItem("PİKSELİN KOMŞULARINI BULMA");
            ToolStripMenuItem toplama = new ToolStripMenuItem("İKİ FOROĞRAFI TOPLAMA");
            ToolStripMenuItem cikarma = new ToolStripMenuItem("FOTOĞRAFTAN DİĞERİNİ ÇIKARMA");

            //MenuStript options
            m.Name = "MenuMain";
            m.Dock = DockStyle.Top;
            m.Items.Add(komsubulma);
            m.Items.Add(toplama);
            m.Items.Add(cikarma);

            //form controls add
            this.Controls.Add(m);

            //Click menu control
            komsubulma.Click += komsubulma_Click;
            toplama.Click += toplama_Click;
            cikarma.Click += cikarma_Click;
        }

        private void komsubulma_Click(object sender, EventArgs e)
        {
            try
            {
                int px = Convert.ToInt32(textBox1.Text);
                int py = Convert.ToInt32(textBox2.Text);
                int c = Convert.ToInt32(textBox3.Text);
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;

                if(c != 1 && c!=2 && c!=3)
                {
                    MessageBox.Show("Lütfen 1, 2 ya da 3 değerlerinden birisini girin!");
                }
                else
                {
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            // 4'lü komşuluklar
                            if (i == px && j == py && c == 1)
                            {
                                R = input_img.GetPixel(i - 1, j).R;
                                label5.Text = Convert.ToString(R);

                                R = input_img.GetPixel(i, j + 1).R;
                                label6.Text = Convert.ToString(R);

                                R = input_img.GetPixel(i + 1, j).R;
                                label8.Text = Convert.ToString(R);

                                R = input_img.GetPixel(i, j - 1).R;
                                label9.Text = Convert.ToString(R);
                            }
                            if (i == px && j == py && c == 2)
                            {
                                G = input_img.GetPixel(i - 1, j).G;
                                label5.Text = Convert.ToString(G);

                                G = input_img.GetPixel(i, j + 1).G;
                                label6.Text = Convert.ToString(G);

                                G = input_img.GetPixel(i + 1, j).G;
                                label8.Text = Convert.ToString(G);

                                G = input_img.GetPixel(i, j - 1).G;
                                label9.Text = Convert.ToString(G);
                            }
                            if (i == px && j == py && c == 3)
                            {
                                B = input_img.GetPixel(i - 1, j).B;
                                label5.Text = Convert.ToString(B);

                                B = input_img.GetPixel(i, j + 1).B;
                                label6.Text = Convert.ToString(B);

                                B = input_img.GetPixel(i + 1, j).B;
                                label8.Text = Convert.ToString(B);

                                B = input_img.GetPixel(i, j - 1).B;
                                label9.Text = Convert.ToString(B);
                            }
                            // 8'li Komşuluklar
                            if (i == px && j == py && c == 1)
                            {
                                R = input_img.GetPixel(i - 1, j).R;
                                label13.Text = Convert.ToString(R);

                                R = input_img.GetPixel(i, j + 1).R;
                                label12.Text = Convert.ToString(R);

                                R = input_img.GetPixel(i + 1, j).R;
                                label11.Text = Convert.ToString(R);

                                R = input_img.GetPixel(i, j - 1).R;
                                label10.Text = Convert.ToString(R);

                                R = input_img.GetPixel(i - 1, j + 1).R;
                                label17.Text = Convert.ToString(R);

                                R = input_img.GetPixel(i + 1, j + 1).R;
                                label16.Text = Convert.ToString(R);

                                R = input_img.GetPixel(i + 1, j - 1).R;
                                label15.Text = Convert.ToString(R);

                                R = input_img.GetPixel(i - 1, j - 1).R;
                                label14.Text = Convert.ToString(R);
                            }
                            if (i == px && j == py && c == 2)
                            {
                                G = input_img.GetPixel(i - 1, j).G;
                                label13.Text = Convert.ToString(G);

                                G = input_img.GetPixel(i, j + 1).G;
                                label12.Text = Convert.ToString(G);

                                G = input_img.GetPixel(i + 1, j).G;
                                label11.Text = Convert.ToString(G);

                                G = input_img.GetPixel(i, j - 1).G;
                                label10.Text = Convert.ToString(G);

                                G = input_img.GetPixel(i - 1, j + 1).G;
                                label17.Text = Convert.ToString(G);

                                G = input_img.GetPixel(i + 1, j + 1).G;
                                label16.Text = Convert.ToString(G);

                                G = input_img.GetPixel(i + 1, j - 1).G;
                                label15.Text = Convert.ToString(G);

                                G = input_img.GetPixel(i - 1, j - 1).G;
                                label14.Text = Convert.ToString(G);
                            }
                            if (i == px && j == py && c == 3)
                            {
                                B = input_img.GetPixel(i - 1, j).B;
                                label13.Text = Convert.ToString(B);

                                B = input_img.GetPixel(i, j + 1).B;
                                label12.Text = Convert.ToString(B);

                                B = input_img.GetPixel(i + 1, j).B;
                                label11.Text = Convert.ToString(B);

                                B = input_img.GetPixel(i, j - 1).B;
                                label10.Text = Convert.ToString(B);

                                B = input_img.GetPixel(i - 1, j + 1).B;
                                label17.Text = Convert.ToString(B);

                                B = input_img.GetPixel(i + 1, j + 1).B;
                                label16.Text = Convert.ToString(B);

                                B = input_img.GetPixel(i + 1, j - 1).B;
                                label15.Text = Convert.ToString(B);

                                B = input_img.GetPixel(i - 1, j - 1).B;
                                label14.Text = Convert.ToString(B);
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz veya Girdiğiniz Değerleri Kontrol Ediniz!");
            }
        }

        private void toplama_Click(object sender, EventArgs e)
        {
            try
            {
                input_img = this.getPic();
                input_img2 = this.getPic2();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width, height);

                if (width != input_img2.Width && height != input_img2.Height)
                {
                    MessageBox.Show("İki Fotoğrafın Boyutları Aynı Olmalı!");
                }
                else
                {
                    for(int i=0; i<width; i++)
                    {
                        for(int j=0; j<height; j++)
                        {
                            R = input_img.GetPixel(i, j).R + input_img2.GetPixel(i, j).R;
                            G = input_img.GetPixel(i, j).G + input_img2.GetPixel(i, j).G;
                            B = input_img.GetPixel(i, j).B + input_img2.GetPixel(i, j).B;
                            if (R > 255)
                                R = 255;
                            if (G > 255)
                                G = 255;
                            if (B > 255)
                                B = 255;
                            output_img.SetPixel(i, j, Color.FromArgb(R, G, B));
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

        private void cikarma_Click(object sender, EventArgs e)
        {
            try
            {
                input_img = this.getPic();
                input_img2 = this.getPic2();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width, height);

                if (width != input_img2.Width && height != input_img2.Height)
                {
                    MessageBox.Show("İki Fotoğrafın Boyutları Aynı Olmalı!");
                }
                else
                {
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            R = input_img.GetPixel(i, j).R - input_img2.GetPixel(i, j).R;
                            G = input_img.GetPixel(i, j).G - input_img2.GetPixel(i, j).G;
                            B = input_img.GetPixel(i, j).B - input_img2.GetPixel(i, j).B;
                            if (R < 0)
                                R = 0;
                            if (G < 0)
                                G = 0;
                            if (B < 0)
                                B = 0;
                            output_img.SetPixel(i, j, Color.FromArgb(R, G, B));
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