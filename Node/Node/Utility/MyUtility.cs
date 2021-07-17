using System.IO;

namespace Node.Utility
{
    /// <summary>
    /// Clase estatica que contienen los metodos comunmente usados para el funcionamiento de la aplicación
    /// </summary>
    class MyUtility
    {
        /// <summary>
        /// Permite separar la cadena enviada por el cliente
        /// </summary>
        /// <param name="request">Mensaje a dividir</param>
        /// <param name="index">Indice de la pieza de la cadena</param>
        /// <returns>Devuelve la pieza solicitada de la cadena</returns>
        public static string SplitTheClientRequest(string request, int index)
        {
            string[] messaje = request.Split('*');
            return messaje[index];
        }

        /// <summary>
        /// Convierte los archivos .txt en un arreglo de bytes
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Devulve un archivo .txt convertido en un arreglo de bytes</returns>
        public static byte[] ConvertFileToByteArray(string path)
        {
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, (int)fs.Length);
            return buffer;
        }
    }
}
