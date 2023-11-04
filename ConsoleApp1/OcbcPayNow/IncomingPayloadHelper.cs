using System.Security.Cryptography;

namespace ConsoleApp1.OcbcPayNow
{
    public static class IncomingPayloadHelper
    {
        public static bool VerifySignature(byte[] data, byte[] signature, string publicKeyContent)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.ImportFromPem(publicKeyContent);

                // Verify the signature using the public key
                bool isSignatureValid = rsa.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                return isSignatureValid;
            }
        }

        public static void LoadPublicKey(this RSA rsa, string publicKeyContent)
        {
            byte[] publicKeyBytes = Utility.ParsePem(publicKeyContent, "PUBLIC KEY");
            rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);
        }
    }
}
