using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{
    public static class RsaHelper
    {
        public static string Sign(string dataToSign, string privateKey)
        {
            byte[] dataByte = Encoding.UTF8.GetBytes(dataToSign);

            using RSA rsa = RSA.Create();
            rsa.ImportFromPem(privateKey);

            // Generate a digital signature using JTC private key
            byte[] signatureByte = rsa.SignData(dataByte, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            return Convert.ToBase64String(signatureByte);
        }

        public static bool Verify(string dataToVerify, string signature, string publicKey)
        {
            byte[] dataByte = Encoding.UTF8.GetBytes(dataToVerify);
            byte[] signatureByte = Convert.FromBase64String(signature);

            using RSA rsa = RSA.Create();
            rsa.ImportFromPem(publicKey);

            // Verify the signature using OCBC public key
            return rsa.VerifyData(dataByte, signatureByte, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
    }
}
