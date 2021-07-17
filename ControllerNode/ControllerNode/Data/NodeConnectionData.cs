using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ControllerNode.Data
{
    /// <summary>
    /// Clase que gestiona toda la parte de los datos
    /// </summary>
    class NodeConnectionData
    {
        /// <summary>
        /// Referencia de SqlCommand
        /// </summary>
        private SqlCommand sqlCommand;

        /// <summary>
        /// Referencia de SqlConnection
        /// </summary>
        private SqlConnection sqlConnection;

        /// <summary>
        /// Referencia de SqlDataReader
        /// </summary>
        private SqlDataReader sqlDataReader;

        /// <summary>
        /// Metodo que elimina la ruta de los nodos registrados en la base de datos
        /// </summary>
        public void DeleteNodes()
        {
            string commandText = "dbo.sp_BORRAR_NODOS";
            this.InitSqlClientComponents(commandText);
            this.ExecuteNonQuery();
        }

        /// <summary>
        /// Registra la ruta de los nodos en la base de datos
        /// </summary>
        /// <param name="directory">Directorio del nodo</param>
        public void RegisterNode(string directory)
        {
            string paramDirectory = "@param_DIRECTORIO", commandText = "dbo.sp_INSERTAR_NODOS";
            this.InitSqlClientComponents(commandText);
            this.CreateParameter(paramDirectory, SqlDbType.VarChar, directory);
            this.ExecuteNonQuery();
        }

        /// <summary>
        /// Inserta los nombres de los archivos en la base de datos
        /// </summary>
        /// <param name="fileName">Nombre del archivo</param>
        public void InsertFile(string fileName)
        {
            string paramFile = "@param_archivo"
               , commandText = "dbo.sp_INSERTAR_ARCHIVO";
            this.InitSqlClientComponents(commandText);
            this.CreateParameter(paramFile, SqlDbType.VarChar, fileName);
            this.ExecuteNonQuery();
        }

        /// <summary>
        /// Obtiene la cantidad de nodos registrados en la base de datos
        /// </summary>
        /// <returns></returns>
        public int GetNumberNodes()
        {
            string commandText = "dbo.sp_OBTENER_CANTIDAD_NODOS";
            this.InitSqlClientComponents(commandText);
            this.ExcecuteReader();
            return ReadGetNumberNodes();
        }

        /// <summary>
        /// Obtiene la lista de nombres de los archivos
        /// </summary>
        /// <returns></returns>
        public List<string> GetListFile()
        {
            string commandText = "dbo.sp_GET_FILE_NAMES";
            this.InitSqlClientComponents(commandText);
            this.ExcecuteReader();
            return ReadGetListFile();
        }

        /// <summary>
        /// Lee la respuesta de la base de datos de la solicitud de la lista con los nombres de los archivos
        /// </summary>
        /// <returns>Lista con los nombres de los archivos</returns>
        private List<string> ReadGetListFile()
        {
            List<string> listFileNames = new();
            while (this.sqlDataReader.Read())
            {
                listFileNames.Add(sqlDataReader.GetString(0));
            }
            this.sqlConnection.Close();
            return listFileNames;
        }

        /// <summary>
        /// Lee la respuesta de la base de datos de la solicitud de la cantidad de nodos registrados
        /// </summary>
        /// <returns>Cantidad de nodos registrados en la base de datos</returns>
        private int ReadGetNumberNodes()
        {
            this.sqlDataReader.Read();
            var numberNodes = this.sqlDataReader.GetInt32(0);
            this.sqlConnection.Close();
            return numberNodes;
        }

        /// <summary>
        /// Inserta en la base de datos los fragmentos de los archivos
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fragmentName"></param>
        /// <param name="nodeName"></param>
        public void InsertFragment(string fileName, string fragmentName, string nodeName)
        {
            string paramNode = "@param_nodo"
                , paramFragment = "@param_fragmento"
                , paramArchivo = "@param_archivo"
                , commandText = "dbo.sp_INSERTAR_FRAGMENTO";
            this.InitSqlClientComponents(commandText);
            this.CreateParameter(paramNode, SqlDbType.VarChar, nodeName);
            this.CreateParameter(paramFragment, SqlDbType.VarChar, fragmentName);
            this.CreateParameter(paramArchivo, SqlDbType.VarChar, fileName);
            this.ExecuteNonQuery();
        }

        /// <summary>
        /// Le pregunta a la base de datos si existe una nueva configuración de sistema
        /// </summary>
        /// <returns>Devuleve 1 si existe una nuevo configuracion de otra manera 0</returns>
        public int IsNewConfigFlag()
        {
            string commandText = "dbo.sp_IS_NEW_CONFIG";
            InitSqlClientComponents(commandText);
            ExcecuteReader();
            return ReadIsNewConfigFlag();
        }

        /// <summary>
        /// Actualiza la bandera que indica si existe una nueva configuración 
        /// </summary>
        public void UpdateCongfigFlag()
        {
            string commandText = "dbo.sp_DELETE_REGISTER_CONFIG";
            InitSqlClientComponents(commandText);
            ExecuteNonQuery();
        }

        /// <summary>
        /// Lee la respuesta de la base de datos
        /// </summary>
        /// <returns>Devuleve 1 si existe una nuevo configuracion de otra manera 0</returns>
        private int ReadIsNewConfigFlag()
        {
            this.sqlDataReader.Read();
            var response = this.sqlDataReader.GetInt32(0); 
            this.sqlConnection.Close();
            return response;
        }

        /// <summary>
        /// Inicializa los sql components para la conexion con la base de datos
        /// </summary>
        /// <param name="commandText"></param>
        private void InitSqlClientComponents(string commandText)
        {
            this.sqlConnection = (SqlConnection)AccessConnection.ConnectToDatabase();
            this.sqlCommand = new SqlCommand(commandText, this.sqlConnection);
        }


        /// <summary>
        /// Crea parametros para el uso de procedimientos almacenados
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        private void CreateParameter(string parameterName, SqlDbType dbType, object value)
        {
            SqlParameter sqlParameter = new(parameterName, dbType);
            sqlParameter.Value = value;
            this.sqlCommand.Parameters.Add(sqlParameter);
        }

        /// <summary>
        /// Ejecuta consultas sin retorno
        /// </summary>
        private void ExecuteNonQuery()
        {
            this.sqlConnection.Open();
            this.sqlCommand.CommandType = CommandType.StoredProcedure;
            this.sqlCommand.ExecuteNonQuery();
            this.sqlConnection.Close();
        }

        /// <summary>
        /// Ejecuta consultas con retorno
        /// </summary>
        private void ExcecuteReader()
        {
            this.ExecuteConnectionCommands();
            this.sqlDataReader = this.sqlCommand.ExecuteReader();
        }

        /// <summary>
        /// Ejecuta los comandos para la conexión
        /// </summary>
        private void ExecuteConnectionCommands()
        {
            this.sqlConnection.Open();
            this.sqlCommand.CommandType = CommandType.StoredProcedure;
        }
    }
}