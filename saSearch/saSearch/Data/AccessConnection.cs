using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IF500_tftp_server.Data
{
    class AccessConnection
    {

        public AccessConnection()
        {
            this.ConnectToDatabase();
        }

        public object ConnectToDatabase()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
                return sqlConnection;
            }
            catch (SqlException sqlException)
            {
                return sqlException.Number;
            }
        }

        static private string GetConnectionString()
        {
            return "Data Source=163.178.107.10; database=IF5000_tarea2_B95212_B97452; User Id=laboratorios; Password=KmZpo.2796";
        }
    }
}
