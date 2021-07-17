namespace Node
{
    /// <summary>
    /// Clase principal del proyecto
    /// </summary>
    class Program
    {
        /// <summary>
        /// Crea una nueva instancia de node
        /// </summary>
        /// <remarks>
        /// El constructor de la clase <b>Node</b> ejecuta una hilo para poder conectarse al servidor
        /// </remarks>
        /// <param name="args"></param>
        static void Main()
        {
            _ = new MyNode.Node();
        }
    }
}
