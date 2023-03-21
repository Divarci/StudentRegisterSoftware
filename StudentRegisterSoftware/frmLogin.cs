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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        sqlconnection conn = new sqlconnection();

        private void btnSI_Click(object sender, EventArgs e)
        {
            if (cb1.Checked)
            {

                SqlCommand cmd = new SqlCommand("Select stusername,stpass,stname,stsurname,stid from Tbl_Students where stusername=@p1 and stpass=@p2", conn.conn());
                cmd.Parameters.AddWithValue("@p1", mskId.Text);
                cmd.Parameters.AddWithValue("@p2", txtPass.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    mskId.Text = dr[0].ToString();
                    txtPass.Text = dr[1].ToString();

                    frmStudentPage fr = new frmStudentPage();
                    fr.tempUserNameSt = dr[0].ToString();
                    fr.tempNameSt = dr[2].ToString();
                    fr.tempSurnameSt = dr[3].ToString();
                    fr.tempIdSt = dr[4].ToString();


                    fr.Show();
                    this.Hide();


                }
                else
                {

                    SqlCommand cmd2 = new SqlCommand("Select tcusername,tcpass,tcName,tcSurname,tcid from Tbl_Teachers where tcusername=@p1 and tcpass=@p2", conn.conn());
                    cmd2.Parameters.AddWithValue("@p1", mskId.Text);
                    cmd2.Parameters.AddWithValue("@p2", txtPass.Text);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.Read())
                    {
                        mskId.Text = dr2[0].ToString();
                        txtPass.Text = dr2[1].ToString();

                        frmExamEntry fr = new frmExamEntry();
                        fr.tempUserName = dr2[0].ToString();
                        fr.tempName = dr2[2].ToString();
                        fr.tempSurname = dr2[3].ToString();
                        fr.temptcid = dr2[4].ToString();

                        fr.Show();
                        this.Hide();

                    }
                    else
                    {

                        SqlCommand cmd3 = new SqlCommand("Select admusername,admpass,admname,admsurname,admid from Tbl_Secretaries where admusername=@p1 and admpass=@p2", conn.conn());
                        cmd3.Parameters.AddWithValue("@p1", mskId.Text);
                        cmd3.Parameters.AddWithValue("@p2", txtPass.Text);
                        SqlDataReader dr3 = cmd3.ExecuteReader();
                        if (dr3.Read())
                        {
                            mskId.Text = dr3[0].ToString();
                            txtPass.Text = dr3[1].ToString();

                            frmSecretaryPage fr = new frmSecretaryPage();
                            fr.tempUserNameAdm = dr3[0].ToString();
                            fr.tempNameAdm = dr3[2].ToString();
                            fr.tempSurnameAdm = dr3[3].ToString();
                            fr.tempIdAdm = dr3[4].ToString();

                            fr.Show();
                            this.Hide();



                        }
                        else
                        {
                            MessageBox.Show("Please provide valid information", "Id or Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            mskId.Text = "";
                            txtPass.Text = "";
                            mskId.Focus();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please provide valid information", "Id or Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskId.Text = "";
                txtPass.Text = "";
                mskId.Focus();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void llSU_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSignUp fr = new frmSignUp();
            fr.Show();
            this.Hide();
        }
    }
}
