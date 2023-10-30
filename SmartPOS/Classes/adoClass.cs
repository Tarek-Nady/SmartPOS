using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SmartPOS.Classes
{
    public class adoClass
    {
        public static SqlConnection sqlcn;
        public static SqlCommandBuilder builder;
        public static void setConnection()
        {
            sqlcn = new SqlConnection(SmartPOS.Properties.Settings.Default.connection);
        }
    }
}
