using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentRegisterSoftware
{
    public partial class frmSecretaryPage : Form
    {
        public frmSecretaryPage()
        {
            InitializeComponent();
        }

        sqlconnection conn = new sqlconnection();

        public string tempUserNameAdm, tempNameAdm, tempSurnameAdm, tempIdAdm;

        private void frmSecretaryPage_Load(object sender, EventArgs e)
        {

        }
    }
}
