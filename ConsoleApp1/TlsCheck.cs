using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class TlsCheck
    {
        public static bool IsTls12Supported()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                using (WebClient client = new WebClient())
                {
                    string response = client.DownloadString("https://example.com");
                    Console.WriteLine("TLS 1.2 is supported.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TLS 1.2 is not supported: " + ex.Message);
            }
            return false;
        }
    }
}
