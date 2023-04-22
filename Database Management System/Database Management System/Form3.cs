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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-GDEGPD0\SQLEXPRESS;Initial Catalog=Polly_Pipe;Integrated Security=True");
        string selected_customer;

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
                string select_query = "SELECT * FROM customer";
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
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string customerid = textBox1.Text;
            string firstname = textBox2.Text;
            string lastname = textBox3.Text;
            string address = textBox4.Text;
            string DOB = textBox5.Text;
            string TP = textBox6.Text;

            SqlCommand cmd = null;
            cmd = new SqlCommand( "insert into customer(customer_id, firstname, lastname, address, DOB, TP) values( '" + customerid + "', '" + firstname + "', '" + lastname + "', '" +address + "', '" +  DOB + "', '" +TP + "')" , conn);

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
            string customerid = textBox1.Text;
            string firstname = textBox2.Text;
            string lastname = textBox3.Text;
            string address = textBox4.Text;
            string DOB = textBox5.Text;
            string TP = textBox6.Text;

            SqlCommand cmd = null;
            cmd = new SqlCommand("Update customer SET firstname ='" + firstname + "', lastname = '" + lastname + "', address = '" + address + "', DOB = '" + DOB + "', TP = '" + TP + "'Where customer_id = '" + customerid + "'" , conn);

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
            string customerid = textBox1.Text;
            try
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand("Delete from customer where customer_id = '" + customerid + "' ", conn);
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
            string customerid = textBox7.Text;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select * from customer where customer_id ='" + customerid + "'", conn);
                SqlDataReader myR = cmd.ExecuteReader();
                if (myR.HasRows)
                {
                    while (myR.Read())
                    {
                        textBox1.Text = myR["customer_id"].ToString();
                        textBox2.Text = myR["firstname"].ToString();
                        textBox3.Text = myR["lastname"].ToString();
                        textBox4.Text = myR["address"].ToString();
                        textBox5.Text = myR["DOB"].ToString();
                        textBox6.Text = myR["TP"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Sorry, No record from this customerid..");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
                conn.Close();
            }

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Load_data();
            
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selected_customer =Convert.ToString(row.Cells[0].Value);
            }
            try
            {
                DataTable dt = new DataTable();
                string select_query = "SELECT * FROM customer WHERE  customer_id='" + selected_customer + "' ";
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
