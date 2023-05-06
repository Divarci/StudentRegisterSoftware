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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StudentRegisterSoftware
{
    public partial class frmStudentPage : Form
    {
        public frmStudentPage()
        {
            InitializeComponent();
        }
        //sql connection
        sqlconnection conn = new sqlconnection();
        //temporary values
        public string tempUserNameSt, tempNameSt, tempSurnameSt, tempIdSt;
        //Select and count failed or passed exams
        public void FailPass(string status)
        {
            SqlCommand cmd = new SqlCommand("Select Count (resstatus)  from Tbl_Exams inner join Tbl_Students on Tbl_Exams.resstid=Tbl_Students.stid where stid=@p1 and resstatus=@p2", conn.conn());
            cmd.Parameters.AddWithValue("@p1", tempIdSt);
            cmd.Parameters.AddWithValue("@p2", status);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                btnText.Text = dr[0].ToString();
            }
            conn.conn().Close();
        }
        //select top first avg exam method
        public void SortBy(string sortit)
        {
            SqlCommand cmd = new SqlCommand("Select top 1 resexavg  from Tbl_Exams inner join Tbl_Students on Tbl_Exams.resstid=Tbl_Students.stid where stid=@p1 order by resexavg " + sortit, conn.conn());
            cmd.Parameters.AddWithValue("@p1", tempIdSt);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                btnText.Text = dr[0].ToString();
            }
            conn.conn().Close();
        }
        //gives us how many leeson passed
        private void btnLessPass_Click(object sender, EventArgs e)
        {

            FailPass("Passed");
        }
        //gives us how many leeson passed with DESC
        private void btnBestGr_Click(object sender, EventArgs e)
        {
            SortBy("desc");
        }
        //gives us how many leeson passed with ASC

        private void btnWorstGr_Click(object sender, EventArgs e)
        {
            SortBy("asc");
        }
        //assgign infos to a chart
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            chart1.Series["Exams"].Points.Clear();
            int choosen = dataGridView1.SelectedCells[0].RowIndex;
            chart1.Series["Exams"].Points.AddXY("EXAM 1", dataGridView1.Rows[choosen].Cells[2].Value);
            chart1.Series["Exams"].Points.AddXY("EXAM 2", dataGridView1.Rows[choosen].Cells[3].Value);
            chart1.Series["Exams"].Points.AddXY("EXAM 3", dataGridView1.Rows[choosen].Cells[4].Value);
            lblSelectedLesson.Text = dataGridView1.Rows[choosen].Cells[1].Value.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //leads you message page
            frmStudentMessage fr = new frmStudentMessage();
            fr.tempUserNameSt = tempUserNameSt;
            fr.tempIdSt = tempIdSt;
            fr.tempNameSt = tempNameSt;
            fr.tempSurnameSt = tempSurnameSt;
            fr.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //leads yo to userinformtion page
            frmUserInformation fr = new frmUserInformation();
            fr.TempId = tempIdSt;
            SqlCommand cmd = new SqlCommand("Select stmobileno,stemail from Tbl_Students where stid=@p1", conn.conn());
            cmd.Parameters.AddWithValue("@p1", tempIdSt);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                fr.tempMobno = dr[0].ToString();
                fr.tempEmail = dr[1].ToString();
            }
            conn.conn().Close();



            fr.Show();

        }

        private void btnLessFail_Click(object sender, EventArgs e)
        {
            //Uses a method  to receive infos
            FailPass("Failed");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //leds you o login
            frmLogin fr = new frmLogin();
            fr.Show();
            this.Close();
        }

        private void frmStudentPage_Load(object sender, EventArgs e)
        {
            //assign infos to the prioratry
            SqlCommand cmd = new SqlCommand("select (tcname+' '+tcsurname) as 'TEACHER', tcbrans as 'LESSONS', resexone as'EXAM 1', resextwo as'EXAM 2', resexthree as'EXAM 3', resexavg as'AVERAGE', resstatus as'STATUS' from Tbl_Exams \r\ninner join Tbl_Teachers\r\non Tbl_Exams.restcid=Tbl_Teachers.ntcid\r\ninner join Tbl_Students\r\non Tbl_Exams.resstid=Tbl_Students.stid where stid=@p1", conn.conn());
            cmd.Parameters.AddWithValue("@p1", tempIdSt);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            conn.conn().Close();

            chart1.Series["Exams"].Points.AddXY("EXAM 1", 1);
            chart1.Series["Exams"].Points.AddXY("EXAM 2", 1);
            chart1.Series["Exams"].Points.AddXY("EXAM 3", 1);

            lblTcNameSurname.Text = tempNameSt + " " + tempSurnameSt;
        }
    }
}
