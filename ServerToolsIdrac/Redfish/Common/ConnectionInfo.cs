using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsIdrac.Redfish.Common
{
    public class ConnectionInfo
    {
        private string host;
        private string user;
        private string password;

        public string Host { get => host; set => host = value; }
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
    }
}
