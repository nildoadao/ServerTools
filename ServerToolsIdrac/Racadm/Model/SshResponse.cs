using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsIdrac.Racadm.Model
{
    public class SshResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public SshResponse(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public SshResponse() { }
    }
}
