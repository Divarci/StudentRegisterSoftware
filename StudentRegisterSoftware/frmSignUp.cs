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
    public partial class frmSignUp : Form
    {
        public frmSignUp()
        {
            InitializeComponent();
        }

        sqlconnection conn = new sqlconnection();

        public void SendMessage(string From, MaskedTextBox UN, TextBox Pass, TextBox Name, TextBox Surname, TextBox Class, TextBox Email, MaskedTextBox Mobile)
        {
            SqlCommand cmd = new SqlCommand("Insert into Tbl_RegisterRequest (reqfrom,requsername,reqpass,reqname,reqsurname,reqclassbrans,reqemail,reqmobile) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", conn.conn());
            cmd.Parameters.AddWithValue("@p1", From);
            cmd.Parameters.AddWithValue("@p2", UN.Text);
            cmd.Parameters.AddWithValue("@p3", Pass.Text);
            cmd.Parameters.AddWithValue("@p4", Name.Text);
            cmd.Parameters.AddWithValue("@p5", Surname.Text);
            cmd.Parameters.AddWithValue("@p6", Class.Text);
            cmd.Parameters.AddWithValue("@p7", Email.Text);
            cmd.Parameters.AddWithValue("@p8", Mobile.Text);
            cmd.ExecuteNonQuery();
            conn.conn();

            MessageBox.Show("Your request has been delivered", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            UN.Text = "";
            Pass.Text = "";
            Name.Text = "";
            Surname.Text = "";
            Class.Text = "";
            Email.Text = "";
            Mobile.Text = "";
        }

        private void btnStSend_Click(object sender, EventArgs e)
        {
            if (cbSt.Checked)
            {
                SendMessage("Student", mskStUserName, txtStPass, txtStName, txtStSurname, txtClass, txtStEmail, mskStMobile);
            }
            else
            {
                MessageBox.Show("Please accept Terms & Conditions", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTcSend_Click(object sender, EventArgs e)
        {
            if (cbTc.Checked)
            {
                SendMessage("Teacher", mskTcUsername, txtTcPass, txtTcName, txtTcSurname, txtTcBrans, txtTcEmail, mskTcMobile);
            }
            else
            {
                MessageBox.Show("Please accept Terms & Conditions", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
