using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsIdrac.Redfish.Util
{
    public class ConnectionUtil
    {
        public static async Task<bool> CheckConnectionAsync(string host)
        {
            bool connection = false;
            await Task.Run(() =>
            {
                try
                {
                    Ping ping = new Ping();
                    PingReply reply = ping.Send(host);
                    if (reply.Status == IPStatus.Success)
                        connection = true;
                    else
                        connection = false;
                }
                catch (Exception) // In case the IP is in the wrong format
                {
                    connection = false;
                }
            });
            return connection;
        }
    }
}
