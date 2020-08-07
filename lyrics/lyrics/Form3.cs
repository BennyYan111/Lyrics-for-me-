using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lyrics
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            textBox1.Multiline = true;
            textBox2.Multiline = true;
            textBox3.Multiline = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox2.ScrollBars = ScrollBars.Vertical;
            textBox3.ScrollBars = ScrollBars.Vertical;

            string constr = "Data Source=LAPTOP-5FC3MHVF\\SQLEXPRESS;Initial Catalog=master;Integrated Security=TRUE;";
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string sql = "select singer from GG Union select singer from GG";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader song = com.ExecuteReader();
            while (song.Read())
            {
                comboBox1.Items.Add(new song(song.GetString(0)));
            }
            song.Close();
            
            sql = "select * from GG  order by singer, song";
            com = new SqlCommand(sql, con);
            song = com.ExecuteReader();
            while (song.Read())
            {
                dataGridView1.Rows.Add(song.GetString(0), song.GetString(1), song.GetString(2));
            }
            song.Close();
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Artist.Items.Clear();
            string constr = "Data Source=LAPTOP-5FC3MHVF\\SQLEXPRESS;Initial Catalog=master;Integrated Security=TRUE;";
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            textBox1.Text = comboBox1.Text;
            string sql = "select * from GG where singer='" + textBox1.Text + "' order by song ASC";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader song = com.ExecuteReader();
            int one = 0;
            while (song.Read())
            {
                Artist.Items.Add(new song(song.GetString(1)));
                if (one == 0)
                {
                    Artist.Text = song.GetString(1);
                    textBox2.Text = song.GetString(1);
                    textBox3.Text = song.GetString(2);
                }
                one++;
            }

            song.Close();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Artist_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = Artist.Text;

            string constr = "Data Source=LAPTOP-5FC3MHVF\\SQLEXPRESS;Initial Catalog=master;Integrated Security=TRUE;";
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string sql = "select * from GG where song=N'" + Artist.Text + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader lyr = com.ExecuteReader();
            while (lyr.Read())
            {
                textBox3.Text = lyr.GetString(2);
            }
            lyr.Close();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string constr = "Data Source=LAPTOP-5FC3MHVF\\SQLEXPRESS;Initial Catalog=master;Integrated Security=TRUE;";
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string sql = @"UPDATE GG 
                            SET singer=N'" + textBox1.Text + "', song=N'" + textBox2.Text + "', lyrics=N'" + textBox3.Text + "' " +
                            "WHERE singer=N'" + comboBox1.Text + "' and song=N'" + Artist.Text + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader lyr = com.ExecuteReader();
            lyr.Close();
            con.Close();

            MessageBox.Show("修改成功!");
            comboBox1.Text = "";
            Artist.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void dataGrid1_Navigate(object sender, NavigateEventArgs ne)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //textBox1.Text = "";
            //textBox2.Text = "";
            //textBox3.Text = "";
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            Artist.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
