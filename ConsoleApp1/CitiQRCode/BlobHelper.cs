using Azure.Storage.Blobs;
using Azure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage.Blobs.Models;

namespace ConsoleApp1.CitiQRCode
{
    public static class BlobHelper
    {
        public static MemoryStream GetPayNowLogo()
        {
            string blobServiceEndpoint = "https://sstnewcspdevintimg.blob.core.windows.net";
            var containerName = "img-store";
            var blobContainerClient = new BlobContainerClient(new Uri($"{blobServiceEndpoint}/{containerName}"), new ManagedIdentityCredential());

            var blobClient = blobContainerClient.GetBlobClient("PayNow Logo.png");

            //var blobDownloadInfo = blobClient.Download();
            //byte[] imageBytes = new byte[blobDownloadInfo.Value.ContentLength];
            //blobDownloadInfo.Value.Content.Read(imageBytes, 0, (int)blobDownloadInfo.Value.ContentLength);

            //return imageBytes;

            var blobDownloadInfo = blobClient.OpenRead();
            var stream = new MemoryStream();
            blobDownloadInfo.CopyTo(stream);
            stream.Seek(0, SeekOrigin.Begin); // Reset the stream position

            return stream;
        }
    }
}
