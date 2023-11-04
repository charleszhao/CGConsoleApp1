using ZXing.QrCode;

namespace ConsoleApp1.CitiQRCode
{
    public static class QRCodeHelper
    {
        public static void GenerateQRCodeImage()
        {
            //var barcodeWriter = new ZXing.BarcodeWriterPixelData
            //{
            //    Format = ZXing.BarcodeFormat.QR_CODE,
            //    Options = new ZXing.Common.EncodingOptions
            //    {
            //        Width = 300,
            //        Height = 300
            //    }
            //};

            //var qrImage = barcodeWriter.Write("00020101021226580009SG.PAYNOW010120213T08GB0025ACT1030100414202309281618205204000053037025406203.005802SG5915JTC CORPORATION6009SINGAPORE62260122RD030320230928293002816304E9D2");
            //var str = Convert.ToBase64String(qrImage.Pixels);
            Byte[] byteArray;
            var width = 250; // width of the QR Code
            var height = 250; // height of the QR Code
            var margin = 0;
            var qrCodeWriter = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width,
                    Margin = margin
                }
            };
            var pixelData = qrCodeWriter.Write("00020101021226580009SG.PAYNOW010120213T08GB0025ACT1030100414202309281618205204000053037025406203.005802SG5915JTC CORPORATION6009SINGAPORE62260122RD030320230928293002816304E9D2");

            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height,
                System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                using (var ms = new MemoryStream())
                {
                    var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), 
                        System.Drawing.Imaging.ImageLockMode.WriteOnly, 
                        System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    try
                    {
                        // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }
                    // save to stream as PNG
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byteArray = ms.ToArray();

                    // save to folder
                    string fileGuid = Guid.NewGuid().ToString().Substring(0, 4);
                    bitmap.Save(@$"C:\Users\charzhao\Desktop\{fileGuid}.png", System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }
    }
}
