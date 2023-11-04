using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class NewKeyVaultHelper
    {
        public static string GetKeyValultValue()
        {
            // Create a SecretClient using the managed identity
            var vaultUri = new Uri("https://vlt-newcsp-devmzna-csp.vault.azure.net/");
            //var secretClient = new SecretClient(vaultUri, new DefaultAzureCredential(new DefaultAzureCredentialOptions
            //{
            //    ManagedIdentityClientId = "8183dea9-62ec-4b43-bb4d-f84a0d2c63a9",
            //    TenantId = "449f1a87-a1b5-41a7-98d3-3052efe60efd"
            //}));

            var secretClient = new SecretClient(vaultUri, new DefaultAzureCredential());

            // Name of the secret you want to retrieve
            string secretName = "scr-cxp-dev-citisubkeygccint";
            string secretValue = string.Empty;
            try
            {
                KeyVaultSecret secret = secretClient.GetSecret(secretName);
                secretValue = secret.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return secretValue;
        }
    }
}
