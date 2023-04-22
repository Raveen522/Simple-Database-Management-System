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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-GDEGPD0\SQLEXPRESS;Initial Catalog=Polly_Pipe;Integrated Security=True");
        string selected_Equipment;


        void clearAll()
        {
            textBox1.Text = "";
            textBox7.Text = "";
            comboBox1.Text = "20 gallon tanks";

        }

        private void Load_data()
        {
            try
            {
                DataTable dt = new DataTable();
                string select_query = "SELECT * FROM Equipment";
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
        private void Form5_Load(object sender, EventArgs e)
        {
            Load_data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Equipment_id = textBox1.Text;
            string Equipment_name = comboBox1.Text;

            SqlCommand cmd = null;
            cmd = new SqlCommand("insert into Equipment(Equipment_id, Equipment_name) values( '" + Equipment_id + "', '" + Equipment_name + "')", conn);

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
            string Equipment_id = textBox1.Text;
            string Equipment_name = comboBox1.Text;

            SqlCommand cmd = null;
            cmd = new SqlCommand("Update Equipment SET Equipment_name ='" + Equipment_name + "' Where Equipment_id = '" + Equipment_id + "'", conn);

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
            string Equipment_id = textBox1.Text;
            try
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand("Delete from Equipment where Equipment_id = '" + Equipment_id + "' ", conn);
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
            string Equipment_id = textBox7.Text;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select * from Equipment where Equipment_id ='" + Equipment_id + "'", conn);
                SqlDataReader myR = cmd.ExecuteReader();
                if (myR.HasRows)
                {
                    while (myR.Read())
                    {
                        textBox1.Text = myR["Equipment_id"].ToString();
                        comboBox1.Text = myR["Equipment_name"].ToString();

                    }
                }
                else
                {
                    MessageBox.Show("Sorry, No record from this Equipment ID..");
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
                selected_Equipment = Convert.ToString(row.Cells[0].Value);
            }
            try
            {
                DataTable dt = new DataTable();
                string select_query = "SELECT * FROM Equipment WHERE  Equipment_id='" + selected_Equipment + "' ";
                SqlCommand cmd = new SqlCommand(select_query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    textBox1.Text = dr[0].ToString();
                    comboBox1.Text = dr[1].ToString();

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
