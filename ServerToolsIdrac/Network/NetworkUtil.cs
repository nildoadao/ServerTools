using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsIdrac.Network
{
    public static class NetworkUtil
    {
        private static int[] ParseIPAddress(string Ip)
        {
            string[] stringOctects = Ip.Split('.');

            if (stringOctects.Length != 4)
                throw new ArgumentException("Invalid IP Address");

            int[] octects = new int[4];

            for (int i = 0; i < 4; i++)
            {
                octects[i] = int.Parse(stringOctects[i]);

                if(octects[i] > 255 | octects[i] < 0)
                    throw new ArgumentException("Invalid IP Address");
            }
            return octects;               
        }

        public static string GetNetworkAddress(string ipAddress, string netMask)
        {
            var ipOctects = ParseIPAddress(ipAddress);
            var netMaskOctects = ParseIPAddress(netMask); 

            int[] network = new int[4];

            network[0] = ipOctects[0] & netMaskOctects[0];
            network[1] = ipOctects[1] & netMaskOctects[1];
            network[2] = ipOctects[2] & netMaskOctects[2];
            network[3] = ipOctects[3] & netMaskOctects[3];

            return string.Format("{0}.{1}.{2}.{3}", network[0], network[1], network[2], network[3]);
        }

        private static int[] GetBroadCastAddress(string ipAddress, string netMask)
        {
            var ipOctects = ParseIPAddress(ipAddress);
            var netMaskOctects = ParseIPAddress(netMask);

            int[] broadcast = new int[4];

            broadcast[0] = ipOctects[0] | (255 - netMaskOctects[0]);
            broadcast[1] = ipOctects[1] | (255 - netMaskOctects[1]);
            broadcast[2] = ipOctects[2] | (255 - netMaskOctects[2]);
            broadcast[3] = ipOctects[3] | (255 - netMaskOctects[3]);

            return broadcast;
        }

        public static string GetNextIpAddress(string ipAddress, string netMask)
        {
            var ipOctects = ParseIPAddress(ipAddress);
            int[] broadcast = GetBroadCastAddress(ipAddress, netMask);

            if (IsBroadCast(ipAddress, netMask))
                return null;

            if(ipOctects[3] < broadcast[3] & ipOctects[3] < 255)
                ipOctects[3]++;

            else if(ipOctects[2] < broadcast[2] & ipOctects[2] < 255)
            {
                ipOctects[3] = 0;
                ipOctects[2]++;
            }
            else if (ipOctects[1] < broadcast[1] & ipOctects[1] < 255)
            {
                ipOctects[3] = 0;
                ipOctects[2] = 0;
                ipOctects[1]++;
            }
            else if (ipOctects[0] < broadcast[0] & ipOctects[0] < 255)
            {
                ipOctects[3] = 0;
                ipOctects[2] = 0;
                ipOctects[1] = 0;
                ipOctects[0]++;
            }

            return string.Format("{0}.{1}.{2}.{3}", ipOctects[0], ipOctects[1], ipOctects[2], ipOctects[3]);
        }

        private static bool IsBroadCast(string ipAddress, string netMask)
        {
            var ipOctects = ParseIPAddress(ipAddress);
            int[] broadcast = GetBroadCastAddress(ipAddress, netMask);

            if (ipOctects[0] == broadcast[0] &
                ipOctects[1] == broadcast[1] &
                ipOctects[2] == broadcast[2] &
                ipOctects[3] == broadcast[3])
                return true;
            else
                return false;
        }
    }
}
