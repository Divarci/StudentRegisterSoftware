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

namespace StudentRegisterSoftware
{
    public partial class frmUserInformation : Form
    {
        public frmUserInformation()
        {
            InitializeComponent();
        }
        //same steps with frmadmininformaton
        sqlconnection conn = new sqlconnection();

        public string tempMobno, tempEmail, TempId;

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == null || txtEmail.Text == "" || txtEmail.Text == string.Empty)
            {

                MessageBox.Show("Please Provide an E-Mail address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("Update Tbl_Students set stmobileno=@p1,stemail=@p2 where stid=@p3", conn.conn());
                cmd.Parameters.AddWithValue("@p1", mskMobno.Text);
                cmd.Parameters.AddWithValue("@p2", txtEmail.Text);
                cmd.Parameters.AddWithValue("@p3", TempId);
                cmd.ExecuteNonQuery();
                conn.conn().Close();

                MessageBox.Show("Your Information has been UPDATED", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmUserInformation_Load(object sender, EventArgs e)
        {
            mskMobno.Text = tempMobno;
            txtEmail.Text = tempEmail;
        }
    }
}
