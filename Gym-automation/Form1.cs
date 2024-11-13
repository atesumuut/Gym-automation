using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace SPOR
{
    public partial class Form1 : Form
    {
        Form2 frm = new Form2();
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection baglinti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=spor.accdb");
        OleDbCommand komut;
        OleDbDataAdapter adtr;
        DataTable tablo = new DataTable();       
        private void button1_Click(object sender, EventArgs e)
        {
            double faktor1,faktor2;
            String cinsiyet = Convert.ToString(comboBox1.Text);

            if (cinsiyet == "Erkek")
            {
                faktor1 = ((((Convert.ToDouble(textBox1.Text) - 152.4) / 2.54) * 2.3) + 50);
                textBox3.Text = faktor1.ToString();
            }
            else
            {
                faktor2 = ((((Convert.ToDouble(textBox1.Text) - 152.4) / 2.54) * 2.3) + 45.5);
                textBox3.Text = faktor2.ToString();
            }
            baglinti.Open();
            komut = new OleDbCommand("INSERT INTO spor (ad_ve_soyad,boy,ideal_kilo,cinsiyet) values ('" + textBox2.Text + "','" + textBox1.Text + "','" + textBox3.Text + "','" + comboBox1.Text + "') ", baglinti);
            komut.ExecuteNonQuery();
            baglinti.Close();

            frm.Show();
            this.hide();
        }
        private void hide()
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            frm.Show();
            this.hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
