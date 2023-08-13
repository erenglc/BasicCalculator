using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicCalculator
{
    public partial class Form1 : Form
    {
        private double sonuc;

        private const string dosyaAdi = "sonuc.txt";

        public Form1()
        {
            InitializeComponent();
            OkuSonuc();
        }

        private void OkuSonuc()
        {
            if (File.Exists(dosyaAdi))
            {
                string sonucMetin = File.ReadAllText(dosyaAdi);
                double.TryParse(sonucMetin, out sonuc);
                textBox3.Text = sonucMetin;
            }
        }

        private void KaydetSonuc()
        {
            File.WriteAllText(dosyaAdi, sonuc.ToString());
        }

        private void HesaplaVeGoster(Func<double, double, double> hesaplamaFunc)
        {
            double sayi1 = Convert.ToDouble(textBox1.Text);
            double sayi2 = Convert.ToDouble(textBox2.Text);

            for (int i = 0; i <= 100; i++)
            {
                progressBar1.Invoke((MethodInvoker)delegate
                {
                    progressBar1.Value = i;
                });

                Thread.Sleep(10);
            }

            textBox3.Invoke((MethodInvoker)delegate
            {
                if (sayi2 != 0)
                {
                    sonuc = hesaplamaFunc(sayi1, sayi2);
                    textBox3.Text = sonuc.ToString();
                    KaydetSonuc(); // Sonucu dosyaya kaydet
                }
                else
                {
                    MessageBox.Show("Bölme işleminde payda sıfır olamaz.");
                }
            });

            progressBar1.Invoke((MethodInvoker)delegate
            {
                progressBar1.Value = 0;
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HesaplaVeGoster((a, b) => a + b);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HesaplaVeGoster((a, b) => a - b);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HesaplaVeGoster((a, b) => a * b);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HesaplaVeGoster((a, b) => b != 0 ? a / b : 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
        }
    }
}
