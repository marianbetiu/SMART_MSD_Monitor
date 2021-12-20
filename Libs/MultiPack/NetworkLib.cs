using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace MultiPack
{
    public static class NetworkLib
    {
        public static string localIPMask = "10.";

        public static IPAddress GetLocalIPAddress()
        {
            IPAddress result = null;

            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                var ipAddresses = host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToList();
                if (ipAddresses.Count > 0)
                {
                    result = ipAddresses.FirstOrDefault(o => (o.ToString().StartsWith(localIPMask)));
                }
            }

            return result;
        }

        public static IPAddress ConvertIPAddressFromString(string ipAddress)
        {
            IPAddress result = new IPAddress(new byte[] { 127, 0, 0, 1 });

            if (!string.IsNullOrEmpty(ipAddress))
            {
                IPAddress.TryParse(ipAddress, out result);
            }

            return result;
        }
    }
}
