using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsIdrac.Racadm.Util
{
    [Serializable]
    public class RacadmException : Exception
    {
        public RacadmException(string message) : base(message) { }
        public RacadmException() : base() { }
        public RacadmException(string message, Exception inner) : base(message, inner) { }
    }
}
