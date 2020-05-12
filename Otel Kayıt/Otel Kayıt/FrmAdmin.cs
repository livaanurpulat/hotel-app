using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otel_Kayıt
{
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox3.Text = textBox3.Text.Substring(1) + textBox3.Text.Substring(0, 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "Admin" && textBox2.Text == "12345")
            {
                Form1 fr = new Form1();
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş Yaptınız Lütfen Tekrar Deneyin!");
                textBox1.Clear();
                textBox2.Clear();
            }
        }
    }
}
