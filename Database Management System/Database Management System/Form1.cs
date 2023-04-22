using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DB_Assignmnet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (txtusername.Text=="admin" && txtpw.Text == "raveen123")
            {
                Form2 frmme = new Form2();
                frmme.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Use correct username & password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
