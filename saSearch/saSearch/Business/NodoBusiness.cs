using IF500_tftp_server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saSearch.Business
{
    class NodoBusiness
    {
        private NodeConnectionData nodeConnection;

        public NodoBusiness()
        {
            this.nodeConnection = new NodeConnectionData();
        }

        public int GetNodeCount()
        {
            return this.nodeConnection.GetNodesCount();
        }
    }
}
