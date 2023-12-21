using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {

            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database3.mdb");
        private void Form3_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = @"
                    SELECT Ad, Soyad, Para
                    FROM bilgiler b1
                    WHERE Para = (
                        SELECT MAX(Para)
                        FROM bilgiler b2
                        WHERE b1.Ad = b2.Ad AND b1.Soyad = b2.Soyad
                    )
                    ORDER BY Para DESC"; // Para değeri en yüksekten en düşüğe sırala

            OleDbDataReader oku = komut.ExecuteReader();
            int sayi = 0;

            while (oku.Read())
            {
                sayi += 1;
                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row.Cells[0].Value = sayi.ToString();
                row.Cells[1].Value = oku["Ad"].ToString();
                row.Cells[2].Value = oku["Soyad"].ToString();
                row.Cells[3].Value = oku["Para"].ToString();

                dataGridView1.Rows.Add(row);
            }
            baglanti.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        } 
    }
    
}
