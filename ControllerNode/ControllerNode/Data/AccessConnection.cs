using System.Data.SqlClient;

namespace ControllerNode.Data
{
    /// <summary>
    /// Clase que permite la conexión con la base de datos
    /// </summary>
    class AccessConnection
    {
        /// <summary>
        /// Constructor de la clase <b>AccessConnection</b>
        /// </summary>
        public AccessConnection()
        {
            ConnectToDatabase();
        }

        /// <summary>
        /// Permite conectarse con la base de datos 
        /// </summary>
        /// <returns>Devuelve un objecto sqlConnection (Conexión)</returns>
        public static object ConnectToDatabase()
        {
            try
            {
                SqlConnection sqlConnection = new(GetConnectionString());
                return sqlConnection;
            }
            catch (SqlException sqlException)
            {
                return sqlException.Number;
            }
        }

        /// <summary>
        /// Devuelve la <b>Cadena de conexion</b>, que permite conexion con la base de datos
        /// </summary>
        /// <returns>Devuelve la cadena de conexión</returns>
        static private string GetConnectionString()
        {
            return "Data Source=163.178.107.10; database=IF5000_tarea2_B95212_B97452; User Id=laboratorios; Password=KmZpo.2796";
        }
    }
}
