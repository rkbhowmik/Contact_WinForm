using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace Telphone
{
    public partial class Phone : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-CMPNBJC\SQLEXPRESS;Initial Catalog=Phone;Integrated Security=True");

        public Phone()
        {
            InitializeComponent();
        }

        private void Phone_Load(object sender, EventArgs e)
        {
            Display();           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox3.Text = "";
            textBox4.Clear();
            comboBox1.SelectedIndex = -1;
            textBox1.Focus();
            lblmsg.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[list]
           ([First]
           ,[Last]
           ,[Mobile]
           ,[Email]
           ,[Category])
             VALUES
                ('" + textBox1.Text + "','" + textBox2.Text+ "','" + textBox3.Text+ "','" + textBox4.Text + "','" + comboBox1.Text +"')", con);

            cmd.ExecuteNonQuery();
            con.Close();
            var msg1 = "Successfully Saved!";
            lblmsg.ForeColor = Color.Green;
            lblmsg.Text = msg1;
            Display();
        }

        void Display()
        {
            SqlDataAdapter sd = new SqlDataAdapter("SELECT list.* FROM  list", con);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow  item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["First"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
           textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
           comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"DELETE FROM list
            WHERE (Mobile = '"+ textBox3.Text +"')", con); // Used Mobile because of its the primary key

            cmd.ExecuteNonQuery();
            con.Close();
            var msg2 = "Delete Succesfully!";
            lblmsg.ForeColor = Color.Red; 
            lblmsg.Text= msg2.ToString();
            Display();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"UPDATE list
                SET First = '" + textBox1.Text + "', " +
                "Last = '" + textBox2.Text + "', " +
                "Mobile = '" + textBox3.Text + "', " +
                "Email = '" + textBox4.Text + "', " +
                "Category = '" + comboBox1.Text + "' WHERE (Mobile = '%" + textBox3.Text + "')", con); 

            cmd.ExecuteNonQuery();
            con.Close();
            var msg3 = "Updated Successfully!";
            lblmsg.Text = msg3;
            Display();
        }

        //Text search event
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.Text = string.Empty;
            SqlDataAdapter sd = new SqlDataAdapter("SELECT * from list Where (Mobile like '%" + textBox5.Text + "%') or (First like '%" + textBox5.Text + "%') ", con);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
            }
        }
    }
}
