using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lyrics
{
    public partial class Form2 : Form
    {
        public Form2()
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
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("填入值不可為空!");
            }
            else
            {
                string constr = "Data Source=LAPTOP-5FC3MHVF\\SQLEXPRESS;Initial Catalog=master;Integrated Security=TRUE;";
                SqlConnection con = new SqlConnection(constr);
                con.Open();

                string sql0 = "select * from GG ";
                SqlCommand com0 = new SqlCommand(sql0, con);
                SqlDataReader lyr0 = com0.ExecuteReader();
                Boolean right = false;
                while (lyr0.Read())
                {
                    right = false;
                    if (textBox2.Text.Equals(lyr0.GetSqlString(1)))
                    {
                        right = true;
                        break;
                    }
                }
                lyr0.Close();

                Boolean err = false;
                if (right == false)
                {
                    try
                    {
                        textBox3.Text = textBox3.Text.Replace("\'", "’");
                        textBox2.Text = textBox2.Text.Replace("\'", "’");
                        textBox1.Text = textBox1.Text.Replace("\'", "’");
                        string sql1 = @"insert into GG(singer, song, lyrics)
                                    values(N'" + textBox1.Text + "', N'" + textBox2.Text + "', N'" + textBox3.Text + "')";
                        SqlCommand com1 = new SqlCommand(sql1, con);
                        SqlDataReader lyr1 = com1.ExecuteReader();
                        lyr1.Close();
                    }
                    catch (SqlException ex)
                    {
                        err = true;
                        MessageBox.Show("填入的資料可能有誤或是已存在!\r\n" + ex.Message);
                    }
                }
                
                string sql = "select singer from GG Union select singer from GG";
                SqlCommand com = new SqlCommand(sql, con);
                SqlDataReader lyr = com.ExecuteReader();
                Boolean have = false;
                while (lyr.Read())
                {
                    if (textBox1.Text.Equals(lyr.GetValue(0)))
                    {
                        have = true;
                        break;
                    }
                }
                lyr.Close();

                if (have == false && textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
                {
                    sql = "insert into singer(singer) values(N'" + textBox1.Text + "')";
                    com = new SqlCommand(sql, con);
                    lyr = com.ExecuteReader();
                    lyr.Close();
                }
                
                if (err == false)
                {
                    MessageBox.Show("新增成功~~~");
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
                con.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.Text;
        }
    }
}
