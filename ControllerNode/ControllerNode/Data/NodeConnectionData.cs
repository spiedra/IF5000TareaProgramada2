using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using IF500_tftp_server.Data;
using System.Drawing;
using System.IO;
using IF500_tftp_server.Utility;

namespace IF500_tftp_server.Data
{
    class NodeConnectionData
    {
        private SqlCommand sqlCommand;
        private SqlConnection sqlConnection;
        private SqlDataReader sqlDataReader;

        public NodeConnectionData()
        {

        }

        public void DeleteNodes()
        {
            string commandText = "dbo.sp_DELETE_NODES";
            this.InitSqlClientComponents(commandText);
            this.ExecuteNonQuery();
        }

        public void RegisterNode(string directory)
        {
            string paramDirectory = "@param_DIRECTORIO", commandText = "dbo.sp_INSERT_NODE";
            this.InitSqlClientComponents(commandText);
            this.CreateParameter(paramDirectory, SqlDbType.VarChar, directory);
            this.ExecuteNonQuery();
        }

        public void InsertFile(string fileName, string last_mod, string size)
        {
            string paramFile = "@param_archivo"
               , paramLastMod = "@param_ult_mod"
               , paramSize = "@param_tamano"
               , commandText = "dbo.sp_INSERTAR_ARCHIVO";
            this.InitSqlClientComponents(commandText);
            this.CreateParameter(paramFile, SqlDbType.VarChar,fileName);
            this.CreateParameter(paramLastMod, SqlDbType.VarChar, last_mod);
            this.CreateParameter(paramSize, SqlDbType.VarChar, size);
            this.ExecuteNonQuery();
        }

        public void InsertFragment(string fileName, string fragment, string node)
        {
            string paramNode = "@param_nodo"
                , paramFragment="@param_fragmento"
                ,paramArchivo="@param_archivo"
                ,commandText = "dbo.sp_INSERTAR_FRAGMENTO";
            this.InitSqlClientComponents(commandText);
            this.CreateParameter(paramNode, SqlDbType.VarChar, node);
            this.CreateParameter(paramFragment, SqlDbType.VarChar, fragment);
            this.CreateParameter(paramArchivo, SqlDbType.VarChar, fileName);
            this.ExecuteNonQuery();
        }
     
        private void InitSqlClientComponents(string commandText)
        {
            AccessConnection accessConnection = new AccessConnection();
            this.sqlConnection = (SqlConnection)accessConnection.ConnectToDatabase();
            this.sqlCommand = new SqlCommand(commandText, this.sqlConnection);
        }

        private void CreateParameter(string parameterName, SqlDbType dbType, object value)
        {
            SqlParameter sqlParameter = new SqlParameter(parameterName, dbType);
            sqlParameter.Value = value;
            this.sqlCommand.Parameters.Add(sqlParameter);
        }

        private void ExecuteNonQuery()
        {
            this.sqlConnection.Open();
            this.sqlCommand.CommandType = CommandType.StoredProcedure;
            this.sqlCommand.ExecuteNonQuery();
            this.sqlConnection.Close();
        }

        private void ExecuteConnectionCommands()
        {
            this.sqlConnection.Open();
            this.sqlCommand.CommandType = CommandType.StoredProcedure;
            this.sqlDataReader = this.sqlCommand.ExecuteReader();
        }
    }
}