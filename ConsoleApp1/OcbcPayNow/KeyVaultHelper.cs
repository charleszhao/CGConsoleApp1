using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.OcbcPayNow
{
    public class KeyVaultHelper
    {
        public static string GetSecretValue(string secretName)
        {
            var crediantel = new ClientSecretCredential("449f1a87-a1b5-41a7-98d3-3052efe60efd",
                        "f26788f4-253f-4d8c-a2f8-6b6664e9fbc4",
                        "L4x8Q~XJl.WB07Lla7jAkrStYR.BlpxrSDqEabZ9");

            var client = new SecretClient(new Uri("https://vlt-newcsp-devmzna-csp.vault.azure.net/"), crediantel);

            KeyVaultSecret secret = client.GetSecret(secretName);
            return secret.Value;
        }
    }
}
