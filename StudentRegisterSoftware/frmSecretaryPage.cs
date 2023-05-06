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
using static System.Net.Mime.MediaTypeNames;

namespace StudentRegisterSoftware
{
    public partial class frmSecretaryPage : Form
    {
        public frmSecretaryPage()
        {
            InitializeComponent();
        }
        //sql connection
        sqlconnection conn = new sqlconnection();
        //temporary variables

        //temporary value which keeps a data comes from listing method.
        int switchId;

        public string tempUserNameAdm, tempNameAdm, tempSurnameAdm, tempIdAdm;
        //student form clean method
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
        //teacher form clean method
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
        //temporary variables clean method
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
        //a method for delete data
        public void DeleteFromGrid(string Table, string Where, string ReqId)
        {
            SqlCommand cmd = new SqlCommand("Delete from " + Table + " where " + Where + "=@p1", conn.conn());
            cmd.Parameters.AddWithValue("@p1", ReqId);
            cmd.ExecuteNonQuery();
            conn.conn().Close();
        }
        //a method for update data
        public void updategrid(string query, MaskedTextBox UN, TextBox Pass, TextBox name, TextBox surname, TextBox classbrans, TextBox email, MaskedTextBox mobile, string stortc)
        {
            byte[] code = ASCIIEncoding.ASCII.GetBytes(Pass.Text);
            string coded = Convert.ToBase64String(code);

            SqlCommand cmd = new SqlCommand(query, conn.conn());
            cmd.Parameters.AddWithValue("@p1", UN.Text);
            cmd.Parameters.AddWithValue("@p2", coded);
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
        //a method for save data
        public void SaveToGrid(string query, MaskedTextBox UN, TextBox Pass, TextBox name, TextBox surname, TextBox classs, TextBox email, MaskedTextBox mobile, string stortc)
        {
            byte[] code = ASCIIEncoding.ASCII.GetBytes(Pass.Text);
            string coded = Convert.ToBase64String(code);

            SqlCommand cmd = new SqlCommand("Insert into " + query + " values (@p1, @p2, @p3, @p4, @p5, @p6, @p7,@p8)", conn.conn());
            cmd.Parameters.AddWithValue("@p1", UN.Text);
            cmd.Parameters.AddWithValue("@p2", coded);
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
        //a method for list data
        public void ListItBaby(string query)
        {
            SqlDataAdapter da = new SqlDataAdapter(query, conn.conn());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
            //variable clear
            TempCashClean();
        }
        //button status and color method which will used for enable and disable button whitg their colors.
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

        private void btnStUpdate_Click(object sender, EventArgs e)
        {
            //same steps with save buttons
            TextBox[] txtboxes = { txtStPass, txtStName, txtStSurname, txtEmail, txtClass };
            MaskedTextBox[] mskboxes = { mskStUserName };
            int txtboxControl = 0, mskboxControl = 0;


            for (int i = 0; i < txtboxes.Length; i++)
            {

                if (txtboxes[i].Text == "" || txtboxes[i].Text == null || txtboxes[i].Text == string.Empty)
                {
                    txtboxControl++;
                }
            }

            for (int y = 0; y < mskboxes.Length; y++)
            {
                if (mskboxes[y].Text == "" || mskboxes[y].Text == null || mskboxes[y].Text == string.Empty)
                {

                    mskboxControl++;
                }
            }

            if (txtboxControl > 0 || mskboxControl > 0)
            {
                MessageBox.Show("Please Provide All informations", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                updategrid("Update Tbl_Students set stusername=@p1,stpass=@p2, stname=@p3, stsurname=@p4, stclass=@p5, stemail = @p6, stmobileno=@p7  where stid=@p8", mskStUserName, txtStPass, txtStName, txtStSurname, txtClass, txtEmail, mskStMobile, tempId);
                StudentClean();
                ListItBaby("execute ListStudents ");
                pnlTc.Enabled = true;

                TempCashClean();
            }

        }

        private void btnTcUpdate_Click(object sender, EventArgs e)
        {
            //same steps with save buttons
            TextBox[] txtboxes = { txtTcPass, txtTcName, txtTcSurname, txtTcEmail, txtTcBrans };
            MaskedTextBox[] mskboxes = { mskTcUsername };

            int txtBoxControl = 0, mskBoxContol = 0;

            for (int i = 0; i < txtboxes.Length; i++)
            {

                if (txtboxes[i].Text == "" || txtboxes[i].Text == null || txtboxes[i].Text == string.Empty)
                {
                    txtBoxControl++;
                }
            }

            for (int y = 0; y < mskboxes.Length; y++)
            {
                if (mskboxes[y].Text == "" || mskboxes[y].Text == null || mskboxes[y].Text == string.Empty)
                {

                    mskBoxContol++;
                }
            }


            if (txtBoxControl > 0 || mskBoxContol > 0)
            {
                MessageBox.Show("Please Provide All informations", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                updategrid("Update Tbl_Teachers set tcusername=@p1,tcpass=@p2, tcname=@p3, tcsurname=@p4, tcbrans=@p5, tcemail = @p6, tcmobileno=@p7  where ntcid=@p8", mskTcUsername, txtTcPass, txtTcName, txtTcSurname, txtTcBrans, txtTcEmail, mskTcMobile, tempId);
                TeacherClean();
                ListItBaby("execute ListTeachers");
                pnlSt.Enabled = true;

                TempCashClean();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //celaning process
            StudentClean();
            TempCashClean();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //celaning process
            TeacherClean();
            TempCashClean();
        }

        private void pbInbox_Click(object sender, EventArgs e)
        {
            //leads you to message page
            frmAdminMessage fr = new frmAdminMessage();
            fr.tempIdAdm = tempIdAdm;
            fr.tempNameAdm = tempNameAdm;
            fr.tempSurnameAdm = tempSurnameAdm;
            fr.Show();

        }

        private void pbSettings_Click(object sender, EventArgs e)
        {
            //leads you to change user settings
            frmAdminInformation fr = new frmAdminInformation();

            fr.TempId = tempIdAdm;
            SqlCommand cmd = new SqlCommand("Select admmobileno,admemail from Tbl_Secretaries where admid=@p1", conn.conn());
            cmd.Parameters.AddWithValue("@p1", tempIdAdm);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                fr.tempMobno = dr[0].ToString();
                fr.tempEmail = dr[1].ToString();
            }
            conn.conn().Close();



            fr.Show();
        }



        private void btnTcSave_Click(object sender, EventArgs e)
        {
            //assigns all forms to an array to check values with a loop
            TextBox[] txtboxes = { txtTcPass, txtTcName, txtTcSurname, txtTcEmail, txtTcBrans };
            MaskedTextBox[] mskboxes = { mskTcUsername };

            //temporary values to keep result of loop
            int txtboxContol = 0, mskboxControl = 0;
            //loop for check all textboxes are null or not. if null or empty temporary value is raising.
            for (int i = 0; i < txtboxes.Length; i++)
            {

                if (txtboxes[i].Text == "" || txtboxes[i].Text == null || txtboxes[i].Text == string.Empty)
                {
                    txtboxContol++;
                }
            }
            //loop for check all maskedboxes are null or not. if null or empty temporary value is raising.
            for (int y = 0; y < mskboxes.Length; y++)
            {
                if (mskboxes[y].Text == "" || mskboxes[y].Text == null || mskboxes[y].Text == string.Empty)
                {

                    mskboxControl++;
                }
            }
            //if there are empty error message shows up
            if (txtboxContol > 0 || mskboxControl > 0)
            {
                MessageBox.Show("Please Provide All informations", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SaveToGrid("Tbl_Teachers (tcusername, tcpass, tcname, tcsurname, tcbrans, tcemail, tcmobileno, tcorst)", mskTcUsername, txtTcPass, txtTcName, txtTcSurname, txtTcBrans, txtTcEmail, mskTcMobile, tempFrom);
                TeacherClean();
                pnlSt.Enabled = true;
                DeleteFromGrid("Tbl_RegisterRequest", "reqid", tempId);
                switchId = 0;
                ListItBaby("execute ListRequest ");
            }
        }

        private void btnStSave_Click(object sender, EventArgs e)
        {
            //same steps with teacher save button
            TextBox[] txtboxes = { txtStPass, txtStName, txtStSurname, txtEmail, txtClass };
            MaskedTextBox[] mskboxes = { mskStUserName };
           
            int txtboxContol = 0, mskboxControl = 0;

            for (int i = 0; i < txtboxes.Length; i++)
            {

                if (txtboxes[i].Text == "" || txtboxes[i].Text == null || txtboxes[i].Text == string.Empty)
                {
                    txtboxContol++;
                }
            }

            for (int y = 0; y < mskboxes.Length; y++)
            {
                if (mskboxes[y].Text == "" || mskboxes[y].Text == null || mskboxes[y].Text == string.Empty)
                {

                    mskboxControl++;
                }
            }


            if (txtboxContol > 0 || mskboxControl > 0)
            {
                MessageBox.Show("Please Provide All informations", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {


                SaveToGrid("Tbl_Students (stusername, stpass, stname, stsurname, stclass, stemail, stmobileno, stortc)", mskStUserName, txtStPass, txtStName, txtStSurname, txtClass, txtEmail, mskStMobile, tempFrom);
                StudentClean();
                pnlTc.Enabled = true;
                DeleteFromGrid("Tbl_RegisterRequest", "reqid", tempId);
                switchId = 0;
                ListItBaby("execute ListRequest ");
                TempCashClean();
            }


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Exit app
            frmLogin fr = new frmLogin();
            fr.Show();
            this.Close();
        }

        private void deleteDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //check if there is a selection
            if (tempId != "")
            {
                //0 means selection on request list table
                if (switchId == 0)
                {
                    DeleteFromGrid("Tbl_RegisterRequest", "reqid", tempId);
                    MessageBox.Show("Data has been DELETED", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListItBaby("execute ListRequest ");
                    TempCashClean();
                }
                //1 means selection on teachers table
                else if (switchId == 1)
                {
                    DeleteFromGrid("Tbl_Teachers", "ntcid", tempId);
                    MessageBox.Show("Data has been DELETED", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListItBaby("execute ListTeachers");
                    TempCashClean();
                }
                //-1 means selection on students table
                else
                {
                    DeleteFromGrid("Tbl_Students", "stid", tempId);
                    MessageBox.Show("Data has been DELETED", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListItBaby("execute ListStudents");
                    TempCashClean();
                }
            }
            //if there is no selecton
            else
            {
                MessageBox.Show("Please Select a USER from Table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
        //cell click of selection brings information
        public string tempUserName, tempPassword, tempName, tempSurname, tempEmail, tempMobile, tempClassBrans, tempId, tempFrom;

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
            //check if there is no selection
            if (tempId != "")
            {
                //if selected student
                if (tempFrom == "Student")
                {
                    //encyrpted
                    byte[] encode = Convert.FromBase64String(tempPassword);
                    string passnew = ASCIIEncoding.ASCII.GetString(encode);

                    //acces panel
                    pnlSt.Enabled = true;
                    //assign informations to forms
                    mskStUserName.Text = tempUserName;
                    txtStPass.Text = passnew;
                    txtStName.Text = tempName;
                    txtStSurname.Text = tempSurname;
                    txtClass.Text = tempClassBrans;
                    txtEmail.Text = tempEmail;
                    mskStMobile.Text = tempMobile;
                    //teacher panel clean and close
                    TeacherClean();
                    pnlTc.Enabled = false;
                }
                //if selected teacher
                else if (tempFrom == "Teacher")
                {
                    //encyrpted
                    byte[] encode = Convert.FromBase64String(tempPassword);
                    string passnew = ASCIIEncoding.ASCII.GetString(encode);
                    //acces panel
                    pnlTc.Enabled = true;
                    //assign informations to forms
                    mskTcUsername.Text = tempUserName;
                    txtTcPass.Text = passnew;
                    txtTcName.Text = tempName;
                    txtTcSurname.Text = tempSurname;
                    txtTcBrans.Text = tempClassBrans;
                    txtTcEmail.Text = tempEmail;
                    mskTcMobile.Text = tempMobile;
                    //student panel clean and close
                    StudentClean();
                    pnlSt.Enabled = false;

                }
            }
            //if there is no selection error message shows up
            else
            {
                MessageBox.Show("Please Select a USER from Table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnTcList_Click(object sender, EventArgs e)
        {
            //keeps values for delete process
            switchId = 1;
            //same steps with other list methods
            ListItBaby("execute ListTeachers ");

            settings(true, false, false, false, true, Color.LightGray, Color.LightGray, Color.LightGray, Color.White);

            TeacherClean();
            StudentClean();

            dataGridView1.BackgroundColor = Color.LightCoral;
            dataGridView1.DefaultCellStyle.BackColor = Color.LightCoral;
        }
        private void btnStList_Click(object sender, EventArgs e)
        {
            //keep info for delete method
            switchId = -1;
            //list process with sql procedure
            ListItBaby("execute ListStudents ");
            //button and form setting which is helping to use app
            settings(false, true, false, false, true, Color.White, Color.LightGray, Color.LightGray, Color.LightGray);
            //enable teacher panel to make usable list button
            pnlTc.Enabled = true;
            //cleaning
            TeacherClean();
            StudentClean();
            //table specification
            dataGridView1.BackgroundColor = Color.PaleGreen;
            dataGridView1.DefaultCellStyle.BackColor = Color.PaleGreen;
        }

        private void btnReqList_Click(object sender, EventArgs e)
        {

            switchId = 0;
            //List request used with sql procedure and c# method
            ListItBaby("execute ListRequest ");
            //To use app more effective unnecessay buttons anre disabled and their colors are changed to gray
            settings(false, false, true, true, true, Color.LightGray, Color.White, Color.White, Color.LightGray);

            pnlTc.Enabled = true;
            //cleans forms
            TeacherClean();
            StudentClean();
            //color specification for request list
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
