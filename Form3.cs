using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace IMAGE_PROCCESSING
{
    public partial class Form3 : Form
    {
        Bitmap input_img, output_img;
        Color input_clr;
        int R = 0, G = 0, B = 0;
        int width, height;
        Image img;
        ArrayList pikselSayilari = new ArrayList();
        ArrayList pikseller = new ArrayList();
        int[] numberP = new int[256];
        int c, d;
        int gmax = 255;
        int gmin = 0;

        public Form3(Image img)
        {
            this.img = img;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.InitialDirectory = @"C:\";
            od.Filter = "resim dosyaları|*.bmp;*.jpg;*.png";
            od.Multiselect = false;
            od.FilterIndex = 2;
            if (od.ShowDialog() == DialogResult.OK)
            {
                this.ayarla(Image.FromFile(od.FileName));
            }
        }
        public void ayarla(Image resim)
        {
            pictureBox1.Image = resim;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.pictureBox1.Image = img;
            this.MainTools();
            this.MainMenu();
        }

        public void MainMenu()
        {
            MenuStrip m = new MenuStrip();
            ToolStripMenuItem hesaplama = new ToolStripMenuItem("Histogram Grafiği");
            ToolStripMenuItem germe = new ToolStripMenuItem("Histogram Germe");
            ToolStripMenuItem esitleme = new ToolStripMenuItem("Histogram Eşitleme");

            //MenuStript options
            m.Name = "MenuMain";
            m.Dock = DockStyle.Top;
            m.Items.Add(hesaplama);
            m.Items.Add(germe);
            m.Items.Add(esitleme);

            //form controls add
            this.Controls.Add(m);

            //Click menu control
            hesaplama.Click += hesaplama_Click;
            germe.Click += germe_Click;
            esitleme.Click += esitleme_Click;
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

                for(int i=0; i < width; i++)
                {
                    for(int j=0; j < height; j++)
                    {
                        pikseller.Add(griton.GetPixel(i,j).R);
                    }
                }

                int sayac = 0;
                for(int i=pikseller.Count; i>0; i--)
                {
                    for(int j=0; j<pikseller.Count; j++)
                    {
                        if(Convert.ToInt32(pikseller[j]) == Convert.ToInt32(pikseller[i]))
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
                int GrafikYuksekligi = pictureBox2.Height ;
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
                
                
                for(int i=0; i<pikselSayilari.Count; i++)
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

        public Bitmap getPic()
        {
            return new Bitmap(pictureBox1.Image);
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
            ac.Image = Image.FromFile("ac.png");
            ac.ImageTransparentColor = System.Drawing.Color.Magenta;
            ac.Name = "DosyaAc_toolbar";
            ac.Size = new System.Drawing.Size(24, 24);
            ac.Text = "Dosya Aç";

            kaydet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            kaydet.Image = Image.FromFile("kaydet.png");
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
    }
}