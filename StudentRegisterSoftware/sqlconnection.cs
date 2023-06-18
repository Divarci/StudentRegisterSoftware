using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegisterSoftware
{
    internal class sqlconnection
    {
        public SqlConnection conn()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=HASAN\SQLEXPRESS;Initial Catalog=StudentRegister;Integrated Security=True");
            conn.Open();
            return conn;
            /*
            SqlConnection conn = new SqlConnection(@"Data Source=HASAN-DERS\SQLEXPRESS;Initial Catalog=StudentSoftware;Integrated Security=True");
            conn.Open();
            return conn;
            */
            
        }
    }
}
