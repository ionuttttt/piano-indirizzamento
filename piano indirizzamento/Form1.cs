using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace piano_indirizzamento
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double bitRiservatiSottoreti = 0, bitRiservatiHost = 0, subnetm = 0, subnetm2 = 0, subnetm3 = 0;
        string classe = "", sub = "";

        public void BitHost()
        {
            int host = Convert.ToInt32(textBox2.Text);
            bitRiservatiHost = Math.Ceiling(Math.Log(host + 2, 2)); //BIT PARTE HOST
        }

        public void BitSottorete()
        {
            int sottoreti = Convert.ToInt32(textBox1.Text);
            bitRiservatiSottoreti = Math.Ceiling(Math.Log(sottoreti, 2)); //BIT PARTE SOTTORETE
        }

        public void calcoloClasse()
        {
            //CALCOLO CLASSE SOTTORETI
            BitHost();
            BitSottorete();

            string IpBase = "";

            if (bitRiservatiHost + bitRiservatiSottoreti <= 8) //CLASSE C
            {
                IpBase = "192.168.0.0";
                classe = "C";
                listBox1.Items.Add("Classe C: " + IpBase);
            }
            else if (bitRiservatiHost + bitRiservatiSottoreti <= 16 && bitRiservatiHost + bitRiservatiSottoreti > 8) //CLASSE B
            {
                IpBase = "172.16.0.0";
                classe = "B";
                listBox1.Items.Add("Classe B: " + IpBase);
            }
            else //CLASSE A
            {
                IpBase = "10.0.0.0";
                classe = "A";
                listBox1.Items.Add("Classe A: " + IpBase);
            }
        }

        public void Subnet(double bit_sotto)
        {
            double n = bit_sotto;

            for (int i = 0; i < n; i++)
            {
                sub += "1";
            }

            for (double i = n; i < 8; i++)
            {
                sub += "0";
            }

            double valore = 0;
            double elevazione = 7;

            for (int i = 0; i < sub.Length; i++)
            {
                double bit = Convert.ToInt32(sub[i]);

                if (sub[i] == '1')
                {
                    valore += Math.Pow(2, elevazione);
                }

                elevazione--;
            }

            if (classe == "C")
            {
                listBox1.Items.Add("Subnet mask: 255.255.255." + valore);
            }

            else if (classe == "B")
            {
                listBox1.Items.Add("Subnet mask: 255.255." + valore + ".0");
            }

            else
            {
                listBox1.Items.Add("Subnet mask: 255." + valore + ".0.0");
            }

        }

        public void CIDR()
        {
            int cidr = 0;

            for (int i = 0; i < 8; i++)
            {
                if (sub[i] == '1')
                {
                    cidr += 1;
                }
            }

            if (classe == "C")
            {
                cidr += 24;
            }
            else if (classe == "B")
            {
                cidr += 16;
            }
            else
            {
                cidr += 8;
            }

            listBox1.Items.Add("Il cidr è: /" + cidr);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            calcoloClasse();
            Subnet(bitRiservatiSottoreti);
            CIDR();
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}
