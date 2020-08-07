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
    public partial class Form4 : Form
    {
        private string string1;

        public Form4()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public string String1
        {
            set
            {
                string1 = value;
            }
        }

        public void SetValue()
        {
            this.label3.Text = string1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string constr = "Data Source=LAPTOP-5FC3MHVF\\SQLEXPRESS;Initial Catalog=master;Integrated Security=TRUE;";
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string sql = "delete from GG where song='" + label3.Text + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader song = com.ExecuteReader();
            song.Close();
            con.Close();

            MessageBox.Show("已將" + label3.Text + "刪除!");

            this.Close();
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }
    }
}
