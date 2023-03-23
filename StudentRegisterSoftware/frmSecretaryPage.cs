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
using System.Xml.Linq;

namespace StudentRegisterSoftware
{
    public partial class frmSecretaryPage : Form
    {
        public frmSecretaryPage()
        {
            InitializeComponent();
        }

        sqlconnection conn = new sqlconnection();

        public string tempId, tempFrom;
        int switchId;
        public string tempUserName, tempPassword, tempName, tempSurname, tempEmail, tempMobile, tempClassBrans;

        public void StudentClean()
        {
            mskStUserName.Text = "";
            txtStPass.Text = "";
            txtStName.Text = "";
            txtStSurname.Text = "";
            txtClass.Text = "";
            txtEmail.Text = "";
            mskStMobile.Text = "";
        }

        public void TeacherClean()
        {
            mskTcUsername.Text = "";
            txtTcPass.Text = "";
            txtTcName.Text = "";
            txtTcSurname.Text = "";
            txtTcBrans.Text = "";
            txtTcEmail.Text = "";
            mskTcMobile.Text = "";
        }

        public void DeleteFromGrid(string Table, string Where, string ReqId)
        {
            SqlCommand cmd = new SqlCommand("Delete from " + Table + " where " + Where + "=@p1", conn.conn());
            cmd.Parameters.AddWithValue("@p1", ReqId);
            cmd.ExecuteNonQuery();
            conn.conn().Close();

            
        }

        public void SaveToGrid(string query, MaskedTextBox UN, TextBox Pass, TextBox name, TextBox surname, TextBox classs, TextBox email, MaskedTextBox mobile, string stortc)
        {
            SqlCommand cmd = new SqlCommand("Insert into " + query + " values (@p1, @p2, @p3, @p4, @p5, @p6, @p7,@p8)", conn.conn());
            cmd.Parameters.AddWithValue("@p1", UN.Text);
            cmd.Parameters.AddWithValue("@p2", Pass.Text);
            cmd.Parameters.AddWithValue("@p3", name.Text);
            cmd.Parameters.AddWithValue("@p4", surname.Text);
            cmd.Parameters.AddWithValue("@p5", classs.Text);
            cmd.Parameters.AddWithValue("@p6", email.Text);
            cmd.Parameters.AddWithValue("@p7", mobile.Text);
            cmd.Parameters.AddWithValue("@p8", stortc);

            cmd.ExecuteNonQuery();
            conn.conn().Close();

            MessageBox.Show("User has been CREATED", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        public void ListItBaby(string query)
        {
            SqlDataAdapter da = new SqlDataAdapter(query, conn.conn());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
           // dataGridView1.Columns[5].Visible = false;
        }

        public string tempUserNameAdm, tempNameAdm, tempSurnameAdm, tempIdAdm;

        private void btnTcSave_Click(object sender, EventArgs e)
        {
            SaveToGrid("Tbl_Teachers (tcusername, tcpass, tcname, tcsurname, tcbrans, tcemail, tcmobileno, tcorst)", mskTcUsername, txtTcPass, txtTcName, txtTcSurname, txtTcBrans, txtTcEmail, mskTcMobile, tempFrom);
            TeacherClean();
            pnlSt.Enabled = true;
            DeleteFromGrid("Tbl_RegisterRequest", "reqid", tempId);
            switchId = 0;
            ListItBaby("execute ListRequest ");


        }

        private void btnStSave_Click(object sender, EventArgs e)
        {
            SaveToGrid("Tbl_Students (stusername, stpass, stname, stsurname, stclass, stemail, stmobileno, stortc)", mskStUserName, txtStPass, txtStName, txtStSurname, txtClass, txtEmail, mskStMobile, tempFrom);
            StudentClean();
            pnlTc.Enabled = true;
            DeleteFromGrid("Tbl_RegisterRequest", "reqid", tempId);
            switchId = 0;
            ListItBaby("execute ListRequest ");
        }

        

        private void btnExit_Click(object sender, EventArgs e)
        {
            frmLogin fr = new frmLogin();
            fr.Show();
            this.Close();
        }

        private void deleteDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (switchId == 0)
            {
                DeleteFromGrid("Tbl_RegisterRequest", "reqid", tempId);
                MessageBox.Show("Data has been DELETED", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListItBaby("execute ListRequest ");

            }
            else if (switchId == 1)
            {
                DeleteFromGrid("Tbl_Teachers", "ntcid", tempId);
                MessageBox.Show("Data has been DELETED", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListItBaby("execute ListTeachers");

            }
            else
            {
                DeleteFromGrid("Tbl_Students", "stid", tempId);
                MessageBox.Show("Data has been DELETED", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListItBaby("execute ListStudents ");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int choosen = dataGridView1.SelectedCells[0].RowIndex;
            tempId = dataGridView1.Rows[choosen].Cells[0].Value.ToString();
            tempFrom = dataGridView1.Rows[choosen].Cells[5].Value.ToString();

            tempUserName = dataGridView1.Rows[choosen].Cells[1].Value.ToString();
            tempPassword = dataGridView1.Rows[choosen].Cells[2].Value.ToString();
            tempName = dataGridView1.Rows[choosen].Cells[3].Value.ToString();
            tempSurname = dataGridView1.Rows[choosen].Cells[4].Value.ToString();
            tempClassBrans = dataGridView1.Rows[choosen].Cells[6].Value.ToString();
            tempEmail = dataGridView1.Rows[choosen].Cells[7].Value.ToString();
            tempMobile = dataGridView1.Rows[choosen].Cells[8].Value.ToString();



        }

        private void copyToBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (tempFrom == "Student")
            {
                pnlSt.Enabled = true;

                mskStUserName.Text = tempUserName;
                txtStPass.Text = tempPassword;
                txtStName.Text = tempName;
                txtStSurname.Text = tempSurname;
                txtClass.Text = tempClassBrans;
                txtEmail.Text = tempEmail;
                mskStMobile.Text = tempMobile;

                TeacherClean();
                pnlTc.Enabled = false;
            }
            else if (tempFrom == "Teacher")
            {
                pnlTc.Enabled = true;

                mskTcUsername.Text = tempUserName;
                txtTcPass.Text = tempPassword;
                txtTcName.Text = tempName;
                txtTcSurname.Text = tempSurname;
                txtTcBrans.Text = tempClassBrans;
                txtTcEmail.Text = tempEmail;
                mskTcMobile.Text = tempMobile;

                StudentClean();
                pnlSt.Enabled = false;


            }
        }
        private void btnTcList_Click(object sender, EventArgs e)
        {
            switchId = 1;
            ListItBaby("execute ListTeachers");
            btnTcUpdate.Enabled = true;
            btnStUpdate.Enabled = false;
            btnStSave.Enabled = false;
            btnTcSave.Enabled = false;
            btnStUpdate.BackColor = Color.LightGray;
            btnStSave.BackColor = Color.LightGray;
            btnTcSave.BackColor = Color.LightGray;
            btnTcUpdate.BackColor = Color.White;
            pnlSt.Enabled = true;
            TeacherClean();
            StudentClean();
        }
        private void btnStList_Click(object sender, EventArgs e)
        {
            switchId = -1;
            ListItBaby("execute ListStudents ");
            btnStUpdate.Enabled = true;
            btnTcUpdate.Enabled = false;
            btnStSave.Enabled = false;
            btnTcSave.Enabled = false;
            btnTcUpdate.BackColor = Color.LightGray;
            btnStSave.BackColor = Color.LightGray;
            btnTcSave.BackColor = Color.LightGray;
            btnStUpdate.BackColor = Color.White;
            pnlTc.Enabled = true;
            TeacherClean();
            StudentClean();
        }

        private void btnReqList_Click(object sender, EventArgs e)
        {
            switchId = 0;
            ListItBaby("execute ListRequest ");
            btnTcUpdate.Enabled = false;
            btnStUpdate.Enabled = false;
            btnStSave.Enabled = true;
            btnTcSave.Enabled = true;
            btnTcUpdate.BackColor = Color.LightGray;
            btnStUpdate.BackColor = Color.LightGray;
            btnTcSave.BackColor = Color.White;
            btnStSave.BackColor = Color.White;
        }

        private void frmSecretaryPage_Load(object sender, EventArgs e)
        {


        }
    }
}
