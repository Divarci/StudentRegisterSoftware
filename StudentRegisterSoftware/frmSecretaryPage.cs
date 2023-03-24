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

        public void TempCashClean()
        {
            tempId = "";
            tempFrom = "";
            tempUserName = "";
            tempPassword = "";
            tempName = "";
            tempSurname = "";
            tempClassBrans = "";
            tempEmail = "";
            tempMobile = ""; ;
        }

        public void DeleteFromGrid(string Table, string Where, string ReqId)
        {
            SqlCommand cmd = new SqlCommand("Delete from " + Table + " where " + Where + "=@p1", conn.conn());
            cmd.Parameters.AddWithValue("@p1", ReqId);
            cmd.ExecuteNonQuery();
            conn.conn().Close();
        }

        public void updategrid(string query, MaskedTextBox UN, TextBox Pass, TextBox name, TextBox surname, TextBox classbrans, TextBox email, MaskedTextBox mobile, string stortc)
        {
            SqlCommand cmd = new SqlCommand(query, conn.conn());
            cmd.Parameters.AddWithValue("@p1", UN.Text);
            cmd.Parameters.AddWithValue("@p2", Pass.Text);
            cmd.Parameters.AddWithValue("@p3", name.Text);
            cmd.Parameters.AddWithValue("@p4", surname.Text);
            cmd.Parameters.AddWithValue("@p5", classbrans.Text);
            cmd.Parameters.AddWithValue("@p6", email.Text);
            cmd.Parameters.AddWithValue("@p7", mobile.Text);
            cmd.Parameters.AddWithValue("@p8", stortc);


            cmd.ExecuteNonQuery();
            conn.conn().Close();

            MessageBox.Show("User has been UPDATED", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public void settings(bool tf1, bool tf2, bool tf3, bool tf4, bool tf5, Color c1, Color c2, Color c3, Color c4)
        {
            btnTcUpdate.Enabled = tf1;
            btnStUpdate.Enabled = tf2;
            btnStSave.Enabled = tf3;
            btnTcSave.Enabled = tf4;
            btnStUpdate.BackColor = c1;
            btnStSave.BackColor = c2;
            btnTcSave.BackColor = c3;
            btnTcUpdate.BackColor = c4;
            pnlSt.Enabled = tf5;
        }

        public string tempUserNameAdm, tempNameAdm, tempSurnameAdm, tempIdAdm;

        private void btnStUpdate_Click(object sender, EventArgs e)
        {
            updategrid("Update Tbl_Students set stusername=@p1,stpass=@p2, stname=@p3, stsurname=@p4, stclass=@p5, stemail = @p6, stmobileno=@p7  where stid=@p8", mskStUserName, txtStPass, txtStName, txtStSurname, txtClass, txtEmail, mskStMobile, tempId);
            StudentClean();
            ListItBaby("execute ListStudents ");
            pnlTc.Enabled = true;

            TempCashClean();

        }

        private void btnTcUpdate_Click(object sender, EventArgs e)
        {
            updategrid("Update Tbl_Teachers set tcusername=@p1,tcpass=@p2, tcname=@p3, tcsurname=@p4, tcbrans=@p5, tcemail = @p6, tcmobileno=@p7  where ntcid=@p8", mskTcUsername, txtTcPass, txtTcName, txtTcSurname, txtTcBrans, txtTcEmail, mskTcMobile, tempId);
            TeacherClean();
            ListItBaby("execute ListTeachers");
            pnlSt.Enabled = true;

            TempCashClean();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            StudentClean();
            TempCashClean();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            TeacherClean();
            TempCashClean();
        }

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
            TempCashClean();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            frmLogin fr = new frmLogin();
            fr.Show();
            this.Close();
        }

        private void deleteDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tempId != "")
            {
                if (switchId == 0)
                {
                    DeleteFromGrid("Tbl_RegisterRequest", "reqid", tempId);
                    MessageBox.Show("Data has been DELETED", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListItBaby("execute ListRequest ");
                    TempCashClean();
                }
                else if (switchId == 1)
                {
                    DeleteFromGrid("Tbl_Teachers", "ntcid", tempId);
                    MessageBox.Show("Data has been DELETED", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListItBaby("execute ListTeachers");
                    TempCashClean();
                }
                else
                {
                    DeleteFromGrid("Tbl_Students", "stid", tempId);
                    MessageBox.Show("Data has been DELETED", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListItBaby("execute ListStudents");
                    TempCashClean();
                }
            }
            else
            {
                MessageBox.Show("Please Select a USER from Table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            /*
            string tempColumnName;
            int choosen2 = dataGridView1.SelectedCells[0].ColumnIndex;
            tempColumnName = dataGridView1.Columns[choosen2].Name.ToString();
            */
        }

        private void copyToBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tempId != "")
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
            else
            {
                MessageBox.Show("Please Select a USER from Table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnTcList_Click(object sender, EventArgs e)
        {

            switchId = 1;
            ListItBaby("execute ListTeachers ");

            settings(true, false, false, false, true, Color.LightGray, Color.LightGray, Color.LightGray, Color.White);

            TeacherClean();
            StudentClean();

            dataGridView1.BackgroundColor = Color.LightCoral;
            dataGridView1.DefaultCellStyle.BackColor = Color.LightCoral;
        }
        private void btnStList_Click(object sender, EventArgs e)
        {
            switchId = -1;
            ListItBaby("execute ListStudents ");

            settings(false, true, false, false, true, Color.White, Color.LightGray, Color.LightGray, Color.LightGray);

            pnlTc.Enabled = true;
            TeacherClean();
            StudentClean();

            dataGridView1.BackgroundColor = Color.PaleGreen;
            dataGridView1.DefaultCellStyle.BackColor = Color.PaleGreen;
        }

        private void btnReqList_Click(object sender, EventArgs e)
        {
            switchId = 0;
            ListItBaby("execute ListRequest ");
            settings(false, false, true, true, true, Color.LightGray, Color.White, Color.White, Color.LightGray);

            pnlTc.Enabled = true;
            TeacherClean();
            StudentClean();

            dataGridView1.BackgroundColor = Color.PaleTurquoise;
            dataGridView1.DefaultCellStyle.BackColor = Color.PaleTurquoise;
        }

        private void frmSecretaryPage_Load(object sender, EventArgs e)
        {
            lblAdmNameSurname.Text = tempNameAdm + " " + tempSurnameAdm;
            TempCashClean();
            
        }
    }
}
