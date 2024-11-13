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
    public partial class Form2 : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=spor.accdb");
        OleDbConnection baglanti_olculer = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=olculerr.accdb");
        DataTable tablo = new DataTable();
        DataSet veriseti = new DataSet();
        int id = 0;
        private void listele()
        {
            OleDbDataAdapter adaptor = new OleDbDataAdapter("select * from spor", baglanti);
            adaptor.Fill(veriseti, "spor");
            dataGridView1.DataSource = veriseti.Tables[0];
            dataGridView1.Columns[0].HeaderText = "kimlik";
            dataGridView1.Columns[1].HeaderText = "ad ve soyad";
            dataGridView1.Columns[2].HeaderText = "boy";
            dataGridView1.Columns[3].HeaderText = "ideal kilo";
            dataGridView1.Columns[4].HeaderText = "cinsiyet";
        }
        public void ara()
        {
            veriseti.Clear();
            OleDbDataAdapter adaptor = new OleDbDataAdapter("select * from spor  where cinsiyet like '%" + textBox2.Text.ToUpper() + "%' or ad_ve_soyad like '%" + textBox2.Text + "%' ", baglanti);
            adaptor.Fill(veriseti, "spor");
            dataGridView1.DataSource = veriseti.Tables["spor"];
            dataGridView1.Columns[0].HeaderText = "kimlik";
            dataGridView1.Columns[1].HeaderText = "ad ve soyad";
            dataGridView1.Columns[2].HeaderText = "boy";
            dataGridView1.Columns[3].HeaderText = "ideal kilo";
            dataGridView1.Columns[4].HeaderText = "cinsiyet";
        }
        public Form2()
        {
            InitializeComponent();   
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            baglanti_olculer.Open();
            listele();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            double idealkilo = Convert.ToDouble(textBox1.Text), suanki = Convert.ToDouble(textBox4.Text), sonuc, sonuc2;
            sonuc = suanki - idealkilo;

            if (suanki < idealkilo)
            {
                label4.Text = "almalısın";
                textBox5.Text = (-1 * sonuc).ToString();
            }
            else
            {
                label4.Text = "vermelisin";
                textBox5.Text = sonuc.ToString();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3(id);
            frm.Show();
            this.Hide();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void button4_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = "update spor set ad_ve_soyad ='" + textBox2.Text + "',boy='" + maskedTextBox1.Text + "',cinsiyet='" + textBox3.Text + "' where ideal_kilo like '" + textBox1.Text + "' ";
            komut.ExecuteNonQuery();
            veriseti.Clear();
            listele();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            new OleDbCommand("delete from spor where kimlik = " + id, baglanti).ExecuteNonQuery();
            new OleDbCommand("delete from olculerr where Kullanici_id = " + id, baglanti_olculer).ExecuteNonQuery();
            
            veriseti.Clear();
            listele();     
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ara();
        }
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            baglanti.Close();
            baglanti_olculer.Close();
            base.OnFormClosing(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
