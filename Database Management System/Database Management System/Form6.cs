using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace DB_Assignmnet
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-GDEGPD0\SQLEXPRESS;Initial Catalog=Polly_Pipe;Integrated Security=True");
        string selected_Location;


        void clearAll()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox7.Text = "";

        }

        private void Load_data()
        {
            try
            {
                DataTable dt = new DataTable();
                string select_query = "SELECT * FROM Location";
                SqlCommand cmd = new SqlCommand(select_query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
        private void Form6_Load(object sender, EventArgs e)
        {
            Load_data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Location_id = textBox1.Text;
            string Location_name = textBox2.Text;

            SqlCommand cmd = null;
            cmd = new SqlCommand("insert into Location(Location_id, Location_name) values( '" + Location_id + "', '" + Location_name + "')", conn);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                clearAll();
                Load_data();
                MessageBox.Show("Successfully Added");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Location_id = textBox1.Text;
            string Location_name = textBox2.Text;

            SqlCommand cmd = null;
            cmd = new SqlCommand("Update Location SET Location_name ='" + Location_name + "' Where Location_id = '" + Location_id + "'", conn);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                clearAll();
                Load_data();
                MessageBox.Show("Successfully Updated");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                conn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Location_id = textBox1.Text;
            try
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand("Delete from Location where Location_id = '" + Location_id + "' ", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                Load_data();
                clearAll();
                MessageBox.Show("Successfully Deleted");


            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message);
                conn.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string Location_id = textBox7.Text;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select * from Location where Location_id ='" + Location_id + "'", conn);
                SqlDataReader myR = cmd.ExecuteReader();
                if (myR.HasRows)
                {
                    while (myR.Read())
                    {
                        textBox1.Text = myR["Location_id"].ToString();
                        textBox2.Text = myR["Location_name"].ToString();

                    }
                }
                else
                {
                    MessageBox.Show("Sorry, No record from this Location ID..");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
                conn.Close();
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selected_Location = Convert.ToString(row.Cells[0].Value);
            }
            try
            {
                DataTable dt = new DataTable();
                string select_query = "SELECT * FROM Location  WHERE  Location_id='" + selected_Location + "' ";
                SqlCommand cmd = new SqlCommand(select_query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    textBox1.Text = dr[0].ToString();
                    textBox2.Text = dr[1].ToString();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
