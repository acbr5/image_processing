using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace IMAGE_PROCCESSING
{
    public partial class Form1 : Form
    {
        Form2 frm2;
        Form3 frm3;
        Form4 frm4;

        Bitmap input_img, output_img;
        Color input_clr;
        int R = 0, G = 0, B = 0;
        int width, height;
        int sayac = 0;

        ArrayList pikselSayilari = new ArrayList();
        ArrayList pikseller = new ArrayList();
        int[] numberP = new int[256];
        int c, d;
        int gmax = 255;
        int gmin = 0;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            this.MainTools();
            this.MainMenu();      
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
            OpenFileDialog od = new OpenFileDialog();
            od.InitialDirectory = @"\";
            od.Filter = "resim dosyaları|*.bmp;*.jpg;*.png";
            od.Multiselect = false;
            od.FilterIndex = 2;
            if (od.ShowDialog() == DialogResult.OK)
            {
                this.ayarla(Image.FromFile(od.FileName));
                this.label1.Text = "Resim başarılı bir şekilde yüklendi. Lütfen menüden seçenekleri seçiniz.";
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {         
           /* sayac++;
            if(sayac % 4 == 0)
            {
                pictureBox1.Image = Image.FromFile("ornek2.JPG");
            }
            else if(sayac % 4 ==1)
            {
                pictureBox1.Image = Image.FromFile("ornek4.jpg");
            }
            else if(sayac % 4 == 2)
            {
                pictureBox1.Image = Image.FromFile("ornek1.jpg");
            }
            else
            {
                pictureBox1.Image = Image.FromFile("ornek3.jpg");
            }*/
        }
        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox2.Size = new System.Drawing.Size(389, 324);
            pictureBox2.Image = null;
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
            ToolStripMenuItem resimDonustur = new ToolStripMenuItem("TEMEL İŞLEMLER");
            ToolStripMenuItem komsulukIslemleri = new ToolStripMenuItem("PİKSEL İŞLEMLERİ");
            ToolStripMenuItem geometrikUzamsalIslemler = new ToolStripMenuItem("UZAMSAL İŞLEMLER");
            ToolStripMenuItem donusumler = new ToolStripMenuItem("DÖNÜŞÜMLER");
            ToolStripMenuItem histogram = new ToolStripMenuItem("HİSTOGRAM İŞLEMLERİ");
            ToolStripMenuItem filtreler = new ToolStripMenuItem("FİLTRE İŞLEMLERİ");

            ToolStripMenuItem griton = new ToolStripMenuItem("Griton");
            ToolStripMenuItem negatif = new ToolStripMenuItem("Negatif");
            ToolStripMenuItem siyahbeyaz = new ToolStripMenuItem("Siyah Beyaz");
            ToolStripMenuItem yakinlastirma = new ToolStripMenuItem("Yakınlaştırma");
            ToolStripMenuItem uzaklastirma = new ToolStripMenuItem("Uzaklaştırma");

            ToolStripMenuItem kontrast = new ToolStripMenuItem("Kontrast Genişletme");
            ToolStripMenuItem logaritma = new ToolStripMenuItem("Logaritma Dönüşümü");
            ToolStripMenuItem gama = new ToolStripMenuItem("Kuvvet Gama Dönüşümü");

            ToolStripMenuItem hesaplama = new ToolStripMenuItem("Histogram Grafiği");
            ToolStripMenuItem germe = new ToolStripMenuItem("Histogram Germe");
            ToolStripMenuItem esitleme = new ToolStripMenuItem("Histogram Eşitleme");

            ToolStripMenuItem gurultu = new ToolStripMenuItem("Gürültü");
            ToolStripMenuItem mean = new ToolStripMenuItem("Mean Filtresi");
            ToolStripMenuItem medyan = new ToolStripMenuItem("Median Filtresi");
            ToolStripMenuItem sobel = new ToolStripMenuItem("Sobel Filtresi");
            ToolStripMenuItem prewitt = new ToolStripMenuItem("Prewitt Filtresi");


            //ToolStripMenuItem options
            resimDonustur.DropDownItems.Add(griton);
            resimDonustur.DropDownItems.Add(negatif);
            resimDonustur.DropDownItems.Add(siyahbeyaz);
            resimDonustur.DropDownItems.Add(yakinlastirma);
            resimDonustur.DropDownItems.Add(uzaklastirma);

            donusumler.DropDownItems.Add(kontrast);
            donusumler.DropDownItems.Add(logaritma);
            donusumler.DropDownItems.Add(gama);

            histogram.DropDownItems.Add(hesaplama);
            histogram.DropDownItems.Add(germe);
            histogram.DropDownItems.Add(esitleme);

            filtreler.DropDownItems.Add(gurultu);
            filtreler.DropDownItems.Add(mean);
            filtreler.DropDownItems.Add(medyan);
            filtreler.DropDownItems.Add(sobel);
            filtreler.DropDownItems.Add(prewitt);

            //MenuStript options
            m.Name = "MenuMain";
            m.Dock = DockStyle.Top;
            m.Items.Add(resimDonustur);
            m.Items.Add(komsulukIslemleri);
            m.Items.Add(geometrikUzamsalIslemler);
            m.Items.Add(donusumler);
            m.Items.Add(histogram);
            m.Items.Add(filtreler);

            //form controls add
            this.Controls.Add(m);

            //Click menu control
            griton.Click += new EventHandler(griton_Click);
            negatif.Click += negatif_Click;
            siyahbeyaz.Click += siyahbeyaz_Click;
            yakinlastirma.Click += yakinlastirma_Click;
            uzaklastirma.Click += uzaklastirma_Click;

            komsulukIslemleri.Click += komsulukIslemleri_Click;

            geometrikUzamsalIslemler.Click += olcekleme_Click;
 
            kontrast.Click += kontrast_Click;
            logaritma.Click += logaritma_Click;
            gama.Click += gama_Click;

            hesaplama.Click += hesaplama_Click;
            germe.Click += germe_Click;
            esitleme.Click += esitleme_Click;

            gurultu.Click += gurultu_Click;
            mean.Click += mean_Click;
            medyan.Click += medyan_Click;
            sobel.Click += sobel_Click;
            prewitt.Click += prewitt_Click;
        }

        private void griton_Click(object sender, EventArgs e)
        {
            try
            {
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width, height);
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        input_clr = input_img.GetPixel(i, j);
                        R = Convert.ToInt32(0.2989 * input_clr.R + 0.5870 * input_clr.G + 0.1140 * input_clr.B);
                        G = Convert.ToInt32(0.2989 * input_clr.R + 0.5870 * input_clr.G + 0.1140 * input_clr.B);
                        B = Convert.ToInt32(0.2989 * input_clr.R + 0.5870 * input_clr.G + 0.1140 * input_clr.B);
                        output_img.SetPixel(i, j, Color.FromArgb(R, G, B));
                    }
                }
                this.degistir(output_img);
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz!");
            }
           
        }

        private void griton()
        {
            try
            {
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width, height);
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        input_clr = input_img.GetPixel(i, j);
                        R = Convert.ToInt32(0.2989 * input_clr.R + 0.5870 * input_clr.G + 0.1140 * input_clr.B);
                        G = Convert.ToInt32(0.2989 * input_clr.R + 0.5870 * input_clr.G + 0.1140 * input_clr.B);
                        B = Convert.ToInt32(0.2989 * input_clr.R + 0.5870 * input_clr.G + 0.1140 * input_clr.B);
                        output_img.SetPixel(i, j, Color.FromArgb(R, G, B));
                    }
                }
                this.ayarla(output_img);
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz!");
            }
        }

        private void negatif_Click(object sender, EventArgs e)
        {
            try
            {
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width, height);
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        input_clr = input_img.GetPixel(i, j);
                        R = 255 - input_clr.R;
                        G = 255 - input_clr.G;
                        B = 255 - input_clr.B;
                        output_img.SetPixel(i, j, Color.FromArgb(R, G, B));
                    }
                }
                this.degistir(output_img);
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz!");
            }            
        }

        private void siyahbeyaz_Click(object sender, EventArgs e)
        {
            try
            {
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width, height);

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

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        input_clr = input_img.GetPixel(i, j);
                        if (input_clr.R < ortalama)
                            R = 0;
                        else
                            R = 255;
                        if (input_clr.G < ortalama)
                            G = 0;
                        else
                            G = 255;
                        if (input_clr.B < ortalama)
                            B = 0;
                        else
                            B = 255;

                        toplam = R + G + B;
                        if (toplam > 255)
                        {
                            toplam = 255;
                        }
                        output_img.SetPixel(i, j, Color.FromArgb(toplam, toplam, toplam));
                    }
                }

                this.degistir(output_img);
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz!");
            }
           
        }

        private void yakinlastirma_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox2.SizeMode=PictureBoxSizeMode.AutoSize;

                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width*2, height*2);

                int i = 0, j = 0;
                int a1, b1, c1, d1, a2, b2, c2, d2, a3, b3, c3, d3;

                for (int x = 0; x < width * 2; x++)
                {
                    for (int y = 0; y < height * 2; y++)
                    {
                        try
                        {
                            if ((x + 2) <= width && (y + 2) <= height)
                            {
                                a1 = input_img.GetPixel(x, y).R / 4;
                                b1 = input_img.GetPixel(x, y + 1).R / 4;
                                c1 = input_img.GetPixel(x + 1, y).R / 4;
                                d1 = input_img.GetPixel(x + 1, y + 1).R / 4;

                                R = Convert.ToInt32(a1 + b1 + c1 + d1);

                                a2 = input_img.GetPixel(x, y).G / 4;
                                b2 = input_img.GetPixel(x, y + 1).G / 4;
                                c2 = input_img.GetPixel(x + 1, y).G / 4;
                                d2 = input_img.GetPixel(x + 1, y + 1).G / 4;

                                G = Convert.ToInt32(a2 + b2 + c2 + d2);

                                a3 = input_img.GetPixel(x, y).B / 4;
                                b3 = input_img.GetPixel(x, y + 1).B / 4;
                                c3 = input_img.GetPixel(x + 1, y).B / 4;
                                d3 = input_img.GetPixel(x + 1, y + 1).B / 4;

                                B = Convert.ToInt32(a3 + b3 + c3 + d3);

                                output_img.SetPixel(i, j, Color.FromArgb(R, G, B));
                                output_img.SetPixel(i, j + 1, Color.FromArgb(R, G, B));
                                output_img.SetPixel(i + 1, j, Color.FromArgb(R, G, B));
                                output_img.SetPixel(i + 1, j + 1, Color.FromArgb(R, G, B));
                            }
                        }                 
                        catch { }
                        j += 2;
                    }
                    j = 0;
                    i += 2;
                }

                this.degistir(output_img);
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz!");
            }
           
        }
        private void uzaklastirma_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width / 2, height / 2);

                int i = 0, j = 0;
                
                for(int x=0; x<width; x = x + 2)
                {
                    for(int y=0; y<height; y = y + 2)
                    {
                        R = input_img.GetPixel(x, y).R;
                        G = input_img.GetPixel(x, y).G;
                        B = input_img.GetPixel(x, y).B;
                        output_img.SetPixel(i, j, Color.FromArgb(R, G, B));

                        j++;
                    }
                    j = 0;
                    i++;
                }
                this.degistir(output_img);
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                label1.Text = "Uzaklaştırma işleminden sonra Temizle butonuna basınız.";                      
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz!");
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
       
        private void komsulukIslemleri_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            frm4= new Form4(img);
            frm4.Show();
        }

        private void olcekleme_Click(object sender, EventArgs e)
        {
            frm2 = new Form2(pictureBox1.Image);
            frm2.Show();
        }

        private void kontrast_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Kontrast Genişletme İşleminin Birebir Aynısı Siyah Beyaza Dönüştürmede Kullanıldığı İçin Değişiklik Olması Adına Burada Farklı Bir Formül Kullanılmıştır.\n\nFormül: f = 259 * (treshold + 255) / (255 * (259 - treshold))", "UYARI");
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width, height);
                double treshold = 25;
                double f = 259 * (treshold + 255) / (255 * (259 - treshold));


                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        R = Convert.ToInt32(input_img.GetPixel(i, j).R * f + 128);
                        G = Convert.ToInt32(input_img.GetPixel(i, j).G * f + 128);
                        B = Convert.ToInt32(input_img.GetPixel(i, j).B * f + 128);
                        if (R > 255) R = 255;
                        if (G > 255) G = 255;
                        if (B > 255) B = 255;
                        if (R < 0) R = 0;
                        if (G < 0) G = 0;
                        if (B < 0) B = 0;

                        output_img.SetPixel(i, j, Color.FromArgb(R, G, B));
                    }
                }
                this.degistir(output_img);
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz veya Eşik Değerini Girdiğinizden Emin Olunuz!");
            }
        }
        private void logaritma_Click(object sender, EventArgs e)
        {
            try
            {
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width, height);
                int c = 10;


                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        R = Convert.ToInt32(c * Math.Log(1 + input_img.GetPixel(i, j).R));
                        G = Convert.ToInt32(c * Math.Log(1 + input_img.GetPixel(i, j).G));
                        B = Convert.ToInt32(c * Math.Log(1 + input_img.GetPixel(i, j).B));
                        if (R > 255) R = 255;
                        if (G > 255) G = 255;
                        if (B > 255) B = 255;
                        if (R < 0) R = 0;
                        if (G < 0) G = 0;
                        if (B < 0) B = 0;

                        output_img.SetPixel(i, j, Color.FromArgb(R, G, B));
                    }
                }
                this.degistir(output_img);
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz!");
            }
        }
        private void gama_Click(object sender, EventArgs e)
        {
            try
            {
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width, height);
                int c = 1;
                double y = 1.5;


                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        R = Convert.ToInt32(c * Math.Pow(input_img.GetPixel(i, j).R, y));
                        G = Convert.ToInt32(c * Math.Pow(input_img.GetPixel(i, j).G, y));
                        B = Convert.ToInt32(c * Math.Pow(input_img.GetPixel(i, j).B, y));
                        if (R > 255) R = 255;
                        if (G > 255) G = 255;
                        if (B > 255) B = 255;
                        if (R < 0) R = 0;
                        if (G < 0) G = 0;
                        if (B < 0) B = 0;

                        output_img.SetPixel(i, j, Color.FromArgb(R, G, B));
                    }
                }
                this.degistir(output_img);
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz!");
            }
        }

        private void grafik_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox2.Image = null;
                input_img = this.getPic();
                Bitmap griton = this.griton(input_img);
                pictureBox1.Image = griton;
                width = griton.Width;
                height = griton.Height;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        pikseller.Add(griton.GetPixel(i, j).R);
                    }
                }

                int sayac = 0;
                for (int i = pikseller.Count; i > 0; i--)
                {
                    for (int j = 0; j < pikseller.Count; j++)
                    {
                        if (Convert.ToInt32(pikseller[j]) == Convert.ToInt32(pikseller[i]))
                        {
                            sayac++;
                        }
                    }
                    pikselSayilari.Add(sayac);
                    sayac = 0;
                }
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz!");
            }
        }

        private void hesaplama_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox2.Image = null;

                input_img = this.getPic();

                Bitmap griton = this.griton(input_img);
                pictureBox1.Image = griton;

                width = griton.Width;
                height = griton.Height;

                for (int x = 0; x < input_img.Width; x++)
                {
                    for (int y = 0; y < input_img.Height; y++)
                    {
                        input_clr = input_img.GetPixel(x, y);

                        pikseller.Add(input_clr.R);
                    }
                }


                for (int r = 0; r < 255; r++)
                {
                    int PikselSayisi = 0;
                    for (int s = 0; s < pikseller.Count; s++)
                    {
                        if (r == Convert.ToInt16(pikseller[s]))
                            PikselSayisi++;
                    }
                    numberP[r] = PikselSayisi;
                }

                int maxP = 0;
                for (int k = 0; k <= 255; k++)
                {
                    if (numberP[k] > maxP)
                    {
                        maxP = numberP[k];
                    }
                }

                Graphics CizimAlani;
                Pen Kalem1 = new Pen(System.Drawing.Color.Blue, 1);
                Pen Kalem2 = new Pen(System.Drawing.Color.Red, 1);
                CizimAlani = pictureBox2.CreateGraphics();

                pictureBox2.Refresh();
                int GrafikYuksekligi = pictureBox2.Height;
                double OlcekY = maxP / GrafikYuksekligi, OlcekX = 1.6;
                for (int x = 0; x <= 255; x++)
                {
                    CizimAlani.DrawLine(Kalem1, (int)(20 + x * OlcekX), GrafikYuksekligi, (int)(20 + x * OlcekX), (GrafikYuksekligi - (int)(numberP[x] / OlcekY)));
                    if (x % 50 == 0)
                        CizimAlani.DrawLine(Kalem2, (int)(20 + x * OlcekX), GrafikYuksekligi, (int)(20 + x * OlcekX), 0);

                }
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz veya Girdiğiniz Değerleri Kontrol Ediniz!");
            }
        }

        private void germe_Click(object sender, EventArgs e)
        {
            try
            {
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width, height);
                int[] histogram = new int[256];


                for (int i = 0; i < pikselSayilari.Count; i++)
                {
                    histogram[i] = Convert.ToInt32(pikselSayilari[i]);
                }

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {

                        Color renk = input_img.GetPixel(j, i);
                        int griDeger = (renk.R + renk.G + renk.B) / 3;

                        for (int m = 0; m < histogram.Length; m++)
                        {
                            if (griDeger == m)
                            {
                                histogram[m] = histogram[m] + 1;
                            }
                        }
                    }
                }
                //Burada c değeri, adeti 0 dan farklı en küçük piksel seçilir.
                for (int z = 0; z < histogram.Length; z++)
                {
                    if (histogram[z] != 0)
                    {
                        c = z;
                        break;
                    }
                }
                //d değeri,adeti 0 dan farklı en büyük piksel seçiliyor.
                for (int z = 254; z >= 0; z--)
                {
                    if (histogram[z] != 0)
                    {
                        d = z;
                        break;
                    }
                }

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Color renk = input_img.GetPixel(j, i);
                        int pgirdi = (renk.R + renk.G + renk.B) / 3;

                        //Histogram germe formülü
                        int pcikti = Convert.ToInt32((pgirdi - c) * ((gmax - gmin) / (d - c)) + gmin);
                        Color griRenk = Color.FromArgb(pcikti, pcikti, pcikti);
                        output_img.SetPixel(j, i, griRenk);
                    }
                }
                pictureBox2.Image = output_img;
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz!");
            }
        }

        private void esitleme_Click(object sender, EventArgs e)
        {
            // Her bir sayı için ( 0 – 255), piksel değeri o sayıya ait olan piksellerin toplamı bulunur.
            // Bu dizideki her bir eleman, bir önceki elemanın değeriyle toplanır.
            // Sonuçta çıkan dizideki her eleman, resimdeki piksel sayısına bölünür ve en yüksek piksel değeri ile çarpılır.
            try
            {
                /*   input_img = new Bitmap(pictureBox1.Image);
                   Bitmap griton = this.griton(input_img);
                   pictureBox1.Image = griton;

                   this.width = input_img.Width;
                   this.height = input_img.Height;
                   output_img = new Bitmap(width, height);

                   ArrayList piksels = new ArrayList();

                   for (int x = 0; x < width; x++)
                   {
                       for (int y = 0; y < height; y++)
                       {
                           input_clr = input_img.GetPixel(x, y);
                           piksels.Add(input_clr.R);                
                       }
                   }
                   int[] cdf = new int[256];
                   int[] renkler = new int[256];

                   for (int i = 0; i < 255; i++)
                   {
                       int sayac = 0;
                       for (int j = 0; j < piksels.Count; j++)
                       {
                           if (i == Convert.ToInt16(piksels[j]))
                               sayac++;
                       }
                       cdf[i] = sayac;           
                   }

                   for (int i = 0; i < cdf.Length; i++)
                   {
                    //   MessageBox.Show(Convert.ToString(cdf[i]));
                       if(i >= 1 && cdf[i] > 0)
                           cdf[i] += cdf[i - 1];
                       MessageBox.Show(Convert.ToString(cdf[i]));
                   }

                   for (int i=0; i<cdf.Length; i++)
                   {
                      renkler[i] = (cdf[i] - 1) * 255 / (width * height - 1);           
                   }

                   for (int i = 0; i < width; i++)
                   {
                       for (int j = 0; j < height; j++)
                       {
                           for(int k=0; k < renkler.Length; k++)
                           {
                               Color renk = input_img.GetPixel(i, j);
                               int pgirdi = renk.R;
                               if(pgirdi == renkler[k])
                               {
                                   Color griRenk = Color.FromArgb(renkler[k], renkler[k], renkler[k]);
                                   output_img.SetPixel(i, j, griRenk);
                               }                             
                           }                 
                       }
                   }
                   pictureBox2.Image = output_img;*/
                eesitleme();
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz!: ");
            }
        }

        private void eesitleme()
        {
            // Her bir sayı için ( 0 – 255), piksel değeri o sayıya ait olan piksellerin toplamı bulunur.
            // Bu dizideki her bir eleman, bir önceki elemanın değeriyle toplanır.
            // Sonuçta çıkan dizideki her eleman, resimdeki piksel sayısına bölünür ve en yüksek piksel değeri ile çarpılır.
            try
            {
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width, height);

                int[] histogram_r = new int[256];
                int[] histogram_g = new int[256];
                int[] histogram_b = new int[256];

                int[] cdf_r = histogram_r;
                int[] cdf_g = histogram_g;
                int[] cdf_b = histogram_b;

                uint pixels = (uint)input_img.Height * (uint)input_img.Width;
                decimal Const = 255 / (decimal)pixels;

                for (int i = 0; i < input_img.Width; i++)
                {
                    for (int j = 0; j < input_img.Height; j++)
                    {
                        input_clr = input_img.GetPixel(i, j);

                        histogram_r[(int)input_clr.R]++;
                        histogram_g[(int)input_clr.G]++;
                        histogram_b[(int)input_clr.B]++;
                    }
                }

                for (int r = 1; r <= 255; r++)
                {
                    cdf_r[r] = cdf_r[r] + cdf_r[r - 1];
                    cdf_g[r] = cdf_g[r] + cdf_g[r - 1];
                    cdf_b[r] = cdf_b[r] + cdf_b[r - 1];
                }

                for (int y = 0; y < input_img.Height; y++)
                {
                    for (int x = 0; x < input_img.Width; x++)
                    {
                        Color pixelColor = input_img.GetPixel(x, y);

                        R = Convert.ToInt32(cdf_r[pixelColor.R] * Const);
                        G = Convert.ToInt32(cdf_r[pixelColor.G] * Const);
                        B = Convert.ToInt32(cdf_r[pixelColor.B] * Const);

                        output_img.SetPixel(x, y, Color.FromArgb(R, G, B));
                    }
                }
                pictureBox2.Image = output_img;
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz!");
            }
        }

        private void gurultu_Click(object sender, EventArgs e)
        {
            try
            {
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width, height);
                Random rand = new Random();

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        int num = rand.Next(1, 75);
                        input_clr = input_img.GetPixel(i, j);
                        R = Math.Abs(Convert.ToInt32(input_clr.R) - Convert.ToInt32(input_clr.R) / num);
                        G = Math.Abs(Convert.ToInt32(input_clr.G) - Convert.ToInt32(input_clr.R) / num);
                        B = Math.Abs(Convert.ToInt32(input_clr.B) - Convert.ToInt32(input_clr.R) / num);
                         
                        output_img.SetPixel(i, j, Color.FromArgb(255,R,G,B));
                    }
                }
                this.ayarla(output_img);
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz!");
            }
        }
        private void mean_Click(object sender, EventArgs e)
        {
            try
            {
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width, height);
                
                int a, b, c, d, ee, f, g, h, l;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        try
                        {
                            if ((i + 2) <= width && (j + 2) <= height)
                            {
                                a = input_img.GetPixel(i - 1, j).R / 9;
                                b = input_img.GetPixel(i, j + 1).R / 9;
                                c = input_img.GetPixel(i + 1, j).R / 9;
                                d = input_img.GetPixel(i, j - 1).R / 9;
                                ee = input_img.GetPixel(i - 1, j + 1).R / 9;
                                f = input_img.GetPixel(i + 1, j + 1).R / 9;
                                g = input_img.GetPixel(i + 1, j - 1).R / 9;
                                h = input_img.GetPixel(i - 1, j - 1).R / 9;
                                l = input_img.GetPixel(i, j).R / 9;
                                R = Convert.ToInt32(a + b + c + d + ee + f + g + h + l);

                                a = input_img.GetPixel(i - 1, j).G / 9;
                                b = input_img.GetPixel(i, j + 1).G / 9;
                                c = input_img.GetPixel(i + 1, j).G / 9;
                                d = input_img.GetPixel(i, j - 1).G / 9;
                                ee = input_img.GetPixel(i - 1, j + 1).G / 9;
                                f = input_img.GetPixel(i + 1, j + 1).G / 9;
                                g = input_img.GetPixel(i + 1, j - 1).G / 9;
                                h = input_img.GetPixel(i - 1, j - 1).G / 9;
                                l = input_img.GetPixel(i, j).G / 9;
                                G = Convert.ToInt32(a + b + c + d + ee + f + g + h + l);

                                a = input_img.GetPixel(i - 1, j).B / 9;
                                b = input_img.GetPixel(i, j + 1).B / 9;
                                c = input_img.GetPixel(i + 1, j).B / 9;
                                d = input_img.GetPixel(i, j - 1).B / 9;
                                ee = input_img.GetPixel(i - 1, j + 1).B / 9;
                                f = input_img.GetPixel(i + 1, j + 1).B / 9;
                                g = input_img.GetPixel(i + 1, j - 1).B / 9;
                                h = input_img.GetPixel(i - 1, j - 1).B / 9;
                                l = input_img.GetPixel(i, j).B / 9;
                                B = Convert.ToInt32(a + b + c + d + ee + f + g + h + l);

                                output_img.SetPixel(i, j, Color.FromArgb(R, G, B));
                            }
                        }
                        catch { }
                    }
                }
                this.degistir(output_img);
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz!");
            }
        }
        private void medyan_Click(object sender, EventArgs e)
        {
            try
            {
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width, height);
               

                int a, b, c, d, ee, f, g, h, l;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        try
                        {
                            if ((i + 2) < width && (j + 2) < height && (i-1) >= 0 && (j-1) >= 0 )
                            {
                                ArrayList reds = new ArrayList();
                                ArrayList greens = new ArrayList();
                                ArrayList blues = new ArrayList();
                                a = Convert.ToInt32(input_img.GetPixel(i - 1, j).R);
                                b = Convert.ToInt32(input_img.GetPixel(i, j + 1).R);
                                c = Convert.ToInt32(input_img.GetPixel(i + 1, j).R);
                                d = Convert.ToInt32(input_img.GetPixel(i, j - 1).R);
                                ee = Convert.ToInt32(input_img.GetPixel(i - 1, j + 1).R);
                                f = Convert.ToInt32(input_img.GetPixel(i + 1, j + 1).R);
                                g = Convert.ToInt32(input_img.GetPixel(i + 1, j - 1).R);
                                h = Convert.ToInt32(input_img.GetPixel(i - 1, j - 1).R);
                                l = Convert.ToInt32(input_img.GetPixel(i, j).R);                 
                                reds.Add(a); reds.Add(b); reds.Add(c); reds.Add(d); reds.Add(ee); reds.Add(f);
                                reds.Add(g); reds.Add(h); reds.Add(l);
                                reds.Sort();
                                R = Convert.ToInt32(reds[4]);

                                a = Convert.ToInt32(input_img.GetPixel(i - 1, j).G);
                                b = Convert.ToInt32(input_img.GetPixel(i, j + 1).G);
                                c = Convert.ToInt32(input_img.GetPixel(i + 1, j).G);
                                d = Convert.ToInt32(input_img.GetPixel(i, j - 1).G);
                                ee = Convert.ToInt32(input_img.GetPixel(i - 1, j + 1).G);
                                f = Convert.ToInt32(input_img.GetPixel(i + 1, j + 1).G);
                                g = Convert.ToInt32(input_img.GetPixel(i + 1, j - 1).G);
                                h = Convert.ToInt32(input_img.GetPixel(i - 1, j - 1).G);
                                l = Convert.ToInt32(input_img.GetPixel(i, j).G);
                                greens.Add(a); greens.Add(b); greens.Add(c); greens.Add(d); greens.Add(ee); greens.Add(f);
                                greens.Add(g); greens.Add(h); greens.Add(l);
                                greens.Sort();
                                G = Convert.ToInt32(greens[4]);

                                a = Convert.ToInt32(input_img.GetPixel(i - 1, j).B);
                                b = Convert.ToInt32(input_img.GetPixel(i, j + 1).B);
                                c = Convert.ToInt32(input_img.GetPixel(i + 1, j).B);
                                d = Convert.ToInt32(input_img.GetPixel(i, j - 1).B);
                                ee = Convert.ToInt32(input_img.GetPixel(i - 1, j + 1).B);
                                f = Convert.ToInt32(input_img.GetPixel(i + 1, j + 1).B);
                                g = Convert.ToInt32(input_img.GetPixel(i + 1, j - 1).B);
                                h = Convert.ToInt32(input_img.GetPixel(i - 1, j - 1).B);
                                l = Convert.ToInt32(input_img.GetPixel(i, j).B);
                                blues.Add(a); blues.Add(b); blues.Add(c); blues.Add(d); blues.Add(ee); blues.Add(f);
                                blues.Add(g); blues.Add(h); blues.Add(l);
                                blues.Sort();
                                B = Convert.ToInt32(blues[4]);

                                output_img.SetPixel(i, j, Color.FromArgb(R, G, B));
                            }
                            else
                            {
                                output_img.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                            }
                        }
                        catch {
                            MessageBox.Show("hataaa");
                        }
                    }
                }
                this.degistir(output_img);
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz!");
            }
        }
        private void sobel_Click(object sender, EventArgs e)
        {
            try
            {
                griton();
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width, height);

                int z7, z8, z9, z1, z2, z3, z6, z4, gx, gy;
                int g;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        try
                        {
                            if ((i + 2) < width && (j + 2) < height && (i - 1) >= 0 && (j - 1) >= 0)
                            {
                                z1 = Convert.ToInt32(input_img.GetPixel(i - 1, j - 1).R);
                                z2 = Convert.ToInt32(input_img.GetPixel(i - 1, j).R);
                                z3 = Convert.ToInt32(input_img.GetPixel(i - 1, j + 1).R);
                                z4 = Convert.ToInt32(input_img.GetPixel(i, j - 1).R);
                                z6 = Convert.ToInt32(input_img.GetPixel(i, j + 1).R);
                                z7 = Convert.ToInt32(input_img.GetPixel(i + 1, j - 1).R);
                                z8 = Convert.ToInt32(input_img.GetPixel(i + 1, j).R);
                                z9 = Convert.ToInt32(input_img.GetPixel(i + 1, j + 1).R);

                                gx = (z7 + 2 * z8 + z9) - (z1 + 2 * z2 + z3);
                                gy = (z3 + 2 * z6 + z9) - (z1 + 2 * z4 + z7);

                                g = Convert.ToInt32(Math.Sqrt(Math.Pow(gx, 2) + Math.Pow(gy, 2)));
                                if (g > 255) g = 255;

                                output_img.SetPixel(i, j, Color.FromArgb(g, g, g));
                            }
                            else
                            {
                                output_img.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                            }
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("hataaa: "+ex);
                        }
                    }
                }
                output_img = negatif(output_img);
                this.degistir(output_img);
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz!");
            }
        }
        private void prewitt_Click(object sender, EventArgs e)
        {
            try
            {
                griton();
                input_img = this.getPic();
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width, height);

                int z7, z8, z9, z1, z2, z3, z6, z4, gx, gy;
                int g;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        try
                        {
                            if ((i + 2) < width && (j + 2) < height && (i - 1) >= 0 && (j - 1) >= 0)
                            {
                                z1 = Convert.ToInt32(input_img.GetPixel(i - 1, j - 1).R);
                                z2 = Convert.ToInt32(input_img.GetPixel(i - 1, j).R);
                                z3 = Convert.ToInt32(input_img.GetPixel(i - 1, j + 1).R);
                                z4 = Convert.ToInt32(input_img.GetPixel(i, j - 1).R);
                                z6 = Convert.ToInt32(input_img.GetPixel(i, j + 1).R);
                                z7 = Convert.ToInt32(input_img.GetPixel(i + 1, j - 1).R);
                                z8 = Convert.ToInt32(input_img.GetPixel(i + 1, j).R);
                                z9 = Convert.ToInt32(input_img.GetPixel(i + 1, j + 1).R);                         

                                gx = (z7 + z8 + z9) - (z1 +  z2 + z3);
                                gy = (z3 + z6 + z9) - (z1 +  z4 + z7);

                                g = Convert.ToInt32(Math.Sqrt(Math.Pow(gx, 2) + Math.Pow(gy, 2)));
                                if (g > 255) g = 255;

                                output_img.SetPixel(i, j, Color.FromArgb(g, g, g));
                            }
                            else
                            {
                                output_img.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("hataaa: " + ex);
                        }
                    }
                }
                output_img = negatif(output_img);
                this.degistir(output_img);
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz!");
            }
        }

        private Bitmap negatif(Bitmap resim)
        {
            try
            {
                input_img = resim;
                this.width = input_img.Width;
                this.height = input_img.Height;
                output_img = new Bitmap(width, height);
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        input_clr = input_img.GetPixel(i, j);
                        R = 255 - input_clr.R;
                        G = 255 - input_clr.G;
                        B = 255 - input_clr.B;
                        output_img.SetPixel(i, j, Color.FromArgb(R, G, B));
                    }
                }
                return output_img;
            }
            catch{ return input_img; }
        }

        public void MainTools()
        {
            ToolStrip m = new ToolStrip();
            ToolStripButton ac = new ToolStripButton();
            ToolStripButton kaydet = new ToolStripButton();

            m.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { ac, kaydet });
            m.Location = new System.Drawing.Point(0, 5000);
            m.Name = "toolStrip1";
            m.Size = new System.Drawing.Size(956, 27);
            m.TabIndex = 0;
            m.Text = "toolStrip1";
 
            ac.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
          //  ac.Image = Image.FromFile("");
            ac.ImageTransparentColor = System.Drawing.Color.Magenta;
            ac.Name = "DosyaAc_toolbar";
            ac.Size = new System.Drawing.Size(24, 24);
            ac.Text = "Dosya Aç";

            kaydet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         //   kaydet.Image = Image.FromFile("kaydet.png");
            kaydet.ImageTransparentColor = System.Drawing.Color.Magenta;
            kaydet.Name = "Kaydet_toolbar";
            kaydet.Size = new System.Drawing.Size(24, 24);
            kaydet.Text = "Kaydet";
          
            m.Name = "ToolsMain";
            m.Dock = DockStyle.Top;
            m.Items.Add(ac);
            m.Items.Add(kaydet);

            this.Controls.Add(m);

            ac.Click += ac_Click;
            kaydet.Click += kaydet_Click;
        }
        private void ac_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.InitialDirectory = @"C:\";
            od.Filter = "resim dosyaları|*.bmp;*.jpg;*.png";
            od.Multiselect = false;
            od.FilterIndex = 2;
            if (od.ShowDialog() == DialogResult.OK)
            {
                this.ayarla(Image.FromFile(od.FileName));
                this.label1.Text = "Resim başarılı bir şekilde yüklendi. Lütfen menüden seçenekleri seçiniz.";
            }
        }
        private void kaydet_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Jpeg Resmi|*.jpg|Bitmap Resmi|*.bmp|Gif Resmi|*.gif";
            saveFileDialog1.Title = "Resmi Kaydet";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "") //Dosya adı boş değilse kaydedecek.
            { 
                FileStream DosyaAkisi = (FileStream)saveFileDialog1.OpenFile();

                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        pictureBox2.Image.Save(DosyaAkisi, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        pictureBox2.Image.Save(DosyaAkisi, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        pictureBox2.Image.Save(DosyaAkisi, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }
                DosyaAkisi.Close();
            }
        }

        private Bitmap griton(Bitmap resim)
        {
            try
            {
                input_img = resim;
                this.width = resim.Width;
                this.height = resim.Height;
                output_img = new Bitmap(width, height);
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        input_clr = input_img.GetPixel(i, j);
                        R = Convert.ToInt32(0.2989 * input_clr.R + 0.5870 * input_clr.G + 0.1140 * input_clr.B);
                        G = Convert.ToInt32(0.2989 * input_clr.R + 0.5870 * input_clr.G + 0.1140 * input_clr.B);
                        B = Convert.ToInt32(0.2989 * input_clr.R + 0.5870 * input_clr.G + 0.1140 * input_clr.B);
                        output_img.SetPixel(i, j, Color.FromArgb(R, G, B));
                    }
                }
                return output_img;
            }
            catch
            {
                MessageBox.Show("Lütfen Fotoğraf Seçiniz!");
                return input_img;
            }
        }
    }
}