using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsIdrac.Redfish.Util
{
    [Serializable]
    public class RedfishException : Exception
    {
        public RedfishException(string message) : base (message) { }
        public RedfishException() : base() { }
        public RedfishException(string message, Exception inner) : base(message, inner) { }
    }
}
