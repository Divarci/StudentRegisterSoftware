using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
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
            
            byte[] dtrry=ASCIIEncoding.ASCII.GetBytes(Pass.Text);
            string passdtst = Convert.ToBase64String(dtrry);


            SqlCommand cmd = new SqlCommand("Insert into Tbl_RegisterRequest (reqfrom,requsername,reqpass,reqname,reqsurname,reqclassbrans,reqemail,reqmobile) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", conn.conn());
            cmd.Parameters.AddWithValue("@p1", From);
            cmd.Parameters.AddWithValue("@p2", UN.Text);
            cmd.Parameters.AddWithValue("@p3", passdtst);
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
            TextBox[] txtboxes = { txtStPass, txtStName, txtStSurname, txtStEmail, txtClass };
            MaskedTextBox[] mskboxes = { mskStUserName };

            int a = 0, b = 0;

            for (int i = 0; i < txtboxes.Length; i++)
            {

                if (txtboxes[i].Text == "" || txtboxes[i].Text == null || txtboxes[i].Text == string.Empty)
                {
                    a++;
                }
            }

            for (int y = 0; y < mskboxes.Length; y++)
            {
                if (mskboxes[y].Text == "" || mskboxes[y].Text == null || mskboxes[y].Text == string.Empty)
                {

                    b++;
                }
            }


            if (a > 0 || b > 0)
            {
                MessageBox.Show("Please Provide All informations", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand select = new SqlCommand("Select stusername from Tbl_Students where stusername=@p1", conn.conn());
                select.Parameters.AddWithValue("@p1", mskStUserName.Text);
                SqlDataReader dr = select.ExecuteReader();
                if (dr.Read())
                {
                    mskStUserName.Text = dr[0].ToString();
                    MessageBox.Show("This Username has been Taken by Another Student. Please choose another Username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    ; SqlCommand select2 = new SqlCommand("Select requsername,reqfrom from Tbl_RegisterRequest where requsername=@p1 and reqfrom=@p2", conn.conn());
                    select2.Parameters.AddWithValue("@p1", mskStUserName.Text);
                    select2.Parameters.AddWithValue("@p2", "Student");
                    SqlDataReader dr2 = select2.ExecuteReader();
                    if (dr2.Read())
                    {
                        mskStUserName.Text = dr2[0].ToString();
                        MessageBox.Show("This Username has been Requested by Another Student. Please choose another Username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
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
                }


                cbSt.Checked = false;
            }
        }

        private void btnTcSend_Click(object sender, EventArgs e)
        {
            TextBox[] txtboxes = { txtTcPass, txtTcName, txtTcSurname, txtTcEmail, txtTcBrans };
            MaskedTextBox[] mskboxes = { mskTcUsername };

            int a = 0, b = 0;

            for (int i = 0; i < txtboxes.Length; i++)
            {

                if (txtboxes[i].Text == "" || txtboxes[i].Text == null || txtboxes[i].Text == string.Empty)
                {
                    a++;
                }
            }

            for (int y = 0; y < mskboxes.Length; y++)
            {
                if (mskboxes[y].Text == "" || mskboxes[y].Text == null || mskboxes[y].Text == string.Empty)
                {

                    b++;
                }
            }

            if (a > 0 || b > 0)
            {
                MessageBox.Show("Please Provide All informations", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand select = new SqlCommand("Select tcusername from Tbl_Teachers where tcusername=@p1", conn.conn());
                select.Parameters.AddWithValue("@p1", mskTcUsername.Text);
                SqlDataReader dr = select.ExecuteReader();
                if (dr.Read())
                {
                    mskTcUsername.Text = dr[0].ToString();
                    MessageBox.Show("This Username has been Taken by Another teacher. Please choose another Username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    ; SqlCommand select2 = new SqlCommand("Select requsername,reqfrom from Tbl_RegisterRequest where requsername=@p1 and reqfrom=@p2", conn.conn());
                    select2.Parameters.AddWithValue("@p1", mskTcUsername.Text);
                    select2.Parameters.AddWithValue("@p2", "Teacher");
                    SqlDataReader dr2 = select2.ExecuteReader();
                    if (dr2.Read())
                    {
                        mskTcUsername.Text = dr2[0].ToString();
                        MessageBox.Show("This Username has been Requested by Another Teacher. Please choose another Username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
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
                }
                cbTc.Checked = false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbEyeSt_MouseHover(object sender, EventArgs e)
        {
            txtStPass.UseSystemPasswordChar = false;
        }

        private void pbEyeSt_MouseLeave(object sender, EventArgs e)
        {
            txtStPass.UseSystemPasswordChar = true;
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            txtTcPass.UseSystemPasswordChar = false;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            txtTcPass.UseSystemPasswordChar = true;
        }
    }
}
