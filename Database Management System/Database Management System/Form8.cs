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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-GDEGPD0\SQLEXPRESS;Initial Catalog=Polly_Pipe;Integrated Security=True");
        string selected_job;


        void clearAll()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox7.Text = "";

        }

        private void Load_data()
        {
            try
            {
                DataTable dt = new DataTable();
                string select_query = "SELECT * FROM Require";
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form8_Load(object sender, EventArgs e)
        {
            Load_data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string jobid = textBox1.Text;
            string jobtype = textBox2.Text;
            string staffid = textBox3.Text;
            string equipmentid = textBox4.Text;


            SqlCommand cmd = null;
            cmd = new SqlCommand("insert into require(require_id, staff_ID, Equipment_id, require_type) values( '" + jobid + "', '" + staffid + "', '" + equipmentid + "', '" + jobtype + "')", conn);

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
            string jobid = textBox1.Text;
            string jobtype = textBox2.Text;
            string staffid = textBox3.Text;
            string equipmentid = textBox4.Text;

            SqlCommand cmd = null;
            cmd = new SqlCommand("Update require SET  staff_id = '" + staffid + "', require_type = '" + jobtype + "',  equipment_id = '" + equipmentid + "'Where require_id = '" + jobid + "'", conn);

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

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selected_job = Convert.ToString(row.Cells[0].Value);
            }
            try
            {
                DataTable dt = new DataTable();
                string select_query = "SELECT * FROM require WHERE  require_id='" + selected_job + "' ";
                SqlCommand cmd = new SqlCommand(select_query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    textBox1.Text = dr[0].ToString();
                    textBox2.Text = dr[3].ToString();
                    textBox3.Text = dr[1].ToString();
                    textBox4.Text = dr[2].ToString();

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

        private void button3_Click(object sender, EventArgs e)
        {

            string jobid = textBox1.Text;
            try
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand("Delete from require where require_id = '" +jobid + "' ", conn);
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
            string jobid = textBox7.Text;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select * from require where require_id ='" +jobid + "'", conn);
                SqlDataReader myR = cmd.ExecuteReader();
                if (myR.HasRows)
                {
                    while (myR.Read())
                    {
                        textBox1.Text = myR["require_id"].ToString();
                        textBox2.Text = myR["staff_id"].ToString();
                        textBox3.Text = myR["equipment_id"].ToString();
                        textBox4.Text = myR["require_type"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Sorry, No record from this Staff ID..");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
                conn.Close();
            }
        }
    }
}
