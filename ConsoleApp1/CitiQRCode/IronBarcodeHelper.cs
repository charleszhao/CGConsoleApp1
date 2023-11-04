using IronBarCode;
using IronSoftware.Drawing;
using QRCodeWriter = IronBarCode.QRCodeWriter;

namespace ConsoleApp1.CitiQRCode
{
    public static class IronBarcodeHelper
    {
        private static string _qrData = "00020101021226580009SG.PAYNOW010120213T08GB0025ACT1030100414202309281618205204000053037025406203.005802SG5915JTC CORPORATION6009SINGAPORE62260122RD030320230928293002816304E9D2";
        public static void GenerateQRCodeImage()
        {
            var qrSize = 400;

            var qrCodeLogo = @"C:\Users\charzhao\Desktop\PayNow Logo.png";

            //var qrLogo = new QRCodeLogo(qrCodeLogo);
            //var logoBytes = BlobHelper.GetPayNowLogo();
            //var stream = new MemoryStream(logoBytes);

            var stream = BlobHelper.GetPayNowLogo();
            var qrLogo = new QRCodeLogo(stream, 0);

            var qrCodeWithLogo = QRCodeWriter.CreateQrCodeWithLogo(_qrData, qrLogo, qrSize);
            qrCodeWithLogo.ChangeBarCodeColor(new IronSoftware.Drawing.Color(124, 26, 120));
            qrCodeWithLogo.SetMargins(0);

            Console.WriteLine(qrCodeWithLogo.ToDataUrl());
            qrCodeWithLogo.SaveAsPng(@$"C:\Users\charzhao\Desktop\PayNow_Test_Iron_{qrSize}.png");
        }
    }
}
