using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime;
using System.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace lyrics
{
    public partial class Form1 : Form
    {
        private float font = 16;
        private double sec = 5;
        public Form1()
        {
            InitializeComponent();
            listBox3.Items.Clear();
            DirectoryInfo path = new DirectoryInfo(@"C:\我的東東\노래 ~");

            foreach (var filename in path.GetDirectories())
            {
                listBox3.Items.Add(filename.Name);
            }

            result1.Font = new Font(new FontFamily(result1.Font.Name), 16, result1.Font.Style);
            textBox1.Text = "16";
            result1.Multiline = true;
            result1.ScrollBars = ScrollBars.Vertical;

            string constr = "Data Source=LAPTOP-5FC3MHVF\\SQLEXPRESS;Initial Catalog=master;Integrated Security=TRUE;";
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            listBox1.Items.Clear();
            string sql = "select singer from GG Union select singer from GG";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader song = com.ExecuteReader();
            while (song.Read())
            {
                listBox1.Items.Add(new song(song.GetString(0)));
            }

            song.Close();

            sql = "select * from GG order by singer, song";
            com = new SqlCommand(sql, con);
            song = com.ExecuteReader();
            int num = 0;
            while (song.Read())
            {
                num++;
                dataGridView1.Rows.Add(song.GetString(0), song.GetString(1), song.GetString(2));
            }
            label6.Text = "有" + num + "首歌的歌詞：";
            song.Close();
            con.Close();

            axWindowsMediaPlayer1.settings.volume = 35;
        }

        private void add_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void result1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
               
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Artist.Text == "")
            {
                MessageBox.Show("未選擇刪除物件!");
            }
            else
            {
                Form4 form4 = new Form4();
                form4.String1 = Artist.Text;
                form4.SetValue();
                form4.Show();
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            Artist.Items.Clear();
            string constr = "Data Source=LAPTOP-5FC3MHVF\\SQLEXPRESS;Initial Catalog=master;Integrated Security=TRUE;";
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            label3.Text = listBox1.Text;
            label3.Text = label3.Text.Replace("&", "&&");
            string sql = "select singer, song from GG where singer=N'" + listBox1.Text + "' order by song ASC";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader song = com.ExecuteReader();
            int S = 0;
            while (song.Read())
            {
                S++;
                Artist.Items.Add(new song(song.GetString(1)));
            }
            label8.Text = S + "首歌：";

            song.Close();
            con.Close();
            Artist.SelectedIndex = 0;
            listBox3.Text = listBox1.Text;
        }

        private void Artist_SelectedIndexChanged(object sender, EventArgs e)
        {
            string constr = "Data Source=LAPTOP-5FC3MHVF\\SQLEXPRESS;Initial Catalog=master;Integrated Security=TRUE;";
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            label2.Text = listBox1.Text + " - " + Artist.Text + " 가사";
            label2.Text = label2.Text.Replace("&", "&&");
            string sql = "select * from GG where song=N'" + Artist.Text + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader lyr = com.ExecuteReader();
            while (lyr.Read())
            {
                result1.Text = lyr.GetString(2);
            }
            lyr.Close();
            con.Close();
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.axWindowsMediaPlayer1.URL = string.Format(@"C:\我的東東\노래 ~\" + listBox3.Text + @"\" + listBox2.Text);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox1.Text = listBox3.Text;
            Artist.SelectedIndex = 0;
            DirectoryInfo path = new DirectoryInfo(@"C:\我的東東\노래 ~\" + listBox3.Text);

            int S = 0;
            foreach (var filename in path.GetFiles())
            {
                S++;
                listBox2.Items.Add(filename.Name);
            }
            label9.Text = S + "首歌：";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            listBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Artist.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string constr = "Data Source=LAPTOP-5FC3MHVF\\SQLEXPRESS;Initial Catalog=master;Integrated Security=TRUE;";
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string singer = listBox1.Text;
            string Art = Artist.Text;
            listBox1.Items.Clear();
            string sql = "select singer from GG Union select singer from GG";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader song = com.ExecuteReader();
            while (song.Read())
            {
                listBox1.Items.Add(new song(song.GetString(0)));
            }

            song.Close();

            dataGridView1.Rows.Clear();
            sql = "select * from GG order by singer, song";
            com = new SqlCommand(sql, con);
            song = com.ExecuteReader();
            while (song.Read())
            {
                dataGridView1.Rows.Add(song.GetString(0), song.GetString(1), song.GetString(2));
            }

            song.Close();

            sql = "select * from GG  order by singer, song";
            com = new SqlCommand(sql, con);
            song = com.ExecuteReader();
            int num = 0;
            while (song.Read())
            {
                num++;
                dataGridView1.Rows.Add(song.GetString(0), song.GetString(1), song.GetString(2));
            }
            label6.Text = "有" + num + "首歌的歌詞：";
            song.Close();
            con.Close();

            listBox1.Text = singer;
            Artist.Text = Art;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            font++;
            result1.Font = new Font(new FontFamily(result1.Font.Name), font, result1.Font.Style);
            textBox1.Text = font.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            font--;
            result1.Font = new Font(new FontFamily(result1.Font.Name), font, result1.Font.Style);
            textBox1.Text = font.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            result1.Font = new Font(new FontFamily(result1.Font.Name), float.Parse(textBox1.Text), result1.Font.Style);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            double time = 0;
            time = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            time -= sec;
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = time;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            double time = 0;
            time = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            time += sec;
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = time;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (sec > 0)
            {
                sec += 1;
                if (sec == 0)
                {
                    sec++;
                }
            }
            button6.Text = "前進" + sec + "秒";
            button7.Text = "後退" + sec + "秒";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (sec > 0)
            {
                sec -= 1;
                if (sec == 0)
                {
                    sec++;
                }
            }
            button6.Text = "前進" + sec + "秒";
            button7.Text = "後退" + sec + "秒";
        }
    }
}
