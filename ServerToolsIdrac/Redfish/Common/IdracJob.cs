using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsIdrac.Redfish.Common
{
    public class IdracJob
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string JobState { get; set; }
        public string Message { get; set; }
        public int PercentComplete { get; set; }
    }
}
