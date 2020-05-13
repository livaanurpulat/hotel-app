/****************************************************************************
**                         SAKARYA ÜNİVERSİTESİ
**              BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
**               BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
**                  NESNEYE DAYALI PROGRAMLAMA DERSİ
**                       2019-2020 BAHAR DÖNEMİ
**
**                    PROJE NUMARASI.........: 01
**                    ÖĞRENCİ ADI............:Liva Nur PULAT
**                    ÖĞRENCİ NUMARASI.......:B191200371
**                    DERSİN ALINDIĞI GRUP...: A
****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Otel_Kayıt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //veri tabanı bağlantısı ekleme.
        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=otel;Integrated Security=True");

        //erişim belirleyici oluşturma.
        private void verilerigoster()
        {
            //eski kayıtlarla yeni kayıtların (silme, güncelleme) üst üste gözükmemesi için temizleme.
            listView1.Items.Clear();
            //bağlantı açma.
            baglanti.Open();
            //veri tabanındaki musteriler tablosundan tüm alanları seçme komutu.
            SqlCommand komut = new SqlCommand("Select * From musteriler", baglanti);
            //bağlantı veri tabanından komutları okuduğu sürece parametreleri döndür.
            SqlDataReader oku = komut.ExecuteReader();

            //veri tabanındaki verileri okudukça sırayla listview'e aktarma için while döngüsü.
            while(oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add(oku["Ad"].ToString());
                ekle.SubItems.Add(oku["Soyad"].ToString());
                ekle.SubItems.Add(oku["OdaNo"].ToString());
                ekle.SubItems.Add(oku["GTarih"].ToString());
                ekle.SubItems.Add(oku["Telefon"].ToString());
                ekle.SubItems.Add(oku["Hesap"].ToString());
                ekle.SubItems.Add(oku["CTarih"].ToString());

                listView1.Items.Add(ekle);
            }
            baglanti.Close();
        }
        //kayıtları listele butonu
        private void button1_Click(object sender, EventArgs e)
        {
            //kayıtları listeleye tıklandığında listview'de verileri gösterir.
            verilerigoster();
        }

        //kaydet butonu
        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            //form üzerinden yeni kayıt eklendiğinde musteriler tablosuna kayıtları ekler.
            SqlCommand komut = new SqlCommand("insert into musteriler (id,Ad,Soyad,OdaNo,GTarih,Telefon,Hesap,CTarih) values ('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + comboBox2.Text.ToString() + "','" + dateTimePicker1.Text.ToString() + "','" + textBox5.Text.ToString() + "','" + textBox6.Text.ToString() + "','" + dateTimePicker2.Text.ToString() + "')", baglanti);
            //parametreleri geri döndürür.
            komut.ExecuteNonQuery();
            //oda no'da seçilen odayı doluoda tablosuna ekler.
            komut.CommandText = "insert into doluoda(doluyerler) values ('" + comboBox2.Text + "')";
            komut.ExecuteNonQuery();
            //seçilen odayı bosoda tablosundan siler.
            komut.CommandText = ("Delete from bosoda where bosyerler ='" + comboBox2.Text + "'");
            komut.ExecuteNonQuery();

           
            baglanti.Close();
            verilerigoster();
            //kayıtlardan sonra textboxlar temzilensin.
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        //silme komutu id ile yapılacak id değişkeni tanımlama.
        int id = 0;
        //silme butonu.
        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            //silme işleminde musteriler adlı tablodan kayıtları id'ye göre silme komutu.
            SqlCommand komut = new SqlCommand("Delete From musteriler where id= (" + id + ")", baglanti);
            komut.ExecuteNonQuery();
            //silme durumunda silinen kayıttaki odayı bosoda tablosuna ekler.
            komut.CommandText = "insert into bosoda(bosyerler) values ('" + comboBox2.Text + "')";
            komut.ExecuteNonQuery();
            //silinen kayıttaki odayı doluoda tablosundan siler.
            komut.CommandText = ("Delete from doluoda where doluyerler ='" + comboBox2.Text + "'");
            komut.ExecuteNonQuery();
            

            baglanti.Close();
            verilerigoster();
        }

        //id'ye çift tıklama olayı.
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].Text);

            //id'ye tıklandığında ilgili kayıtları ilgili alanlara gönderir.
            textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
            comboBox2.Text = listView1.SelectedItems[0].SubItems[3].Text;
            dateTimePicker1.Text = listView1.SelectedItems[0].SubItems[4].Text;
            textBox5.Text = listView1.SelectedItems[0].SubItems[5].Text;
            textBox6.Text = listView1.SelectedItems[0].SubItems[6].Text;
            dateTimePicker2.Text = listView1.SelectedItems[0].SubItems[7].Text;
        }

        //güncelleme butonu.
        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            // veri tabanındaki musteriler adlı tablodaki alanları güncelleme komutu.
            SqlCommand komut = new SqlCommand("update musteriler set id='" + textBox1.Text.ToString() + "',Ad='" + textBox2.Text.ToString() + "',Soyad='" + textBox3.Text.ToString() + "',OdaNo='" + comboBox2.Text.ToString() + "',GTarih='" + dateTimePicker1.Text.ToString() + "',Telefon='" + textBox5.Text.ToString() + "',Hesap='" + textBox6.Text.ToString() + "',CTarih='" + dateTimePicker2.Text.ToString() + "' where id =" + id + "", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            verilerigoster();
        }

        //arama butonu.
        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            baglanti.Open();
            //musteriler adlı tablodaki tüm alanlarda aranması için komut. Ad'a göre arama yapılır.
            SqlCommand komut = new SqlCommand("Select * From musteriler where Ad like '%" + textBox7.Text + "%'", baglanti);
            SqlDataReader oku = komut.ExecuteReader();

            //yapılan aramada tüm alanlara verilerin gelmesi için while döngüsü.
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add(oku["Ad"].ToString());
                ekle.SubItems.Add(oku["Soyad"].ToString());
                ekle.SubItems.Add(oku["OdaNo"].ToString());
                ekle.SubItems.Add(oku["GTarih"].ToString());
                ekle.SubItems.Add(oku["Telefon"].ToString());
                ekle.SubItems.Add(oku["Hesap"].ToString());
                ekle.SubItems.Add(oku["CTarih"].ToString());

                listView1.Items.Add(ekle);
            }
            baglanti.Close();
        }

        //kayan yazı için.
        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox4.Text = textBox4.Text.Substring(1) + textBox4.Text.Substring(0, 1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From bosoda", baglanti);
            SqlDataReader oda = komut.ExecuteReader();
            while (oda.Read())
            {
                comboBox2.Items.Add(oda[0].ToString());
            }
            baglanti.Close();
        }
    }
}
