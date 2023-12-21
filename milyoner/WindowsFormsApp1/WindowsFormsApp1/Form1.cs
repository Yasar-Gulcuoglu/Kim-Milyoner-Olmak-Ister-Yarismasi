using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        SoundPlayer acilis = new SoundPlayer(Resource1.acilis);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if((textBox1.Text != "") && (textBox2.Text != "")) 
            {
                string Ad = textBox1.Text.ToLower();
                string Soyad = textBox2.Text.ToLower();
                this.Hide();
                acilis.PlaySync();
                Thread.Sleep(1000);
                Form2 form2 = new Form2(Ad,Soyad);
                form2.Show();


            }
            else
            {
                MessageBox.Show("Alanlar Boş Olamaz");
            }


        }


    }
}
