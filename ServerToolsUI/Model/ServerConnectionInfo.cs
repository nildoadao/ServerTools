using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsUI.Model
{
    public class ServerConnectionInfo
    {
        private string hostname;
        private string user;
        private string password;

        public ServerConnectionInfo() { }

        public ServerConnectionInfo(string hostname, string user, string password)
        {
            this.hostname = hostname;
            this.user = user;
            this.password = password;
        }

        public string Hostname { get => hostname; set => hostname = value; }
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
    }
}
