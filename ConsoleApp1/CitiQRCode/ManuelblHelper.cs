using Net.Codecrete.QrCodeGenerator;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
//using System.Drawing.Imaging;
using System.Text;

namespace ConsoleApp1.CitiQRCode
{
    public class ManuelblHelper
    {
        private static string _qrData = "00020101021226580009SG.PAYNOW010120213T08GB0025ACT10301004142023102715540852040000530370254041.005802SG5915JTC CORPORATION6009SINGAPORE62260122RD03032023102727848601630414EF";

        public static void GenerateQRCodeImage()
        {
            var qrImageSize = 300;
            qrImageSize = qrImageSize <= 0 ? 200 : qrImageSize;

            var qr = QrCode.EncodeText(_qrData, QrCode.Ecc.Medium);

            var scale = (int)Math.Ceiling((double)qrImageSize / qr.Size);

            Console.WriteLine(qr.Version);
            var paynowLogo = @"C:\Users\charzhao\Desktop\PayNow Logo.jpg";
            var foreground = new Color(new Rgb24(124, 26, 120));
            var background = new Color(new Rgb24(255, 255, 255));

            using (var bitmap = qr.ToBitmap(scale: 5, border: 0, foreground, background))
            using (var logo = Image.Load(paynowLogo))
            {
                // resize logo
                var w = (int)Math.Round(bitmap.Width * 0.3f);
                logo.Mutate(logo => logo.Resize(w, 0));

                // draw logo in center
                var topLeft = new Point((bitmap.Width - logo.Width) / 2, (bitmap.Height - logo.Height) / 2);
                bitmap.Mutate(img => img.DrawImage(logo, topLeft, 1));

                var format = new JpegEncoder();
                var configuration = new Configuration(new PngConfigurationModule());
                var x = bitmap.ToBase64String(configuration.ImageFormats.ElementAt(0));
                Console.WriteLine(x);

                bitmap.SaveAsPng(@"C:\Users\charzhao\Desktop\PayNow_Test_Manuel.png");
            }

            //string svg = qr.ToSvgString(4, "#7C1A78", "#ffffff");
            //File.WriteAllText(@"C:\Users\charzhao\Desktop\PayNow_Test_Manuel.svg", svg, Encoding.UTF8);
        }
    }
}
