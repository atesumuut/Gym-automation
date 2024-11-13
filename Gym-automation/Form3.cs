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
    public partial class Form3 : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=olculerr.accdb");
        int kullanici_id = -1;
        bool is_new = true;

        public Form3(int id)
        {
            InitializeComponent();
            baglanti.Open();

            kullanici_id = id;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (is_new == true)
            {
            new OleDbCommand(
                    "INSERT INTO olculerr (Kullanici_id, Kol_olcunuz, Bilek_olcunuz, Bacak_olcunuz, Omuz_olcunuz, Gogus_olcunuz, Bel_olcunuz) values(" + kullanici_id + ", '" + textBox1.Text + "', '" + textBox3.Text + "', '" + textBox5.Text + "', '" + textBox7.Text + "', '" + textBox8.Text + "', '" + textBox9.Text + "' ) ",
                    baglanti
                ).ExecuteNonQuery();
            }
            else
            {
            new OleDbCommand(
                    "update olculerr set Kol_olcunuz ='" + textBox1.Text + "', Bilek_olcunuz = '" + textBox3.Text + "', Bacak_olcunuz = '" + textBox5.Text + "', Omuz_olcunuz = '" + textBox7.Text + "', Gogus_olcunuz = '" + textBox8.Text + "', Bel_olcunuz = '" + textBox9.Text + "' WHERE Kullanici_id = " + kullanici_id,
                    baglanti
                ).ExecuteNonQuery();
            }
        }


        private void Form3_Load(object sender, EventArgs e)
        {
            OleDbCommand command = new OleDbCommand(
                    "SELECT * FROM olculerr WHERE Kullanici_id = " + kullanici_id,
                    baglanti
                );
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                is_new = false;
                textBox1.Text = reader.GetValue(2).ToString();
                textBox3.Text = reader.GetValue(3).ToString();
                textBox5.Text = reader.GetValue(4).ToString();
                textBox7.Text = reader.GetValue(5).ToString();
                textBox8.Text = reader.GetValue(6).ToString();
                textBox9.Text = reader.GetValue(7).ToString();
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            baglanti.Close();
            base.OnFormClosing(e);
        }
    }
}
