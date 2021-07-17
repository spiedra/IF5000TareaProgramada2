using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Node.Utility
{
    class MyUtility
    {
        /// <summary>
        /// obtiene archivo de nodo segun nombre de nodo y archivo
        /// <param name="request">Mensaje a realizarle split</param>
        /// <param name="index">posicion del mensaje a recuperar</param>
        /// </summary>
        public static string splitTheClientRequest(string request, int index)
        {
            string[] messaje = request.Split('*');
            return messaje[index];
        }
        /// <summary>
        /// convertir archivo a arreglo de bytes
        /// <param name="path">ruta del archivo a convertir</param>
        /// </summary>
        public static byte[] ConvertFileToByteArray(string path)
        {
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, (int)fs.Length);
            return buffer;
        }
    }
}
