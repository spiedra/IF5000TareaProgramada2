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
        /// <summary>
        /// Obtener cantidad de nodos en base de datos
        /// </summary>
        /// <returns>Retorna cantidad de nodos</returns>
        public int GetNodesCount()
        {
            string commandText = "dbo.sp_OBTENER_CANTIDAD_NODOS";
            this.InitSqlClientComponents(commandText);
            this.ExecuteConnectionCommands();
            this.sqlDataReader.Read();
            int cantidad= this.sqlDataReader.GetInt32(0);
            this.sqlConnection.Close();
            return cantidad;
        }
        /// <summary>
        /// Inicializa componentes sql
        /// </summary>
        /// <param name="commandText">comando sql a ejecutar</param>
        private void InitSqlClientComponents(string commandText)
        {
            AccessConnection accessConnection = new AccessConnection();
            this.sqlConnection = (SqlConnection)accessConnection.ConnectToDatabase();
            this.sqlCommand = new SqlCommand(commandText, this.sqlConnection);
        }
        /// <summary>
        /// Ejecuta el comando y el lector de la conexion
        /// </summary>
        private void ExecuteConnectionCommands()
        {
            this.sqlConnection.Open();
            this.sqlCommand.CommandType = CommandType.StoredProcedure;
            this.sqlDataReader = this.sqlCommand.ExecuteReader();
        }
    }
}