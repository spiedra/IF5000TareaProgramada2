using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saSearch.Client
{
    class NodoApagadoSingleton
    {
        private static NodoApagadoSingleton nodoApagado = null;
        public int IndexApagado { get; set; }
        public bool EsNodoApagado { get; set; }

        private NodoApagadoSingleton()
        {
            this.IndexApagado = -1;
            this.EsNodoApagado = false;
        }

        public static NodoApagadoSingleton getApagadoSingleton()
        {
            if (nodoApagado == null)
            {
                nodoApagado = new();
            }
            return nodoApagado;
        }
    }
}
