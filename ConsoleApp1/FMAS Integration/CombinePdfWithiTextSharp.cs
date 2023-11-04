using iText.IO.Source;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using Newtonsoft.Json;

namespace ConsoleApp1.FMAS_Integration
{
    public static class CombinePdfWithiTextSharp
    {
        public static string CombinePdf()
        {
            var taxInvoiceAllPdfStr = File.ReadAllText(@"C:\Users\charzhao\Downloads\FMAS_TaxInvoice_Multi_PDF_Sample.json");
            taxInvoiceAllPdfStr = taxInvoiceAllPdfStr.Trim('"').Replace("\\", "");
            var pdfList = JsonConvert.DeserializeObject<List<PdfResponse>>(taxInvoiceAllPdfStr);

            if (pdfList == null || !pdfList.Any())
            {
                return string.Empty;
            }

            using (var mergedStream = new MemoryStream())
            {
                using (var writer = new PdfWriter(mergedStream))
                {
                    using (var mergedPdf = new PdfDocument(writer))
                    {
                        foreach (var pdf in pdfList)
                        {
                            byte[] subsequentPdfBytes = Convert.FromBase64String(pdf.FileBinary);

                            using (var subsequentPdfStream = new MemoryStream(subsequentPdfBytes))
                            {
                                using (var reader = new PdfReader(subsequentPdfStream))
                                {
                                    using (var subsequentPdf = new PdfDocument(reader))
                                    {
                                        subsequentPdf.CopyPagesTo(1, subsequentPdf.GetNumberOfPages(), mergedPdf);
                                    }
                                }
                            }
                        }
                    }
                }
                byte[] mergedPdfData = mergedStream.ToArray();
                string mergedBase64PdfString = Convert.ToBase64String(mergedPdfData);

                File.WriteAllBytes(@"C:\Users\charzhao\Desktop\combined.pdf", mergedPdfData);
                return mergedBase64PdfString;
            }

            //using (var mergedStream = new ByteArrayOutputStream())
            //{
            //    using (var writer = new PdfWriter(mergedStream))
            //    {
            //        using (var mergedDocument = new PdfDocument(writer))
            //        {
            //            //var merger = new PdfMerger(mergedDocument);
            //            PdfMerger merger = null;

            //            foreach (var pdf in pdfList)
            //            {
            //                byte[] pdfBytes = Convert.FromBase64String(pdf.FileBinary);

            //                using (var copyFromMemoryStream = new MemoryStream(pdfBytes))
            //                {
            //                    using (var reader = new PdfReader(copyFromMemoryStream))
            //                    {
            //                        using (var copyFromDocument = new PdfDocument(reader))
            //                        {
            //                            merger.Merge(copyFromDocument, 1, copyFromDocument.GetNumberOfPages());
            //                        }
            //                    }
            //                }
            //            }

            //            byte[] mergedPdfData = mergedStream.ToArray();
            //            string mergedBase64PdfString = Convert.ToBase64String(mergedPdfData);

            //            File.WriteAllBytes(@"C:\Users\charzhao\Desktop\combined.pdf", mergedPdfData);

            //            return mergedBase64PdfString;
            //        }
            //    }
            //}
        }
    }
}
