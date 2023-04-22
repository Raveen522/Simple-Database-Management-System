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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-GDEGPD0\SQLEXPRESS;Initial Catalog=Polly_Pipe;Integrated Security=True");
        string selected_install;

        void clearAll()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
        }

        private void Load_data()
        {
            try
            {
                DataTable dt = new DataTable();
                string select_query = "SELECT * FROM Installation";
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
        private void Form7_Load(object sender, EventArgs e)
        {
            Load_data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string installation_id = textBox1.Text;
            string installation_name = textBox2.Text;
            string type = textBox3.Text;
            string staff_id = textBox4.Text;
            string customer_id = textBox5.Text;
            string location_id = textBox6.Text;
            string equipment_id = textBox7.Text;


            SqlCommand cmd = null;
            cmd = new SqlCommand("insert into installation (installation_id , installation_name, type, staff_id,customer_id, location_id,equipment_id) values( '" + installation_id + "', '" + installation_name + "', '" + type + "', '" + staff_id + "', '" + customer_id + "', '" + location_id + "','" + equipment_id + "')", conn);

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
            string installation_id = textBox1.Text;
            string installation_name = textBox2.Text;
            string type = textBox3.Text;
            string staff_id = textBox4.Text;
            string customer_id = textBox5.Text;
            string location_id = textBox6.Text;
            string equipment_id = textBox7.Text;

            SqlCommand cmd = null;
            cmd = new SqlCommand("Update  installation SET  installation_name ='" + installation_name + "', type = '" + type + "', staff_id = '" + staff_id + "', customer_id = '" + customer_id + "', location_id = '" + location_id + "',equipment_id ='" + equipment_id + "' Where installation_id = '" + installation_id + "'", conn);

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
            string installation_id = textBox1.Text;
            try
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand("Delete from installation where installation_id = '" + installation_id + "' ", conn);
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
            string installation_id = textBox8.Text;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select * from installation where installation_id ='" + installation_id + "'", conn);
                SqlDataReader myR = cmd.ExecuteReader();
                if (myR.HasRows)
                {
                    while (myR.Read())
                    {
                        textBox1.Text = myR["installation_id"].ToString();
                        textBox2.Text = myR["installation_name"].ToString();
                        textBox3.Text = myR["type"].ToString();
                        textBox4.Text = myR["staff_id"].ToString();
                        textBox5.Text = myR["customer_id"].ToString();
                        textBox6.Text = myR["location_id"].ToString();
                        textBox7.Text = myR["equipment_id"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Sorry, No record from this installation..");
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
                selected_install = Convert.ToString(row.Cells[0].Value);
            }
            try
            {
                DataTable dt = new DataTable();
                string select_query = "SELECT * FROM installation WHERE  installation_id='" + selected_install + "' ";
                SqlCommand cmd = new SqlCommand(select_query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    textBox1.Text = dr[0].ToString();
                    textBox2.Text = dr[1].ToString();
                    textBox3.Text = dr[2].ToString();
                    textBox4.Text = dr[3].ToString();
                    textBox5.Text = dr[4].ToString();
                    textBox6.Text = dr[5].ToString();
                    textBox7.Text = dr[6].ToString();
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
