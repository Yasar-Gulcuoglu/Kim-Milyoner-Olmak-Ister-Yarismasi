using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;


namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        string _ad;
        string _soyad;
        public Form2(string ad,string soyad)
        {   _ad = ad; _soyad = soyad;
            InitializeComponent();
            this.Load += Form1_Load; // Form yüklendiğinde Form1_Load metodunu çağır
            
            SayiUret();
            soruCek();           
        }
        private System.Windows.Forms.Button clickedButton; // Sınıf düzeyinde clickedButton tanımlandı
        private void Form1_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde tüm butonları kontrol et ve Click olayına Button_Click metodunu bağla
            foreach (Control c in this.Controls)
            {
                if (c is System.Windows.Forms.Button)
                {
                    ((System.Windows.Forms.Button)c).Click += Button_Click;
                }
            }
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database7.mdb");
        
        List<int> numbers = new List<int>();
        private void SayiUret()
        {
            HashSet<int> uniqueNumbers = new HashSet<int>();
            Random rnd = new Random();
            while (numbers.Count < 20)
            {
                int sayi = rnd.Next(1, 21);
                if (uniqueNumbers.Add(sayi))
                {
                    numbers.Add(sayi);
                }
            }
            baglanti.Open();
        }
        int sayi = -1;
        int buttonIndex = -1;
        int para = -50000;
        int saniye = 168;
        
 
        SoundPlayer dogru = new SoundPlayer(Resource1.dogru);
        SoundPlayer yanlis = new SoundPlayer(Resource1.yanlış);
        SoundPlayer sure = new SoundPlayer(Resource1.sure);
        private void soruCek()

        {   // Para butonlarının adını değiştiriyor bu sayede butonlar yukarıya doğru ilerliyor
            System.Windows.Forms.Button[] buttons = new System.Windows.Forms.Button[20];
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = this.Controls.Find("button" + (i + 1), true).FirstOrDefault() as System.Windows.Forms.Button;
            }
            buttonIndex += 1;
            sayi += 1;
            para += 50000;
            if (sayi == 20)
            {
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database3.mdb");
                OleDbCommand cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO bilgiler (Ad, Soyad, Para) VALUES (@ad, @soyad, @para)";
                cmd.Parameters.AddWithValue("@ad", _ad);
                cmd.Parameters.AddWithValue("@soyad", _soyad);
                cmd.Parameters.AddWithValue("@para", para);
                cmd.ExecuteNonQuery();
                con.Close();
                timer1.Stop();
                MessageBox.Show("Tebrikler "+_ad+" "+_soyad+", yeni milyonerimiz sizsiniz!");
                this.Hide();
                Form3 form3 = new Form3();
                form3.Show();

            }
            else
            {
                OleDbCommand komut = new OleDbCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT * FROM sorular WHERE Sayı = @sayi";
                komut.Parameters.AddWithValue("@sayi", numbers[sayi]);
                OleDbDataReader oku = komut.ExecuteReader();
                
                if (oku.Read())
                {   
                    richTextBox1.Text = oku["Soru"].ToString();
                    SoundPlayer[] sorular = new SoundPlayer[]
{
                    new SoundPlayer(Resource1.il),
                    new SoundPlayer(Resource1.gol),
                    new SoundPlayer(Resource1.Suyun_kaldırma_kuvve),
                    new SoundPlayer(Resource1.Türkiye_nin_en_fazla),
                    new SoundPlayer(Resource1.Mercekler_ışığın_han),
                    new SoundPlayer(Resource1.Mala),
                    new SoundPlayer(Resource1.Kimler_kepenek_giyer),
                    new SoundPlayer(Resource1.Osmanlı_Devletinin_k),
                    new SoundPlayer(Resource1.Çanakkale_Savaşı_sır),
                    new SoundPlayer(Resource1.Yazları_sıcak_ve_kur),
                    new SoundPlayer(Resource1.bolge),
                    new SoundPlayer(Resource1.Türkçe_hangi_dil_gru),
                    new SoundPlayer(Resource1.Aşağıdaki_illerden_h),
                    new SoundPlayer(Resource1.Gülü_ile_meşhur_olan),
                    new SoundPlayer(Resource1.Aşağıdaki_dağlardan),
                    new SoundPlayer(Resource1.Aşağıdakilerden_hang),
                    new SoundPlayer(Resource1.Duvara_asılı_bir_har),
                    new SoundPlayer(Resource1.Pirinç_hangi_ürünün),
                    new SoundPlayer(Resource1.rumeli),
                    new SoundPlayer(Resource1.gezegen),
                };

                    for (int i = 1; i <= 20; i++)
                    {
                        if (oku["Sayı"].ToString() == i.ToString())
                        {
                            

                            // Doğru 'soru' nesnesini kullanın
                            SoundPlayer soruSound = sorular[i - 1];

                            // Ses dosyasını çal
                            soruSound.Play();

                            // Eğer eşleşme bulunduysa döngüyü sonlandır
                            break;
                        }
                    }

                    List<int> numbers = new List<int>();
                    HashSet<int> uniqueNumbers = new HashSet<int>();
                    Random rnd = new Random();
                    while (numbers.Count < 4)
                    {
                        int sayi = rnd.Next(1, 5);
                        if (uniqueNumbers.Add(sayi))
                        {
                            numbers.Add(sayi);
                        }
                    }
                    button21.Text = oku["Button" + numbers[0]].ToString();
                    button22.Text = oku["Button" + numbers[1]].ToString();
                    button23.Text = oku["Button" + numbers[2]].ToString();
                    button24.Text = oku["Button" + numbers[3]].ToString();
                    button21.BackColor = SystemColors.Control;
                    button22.BackColor = SystemColors.Control;
                    button23.BackColor = SystemColors.Control;
                    button24.BackColor = SystemColors.Control;
                }
                else
                {
                    richTextBox1.Text = "Soru bulunamadı.";
                }
            }
            if (buttonIndex < buttons.Length)
            {
                // Butonun adını değiştir
                buttons[buttonIndex].Name = "button" + (buttonIndex).ToString();
                buttons[buttonIndex].BackColor = Color.Green;
            }
            baglanti.Close(); //
        }
        private void Button_Click(object sender, EventArgs e)
        {
            baglanti.Open(); //
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = "SELECT * FROM sorular WHERE Sayı = @sayi";
            komut.Parameters.AddWithValue("@sayi", numbers[sayi]);
            OleDbDataReader oku = komut.ExecuteReader();
            // Hangi butonun tıklandığını bul
            clickedButton = sender as System.Windows.Forms.Button;
            if (clickedButton != null)
            {
                if (oku.Read())
                {
                    if ((clickedButton.Text) == (oku["Button4"]).ToString())
                    {
                        button21.BackColor = Color.Red;
                        button22.BackColor = Color.Red;
                        button23.BackColor = Color.Red;
                        button24.BackColor = Color.Red;
                        clickedButton.BackColor = Color.Green;
                        dogru.Play();
                        Task.Delay(4000).Wait();
                        soruCek();
                        saniye = 168;
                    }
                    else
                    {
                        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database3.mdb");
                        OleDbCommand cmd = new OleDbCommand();
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "INSERT INTO bilgiler (Ad, Soyad, Para) VALUES (@ad, @soyad, @para)";
                        cmd.Parameters.AddWithValue("@ad", _ad);
                        cmd.Parameters.AddWithValue("@soyad", _soyad);
                        cmd.Parameters.AddWithValue("@para", para);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        button21.BackColor = Color.Red;
                        button22.BackColor = Color.Red;
                        button23.BackColor = Color.Red;
                        button24.BackColor = Color.Red;
                        clickedButton.BackColor = Color.Red;
                        foreach (Control control in Controls)
                        {
                            if (control is System.Windows.Forms.Button button && button.Text == oku["Button4"].ToString())
                            {
                                // Bulunan butonun arka plan rengini yeşil olarak ayarla
                                button.BackColor = Color.Green;
                            }                                                      
                        }
                        yanlis.Play();
                        timer1.Stop();
                        Task.Delay(1000).Wait();
                        MessageBox.Show("Kaybettiniz, " + _ad +" "+_soyad+" kazandığınız para: " + para + " tl");
                        this.Hide();
                        Form3 form3 = new Form3();
                        form3.Show();
                    }
                }
            }
            baglanti.Close(); //
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {


            timer1.Interval = 100;
            saniye = saniye - 1;
            button25.Text = Convert.ToString(saniye);

            if (113 < saniye && saniye < 168)
            {
                button25.BackColor = Color.Blue;
            }
            else if (58 < saniye && saniye < 113)
            {
                button25.BackColor = Color.Pink;
            }
            else if (1 < saniye && saniye < 58)
            {
                button25.BackColor = Color.Purple;

            }
            else if (saniye <= 0)
            {
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database3.mdb");
                OleDbCommand cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO bilgiler (Ad, Soyad, Para) VALUES (@ad, @soyad, @para)";
                cmd.Parameters.AddWithValue("@ad", _ad);
                cmd.Parameters.AddWithValue("@soyad", _soyad);
                cmd.Parameters.AddWithValue("@para", para);
                cmd.ExecuteNonQuery();
                con.Close();
                timer1.Stop();
                button25.BackColor = Color.Red;
                sure.Play();
                MessageBox.Show("Süre doldu, " + _ad + " " + _soyad + " kazandığınız para: " + para + " tl");
                this.Hide();
                Form3 form3 = new Form3();
                form3.Show();
            }
            baglanti.Close();

        }

    }
}
