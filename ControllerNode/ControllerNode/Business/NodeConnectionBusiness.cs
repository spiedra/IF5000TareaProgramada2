using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IF500_tftp_server.Data;
using IF500_tftp_server.Utility;

namespace IF500_tftp_server.Business
{
    class NodeConnectionBusiness
    {
        private NodeConnectionData userConnection;

        public NodeConnectionBusiness()
        {
            this.userConnection = new NodeConnectionData();
        }
        public void DeleteNodes()
        {
            this.userConnection.DeleteNodes();
        }

        public void RegisterNode(string directory)
        {
            this.userConnection.RegisterNode(directory);
        }

        public void InsertFragment(string fileName, string fragment, string node)
        {
            this.userConnection.InsertFragment(fileName, fragment, node);
        }

        public void InsertFile(string fileName, string last_mod, string size)
        {
            this.InsertFile(fileName, last_mod, size);
        }
    }
}