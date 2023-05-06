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
    public partial class frmAdminInformation : Form
    {
        public frmAdminInformation()
        {
            InitializeComponent();
        }
        //sql connecton
        sqlconnection conn = new sqlconnection();
        //temporary values
        public string tempMobno, tempEmail, TempId;

        private void button1_Click(object sender, EventArgs e)
        {
            //close window
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(txtEmail.Text == null || txtEmail.Text =="" || txtEmail.Text==string.Empty)
            {
                //if forms are empty
                MessageBox.Show("Please Provide an E-Mail address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //if it is not update works
                SqlCommand cmd = new SqlCommand("Update Tbl_Secretaries set admmobileno=@p1,admemail=@p2 where admid=@p3", conn.conn());
                cmd.Parameters.AddWithValue("@p1", mskMobno.Text);
                cmd.Parameters.AddWithValue("@p2", txtEmail.Text);
                cmd.Parameters.AddWithValue("@p3", TempId);
                cmd.ExecuteNonQuery();
                conn.conn().Close();

                MessageBox.Show("Your Information has been UPDATED", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void frmAdminInformation_Load(object sender, EventArgs e)
        {
            //assign infos to forms
            mskMobno.Text = tempMobno;
            txtEmail.Text = tempEmail;
        }
    }
}
