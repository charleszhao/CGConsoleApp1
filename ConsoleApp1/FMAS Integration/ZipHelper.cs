using Newtonsoft.Json;
using System.IO.Compression;

namespace ConsoleApp1.FMAS_Integration
{
    public static class ZipHelper
    {
        public static string ZipPdf()
        {
            var taxInvoiceAllPdfStr = File.ReadAllText(@"C:\Users\charzhao\Downloads\FMAS_TaxInvoice_Multi_PDF_Sample.json");
            taxInvoiceAllPdfStr = taxInvoiceAllPdfStr.Trim('"').Replace("\\", "");
            var pdfList = JsonConvert.DeserializeObject<List<PdfResponse>>(taxInvoiceAllPdfStr);

            using (MemoryStream zipStream = new MemoryStream())
            {
                using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                {
                    foreach (var fileData in pdfList)
                    {
                        byte[] fileBytes = Convert.FromBase64String(fileData.FileBinary);

                        // Create a zip entry with the file name (you can customize this)
                        var entry = archive.CreateEntry($"{Guid.NewGuid().ToString()}.pdf", CompressionLevel.Fastest);

                        // Write the Base64-decoded data to the zip entry
                        using (var entryStream = entry.Open())
                        {
                            entryStream.Write(fileBytes, 0, fileBytes.Length);
                        }
                    }
                }

                // Get the zipped file as a byte array
                byte[] zipFileBytes = zipStream.ToArray();
                string zipBase64PdfString = Convert.ToBase64String(zipFileBytes);
                Console.WriteLine(zipBase64PdfString);
                File.WriteAllBytes(@"C:\Users\charzhao\Desktop\combined.zip", zipFileBytes);
                // Return the zipped file as a response
                return zipBase64PdfString;
            }
        }
    }
}
