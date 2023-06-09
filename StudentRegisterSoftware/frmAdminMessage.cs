﻿using System;
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
    public partial class frmAdminMessage : Form
    {
        public frmAdminMessage()
        {
            InitializeComponent();
        }
        //sql connection
        sqlconnection conn = new sqlconnection();

        public string tempUserNameAdm, tempNameAdm, tempSurnameAdm, tempIdAdm;
        //collects message list
        public void MessageList(string query, DataGridView dg)
        {
            SqlCommand cmd = new SqlCommand("Select msgid,msgfromid,msgfromfullname as 'SENDER',msgtoid,msgtofullname as 'TO SENT',subject as 'SUBJECT',description as 'DESCRIPTION' from Tbl_Messages where " + query + "=@p1", conn.conn());
            cmd.Parameters.AddWithValue("@p1", tempIdAdm);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dg.DataSource = dt;
            dg.Columns[0].Visible = false;
            dg.Columns[1].Visible = false;
            dg.Columns[3].Visible = false;
        }

        private void frmAdminMessage_Load(object sender, EventArgs e)
        {
            //list all messages to the charts
            MessageList("msgtoid", dataGridView1);
            MessageList("msgfromid", dataGridView2);
            //adds 3 item to combobox
            string[] groups = { "Teacher", "Student", "Admin" };

            for (int i = 0; i < groups.Length; i++)
            {
                cmbSelectGroup.Items.Add(groups[i]);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //send message
            SqlCommand cmd = new SqlCommand("Insert into Tbl_Messages (msgfromid,msgfromfullname,msgtoid,msgtofullname,subject,description) values (@p1,@p2,@p3,@p4,@p5,@p6)", conn.conn());
            cmd.Parameters.AddWithValue("@p1", tempIdAdm);
            cmd.Parameters.AddWithValue("@p2", tempNameAdm + " " + tempSurnameAdm);
            cmd.Parameters.AddWithValue("@p3", cmbToSend.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@p4", cmbToSend.Text);
            cmd.Parameters.AddWithValue("@p5", txtSubject.Text);
            cmd.Parameters.AddWithValue("@p6", rchDescription.Text);
            cmd.ExecuteNonQuery();
            conn.conn().Close();
            //list all messages
            MessageList("msgfromid", dataGridView2);
            CleanBoxes();
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            //updates received messages
            MessageList("msgtoid", dataGridView1);
            CleanBoxes();
        }
        //brings relevant people by their group
        private void cmbSelectGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbSelectGroup.Text)
            {
                case "Teacher":
                    //assign combobox
                    SelectGroup("Select ntcid, (tcname + ' ' + tcsurname) as 'Teachers' from Tbl_Teachers", "ntcid", "Teachers");

                    break;
                case "Student":
                    //assign combobox
                    SelectGroup("Select stid, (stname+' '+stsurname) as 'Students' from Tbl_Students", "stid", "Students");

                    break;
                case "Admin":
                    //assign combobox
                    SelectGroup("Select admid, (admname+' ' +admsurname) as 'Admins' from Tbl_Secretaries", "admid", "Admins");

                    break;
                default: break;

            }
        }

        //cleaning method
        public void CleanBoxes()
        {
            cmbSelectGroup.Text = "";
            cmbToSend.Text = "";
            txtSubject.Text = "";
            rchDescription.Text = "";
        }
        //combobox item assign method
        public void SelectGroup(string query, string col1, string col2)
        {
            cmbToSend.DataSource = null;
            cmbToSend.Items.Clear();

            SqlDataAdapter da = new SqlDataAdapter(query, conn.conn());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbToSend.ValueMember = col1;
            cmbToSend.DisplayMember = col2;
            cmbToSend.DataSource = dt;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //exit app
            this.Close();
        }
    }
}
