using Simplic.DocumentProcessing.Service;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// Pdf split service
    /// </summary>
    public class PdfSplitService : IPdfSplitService
    {
        /// <summary>
        /// Split pdf by page range
        /// </summary>
        /// <param name="pdf">Pdf to split</param>
        /// <param name="ranges">List of page ranges</param>
        /// <returns>List of splitted pdfs</returns>
        public IList<PdfSplitResult> Split(byte[] pdf, IList<PageNumberRange> ranges)
        {
            var result = new List<PdfSplitResult>();

            using (var stream = new MemoryStream(pdf))
            {
                using (var pdfInstance = GdPictureHelper.GetPDFInstance())
                {
                    pdfInstance.LoadFromStream(stream);

                    if (ranges == null || ranges.Count == 0)
                        return new List<PdfSplitResult> { new PdfSplitResult { Pdf = pdf, PageCount = pdfInstance.GetPageCount() } };

                    foreach (var range in ranges.Where(x => x.PageCount > 0 && x.StartPageNumber + (x.PageCount - 1) <= pdfInstance.GetPageCount()))
                    {
                        using (var newPdf = GdPictureHelper.GetPDFInstance())
                        {
                            newPdf.NewPDF();
                            for (int i = 0; i < range.PageCount; i++)
                                newPdf.ClonePage(pdfInstance, range.StartPageNumber + i);

                            using (var targetStream = new MemoryStream())
                            {
                                newPdf.SaveToStream(targetStream);
                                targetStream.Position = 0;
                                result.Add(new PdfSplitResult { Pdf = targetStream.ToArray(), PageCount = range.PageCount });
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Split pdf by page number
        /// </summary>
        /// <param name="pdf">Pdf to split</param>
        /// <param name="ranges">List of page number</param>
        /// <returns>Splitted pdfs</returns>
        public PdfSplitResult SplitByPageNumbers(byte[] pdf, IList<int> pageNumbers)
        {
            var result = new PdfSplitResult();

            using (var stream = new MemoryStream(pdf))
            {
                using (var pdfInstance = GdPictureHelper.GetPDFInstance())
                {
                    pdfInstance.LoadFromStream(stream);

                    if (pageNumbers == null || pageNumbers.Count == 0)
                        return new PdfSplitResult { Pdf = pdf, PageCount = pdfInstance.GetPageCount() };

                    using (var newPdf = GdPictureHelper.GetPDFInstance())
                    {
                        newPdf.NewPDF();
                        foreach (var page in pageNumbers)
                            newPdf.ClonePage(pdfInstance, page);

                        using (var targetStream = new MemoryStream())
                        {
                            newPdf.SaveToStream(targetStream);
                            targetStream.Position = 0;
                            result = new PdfSplitResult { Pdf = targetStream.ToArray(), PageCount = pageNumbers.Count };
                        }
                    }
                }
            }
            return result;
        }
    }
}
