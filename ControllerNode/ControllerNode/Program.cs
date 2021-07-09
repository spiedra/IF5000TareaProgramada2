using System;

namespace ControllerNode
{
    class Program
    {
        static void Main(string[] args)
        {
            Server s = new Server("localhost", 4404);
            s.Start();
        }
    }
}
