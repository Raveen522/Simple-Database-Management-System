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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-GDEGPD0\SQLEXPRESS;Initial Catalog=Polly_Pipe;Integrated Security=True");
        string selected_staff;


        void clearAll()
        {
            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";
            comboBox1.Text = "Plumber";
        }

        private void Load_data()
        {
            try
            {
                DataTable dt = new DataTable();
                string select_query = "SELECT * FROM staff";
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
        private void Form4_Load(object sender, EventArgs e)
        {
            Load_data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string staffid = textBox1.Text;
            string stafffirstname = textBox3.Text;
            string stafflastname = textBox4.Text;
            string stafftype = comboBox1.Text;
            string staffTP = textBox5.Text;

            SqlCommand cmd = null;
            cmd = new SqlCommand("insert into staff(staff_id, firstname, lastname, type, TP) values( '" + staffid + "', '" + stafffirstname + "', '" + stafflastname + "', '" + stafftype + "', '" + staffTP + "')", conn);

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
            string staffid = textBox1.Text;
            string stafffirstname = textBox3.Text;
            string stafflastname = textBox4.Text;
            string stafftype = comboBox1.Text;
            string staffTP = textBox5.Text;

            SqlCommand cmd = null;
            cmd = new SqlCommand("Update staff SET firstname ='" + stafffirstname + "', lastname = '" + stafflastname + "', type = '" +stafftype + "',  TP = '" + staffTP + "'Where staff_id = '" + staffid + "'", conn);

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
            string staffid = textBox1.Text;
            try
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand("Delete from staff where staff_id = '" + staffid + "' ", conn);
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

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selected_staff = Convert.ToString(row.Cells[0].Value);
            }
            try
            {
                DataTable dt = new DataTable();
                string select_query = "SELECT * FROM staff WHERE  staff_id='" + selected_staff + "' ";
                SqlCommand cmd = new SqlCommand(select_query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    textBox1.Text = dr[0].ToString();
                    textBox3.Text = dr[1].ToString();
                    textBox4.Text = dr[2].ToString();
                    comboBox1.Text = dr[3].ToString();
                    textBox5.Text = dr[4].ToString();

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

        private void button4_Click(object sender, EventArgs e)
        {
            string staffid = textBox7.Text;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select * from staff where staff_id ='" + staffid + "'", conn);
                SqlDataReader myR = cmd.ExecuteReader();
                if (myR.HasRows)
                {
                    while (myR.Read())
                    {
                        textBox1.Text = myR["staff_id"].ToString();
                        textBox3.Text = myR["firstname"].ToString();
                        textBox4.Text = myR["lastname"].ToString();
                        comboBox1.Text = myR["type"].ToString();
                        textBox5.Text = myR["TP"].ToString();
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
