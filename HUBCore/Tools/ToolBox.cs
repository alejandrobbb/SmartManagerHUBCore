using System.Net;
using System.Text;
using System.Security.Cryptography;

namespace HUBCore.Tools
{
    public class ToolBox
    {
        public string EncriptarPassword(string password)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(password));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public string IpCatch()
        {
            string hostName = Dns.GetHostName();
            string ip = "";

            IPAddress[] addresses = Dns.GetHostAddresses(hostName);

            foreach (IPAddress address in addresses)
            {
                ip = address.ToString();
            }
            return ip;
        }
    }
}
