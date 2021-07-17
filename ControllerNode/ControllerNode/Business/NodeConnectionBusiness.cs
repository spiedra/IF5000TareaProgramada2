using System.Collections.Generic;
using IF500_tftp_server.Data;

namespace IF500_tftp_server.Business
{
    /// <summary>
    /// Clase logica de negocio de la entidad <b>Node</b>
    /// </summary>
    /// <remarks>
    /// Permite la comunicación entre la capa <b>Data</b> y <b>Server</b> para una apropiada distribución de responsabilidades
    /// </remarks>
    class NodeConnectionBusiness
    {
        /// <summary>
        /// Variable que permite instanciar a la clase nodeConnectionData
        /// </summary>
        /// <remarks>
        /// Solo puede ser instanciada en el constructor de la clase
        /// </remarks>
        private readonly NodeConnectionData nodeConnectionData;

        /// <summary>
        /// Constructor por defecto, en donde se crea la instancia de la variable "nodeConnectionData"
        /// </summary>
        public NodeConnectionBusiness()
        {
            this.nodeConnectionData = new NodeConnectionData();
        }

        /// <summary>
        /// Llama al DeleteNodes nodos de la clase NodeConnectionData para elmiminar nodos
        /// </summary>
        public void DeleteNodes()
        {
            this.nodeConnectionData.DeleteNodes();
        }

        /// <summary>
        /// Llama el metodo RegisterNode de la clase NodeConnectionData para registrar nodos
        /// </summary>
        /// <param name="directory">Ruta del directorio para registrar el nodo</param>
        public void RegisterNode(string directory)
        {
            this.nodeConnectionData.RegisterNode(directory);
        }

        /// <summary>
        /// Llama el metodo InsertFragment de la clase NodeConnectionData para insertar los fragmentos tanto de los archivos como de los metadatos
        /// </summary>
        /// <param name="fileName">Nombre del archivo</param>
        /// <param name="fragment">Fragmento del archivo</param>
        /// <param name="node">Nombre del nodo en donde se va a ubicar los fragmentos</param>
        public void InsertFragment(string fileName, string fragment, string node)
        {
            this.nodeConnectionData.InsertFragment(fileName, fragment, node);
        }

        /// <summary>
        /// Inserta en 
        /// </summary>
        /// <param name="fileName"></param>
        public void InsertFile(string fileName)
        {
            this.nodeConnectionData.InsertFile(fileName);
        }

        /// <summary>
        /// Llama el metodo GetNumberNodes de la clase nodeConnectionData para obtener los numeros de nodos del sistema
        /// </summary>
        /// <returns>Devulve el numero de nodos que se encuentran actualmente en el sistema</returns>
        public int GetNumberNodes()
        {
            return this.nodeConnectionData.GetNumberNodes();
        }

        /// <summary>
        /// Llama el metodo GetListFile de la clase nodeConnectionData para obtener la lista de los nombres de archivos
        /// </summary>
        /// <returns>Devuelve una lista de cadenas con los nombres de los archivos ingresados en la base de datos</returns>
        public List<string> GetListFile()
        {
            return this.nodeConnectionData.GetListFile();
        }
    }
}