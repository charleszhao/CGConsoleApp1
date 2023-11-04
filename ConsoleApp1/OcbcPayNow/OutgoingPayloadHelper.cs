using System.Security.Cryptography;

namespace ConsoleApp1.OcbcPayNow
{
    public static class OutgoingPayloadHelper
    {
        //public static byte[] Encrypt(byte[] payload, string privateKeyContent, string password)
        //{
        //    using (RSA privateKey = Utility.LoadPrivateKey(privateKeyContent, password))
        //    {
        //        return privateKey.Encrypt(payload, RSAEncryptionPadding.OaepSHA256);
        //    }
        //}

        public static byte[] GenerateSignature(byte[] data, string privateKeyContent)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.ImportFromPem(privateKeyContent); // Import the private key from PEM format

                // Generate a digital signature using the private key
                byte[] signature = rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                return signature;
            }
        }
    }
}
