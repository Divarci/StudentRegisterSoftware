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
    public partial class frmExamEntry : Form
    {
        public frmExamEntry()
        {
            InitializeComponent();
        }
        public void AvarageCalvulation()
        {
            int x, y, z;
            x = Convert.ToInt16(txtExamFirst.Text);
            y = Convert.ToInt16(txtExamSecond.Text);
            z = Convert.ToInt16(txtExamThird.Text);

            avg = (x + y + z) / 3;
        }

        public void ListViaIndexChange(string condition, ComboBox cmb)
        {
            SqlCommand cmd2 = new SqlCommand("select resid,(stname+' '+stsurname) as 'STUDENT NAME', (tcname+' '+tcsurname) as 'TEACHER NAME',tcbrans as 'LESSON', resexone as 'EXAM 1', resextwo as 'EXAM 2', resexthree as 'EXAM 3', resexavg as 'EXAM AVERAGE',resstatus as 'STATUS', restcid from Tbl_Exams \r\ninner join Tbl_Students\r\non Tbl_Exams.resstid = Tbl_Students.stid\r\ninner join Tbl_Teachers\r\non Tbl_Exams.restcid = Tbl_Teachers.Ntcid where " + condition, conn.conn());
            cmd2.Parameters.AddWithValue("@p1", cmb.SelectedValue);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
            dataGridView1.Columns[0].Visible = false;
        }

        public void ExamResultClear()
        {

            txtExamFirst.Text = "";
            txtExamSecond.Text = "";
            txtExamThird.Text = "";
        }

        sqlconnection conn = new sqlconnection();

        public string tempUserName, tempName, tempSurname, temptcid;
        string selectedIndex, examone, examtwo, examthree;
        int avg;
        string restcid;


        private void deleteDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // delete with right click menu


            if (restcid == temptcid)
            {
                DialogResult result = new DialogResult();
                result = MessageBox.Show("Are you sure to DELETE selected data?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("Delete from Tbl_Exams where resid=@p1", conn.conn());
                    cmd.Parameters.AddWithValue("@p1", selectedIndex);
                    cmd.ExecuteNonQuery();
                    conn.conn().Close();

                    MessageBox.Show("Data has been DELETED?", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ListViaIndexChange("stid=@p1", cmbStudent);
                }
            }
            else
            {
                MessageBox.Show("You can only Delete exam results belong to you", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void copyToBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {


            // condition seetings for update


            btnUpdate.Enabled = true;
            btnSave.Enabled = false;
            pictureBox4.Visible = true;
            btnSave.BackColor = Color.LightGray;
            btnUpdate.BackColor = Color.White;

            txtExamFirst.Text = examone;
            txtExamSecond.Text = examtwo;
            txtExamThird.Text = examthree;


        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            //update cancel settings
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnUpdate.BackColor = Color.LightGray;
            btnSave.BackColor = Color.White;
            cmbStudent.Text = "";
            cmbClass.Text = "";
            ExamResultClear();
            pictureBox4.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            frmLogin fr = new frmLogin();
            fr.Show();
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // update record
            try
            {

                if (restcid == temptcid)
                {
                    AvarageCalvulation();

                    SqlCommand cmd = new SqlCommand("update Tbl_Exams set resexone=@p1, resextwo=@p2, resexthree=@p3, resexavg=@p4, resstatus=@p5 where resid=@p6 and restcid=@p7", conn.conn());
                    cmd.Parameters.AddWithValue("@p1", txtExamFirst.Text);
                    cmd.Parameters.AddWithValue("@p2", txtExamSecond.Text);
                    cmd.Parameters.AddWithValue("@p3", txtExamThird.Text);
                    cmd.Parameters.AddWithValue("@p4", avg);
                    if (avg < 45)
                    {
                        cmd.Parameters.AddWithValue("@p5", "Failed");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@p5", "Passed");
                    }

                    cmd.Parameters.AddWithValue("@p6", selectedIndex);
                    cmd.Parameters.AddWithValue("@p7", Convert.ToInt16(temptcid));

                    cmd.ExecuteNonQuery();
                    conn.conn().Close();

                    MessageBox.Show("Exam Results are UPDATED?", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    ExamResultClear();

                    ListViaIndexChange("stid=@p1", cmbStudent);

                    pictureBox4.Enabled = false;
                    btnSave.Enabled = true;
                    btnSave.BackColor = Color.White;
                }
                else
                {
                    MessageBox.Show("You can only change exam results belong to you", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please provide valid information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // exam record
            try
            {
                AvarageCalvulation();

                SqlCommand cmd = new SqlCommand("Insert into Tbl_Exams (resstid,restcid,resexone,resextwo,resexthree,resexavg,resstatus) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", conn.conn());
                cmd.Parameters.AddWithValue("@p1", cmbStudent.SelectedValue);
                cmd.Parameters.AddWithValue("@p2", temptcid);
                cmd.Parameters.AddWithValue("@p3", txtExamFirst.Text);
                cmd.Parameters.AddWithValue("@p4", txtExamSecond.Text);
                cmd.Parameters.AddWithValue("@p5", txtExamThird.Text);
                cmd.Parameters.AddWithValue("@p6", avg);
                if (avg < 45)
                {
                    cmd.Parameters.AddWithValue("@p7", "Failed");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@p7", "Passed");
                }
                cmd.ExecuteNonQuery();
                conn.conn().Close();

                MessageBox.Show("Exam Results are Created?", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


                ExamResultClear();
                ListViaIndexChange("stid=@p1", cmbStudent);
            }
            catch (Exception)
            {
                MessageBox.Show("Please provide valid information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            // student combobox fill
            SqlCommand cmd = new SqlCommand("Select  stid,(stname + ' ' + stsurname) as Students from Tbl_Students where stclass = @p1", conn.conn());
            cmd.Parameters.AddWithValue("@p1", cmbClass.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbStudent.ValueMember = "stid";
            cmbStudent.DisplayMember = "Students";
            cmbStudent.DataSource = dt;
            cmbStudent.Text = "";
            /*
            // selected class of all students fill
            SqlCommand cmd2 = new SqlCommand("select resid,(stname+' '+stsurname) as 'STUDENT NAME', (tcname+' '+tcsurname) as 'TEACHER NAME',tcbrans as 'LESSON', resexone as 'EXAM 1', resextwo as 'EXAM 2', resexthree as 'EXAM 3', resexavg as 'EXAM AVERAGE',resstatus as 'STATUS', restcid from Tbl_Exams \r\ninner join Tbl_Students\r\non Tbl_Exams.resstid = Tbl_Students.stid\r\ninner join Tbl_Teachers\r\non Tbl_Exams.restcid = Tbl_Teachers.tcid where stclass=@p1", conn.conn());
            cmd2.Parameters.AddWithValue("@p1", cmbClass.SelectedValue);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
            dataGridView1.Columns[0].Visible = false;
            */

            ListViaIndexChange("stclass=@p1", cmbClass);

        }

        private void cmbStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
             //selected student
             SqlCommand cmd2 = new SqlCommand("select resid,(stname+' '+stsurname) as 'STUDENT NAME', (tcname+' '+tcsurname) as 'TEACHER NAME',tcbrans as 'LESSON', resexone as 'EXAM 1', resextwo as 'EXAM 2', resexthree as 'EXAM 3', resexavg as 'EXAM AVERAGE' ,resstatus as 'STATUS', restcid from Tbl_Exams \r\ninner join Tbl_Students\r\non Tbl_Exams.resstid = Tbl_Students.stid\r\ninner join Tbl_Teachers\r\non Tbl_Exams.restcid = Tbl_Teachers.tcid where stid=@p1", conn.conn());
             cmd2.Parameters.AddWithValue("@p1", cmbStudent.SelectedValue);
             SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
             DataTable dt2 = new DataTable();
             da2.Fill(dt2);
             dataGridView1.DataSource = dt2;
             dataGridView1.Columns[0].Visible = false;
            */

            ListViaIndexChange("stid=@p1", cmbStudent);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //select info from table
            int choosen = dataGridView1.SelectedCells[0].RowIndex;
            selectedIndex = dataGridView1.Rows[choosen].Cells[0].Value.ToString();
            examone = dataGridView1.Rows[choosen].Cells[4].Value.ToString();
            examtwo = dataGridView1.Rows[choosen].Cells[5].Value.ToString();
            examthree = dataGridView1.Rows[choosen].Cells[6].Value.ToString();
            restcid = dataGridView1.Rows[choosen].Cells[9].Value.ToString();


        }

        private void frmExamEntry_Load(object sender, EventArgs e)
        {
            //  info fill
            lblTcNameSurname.Text = tempName + " " + tempSurname + " - TEACHER ";
            btnUpdate.Enabled = false;
            btnUpdate.BackColor = Color.LightGray;

            // class combobox fill
            SqlDataAdapter da = new SqlDataAdapter("Select DISTINCT stclass from Tbl_Students", conn.conn());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbClass.ValueMember = "stclass";
            cmbClass.DisplayMember = "stclass";
            cmbClass.DataSource = dt;
            cmbClass.Text = "";



        }
    }
}
