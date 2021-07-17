using ControllerNode.MyServer;

namespace ControllerNode
{
    /// <summary>
    /// Clase principal del proyecto
    /// </summary>
    class Program
    {
        /// <summary>
        /// Hace un llamado al hilo del servido para que se empieze a ejecutar
        /// </summary>
        static void Main()
        {
            Server s = new("localhost", 4404);
            s.Start();
        }
    }
}
