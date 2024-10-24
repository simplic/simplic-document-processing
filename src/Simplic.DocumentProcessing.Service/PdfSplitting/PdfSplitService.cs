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
        private readonly IPdfService pdfService;

        public PdfSplitService(IPdfService pdfService)
        {
            this.pdfService = pdfService;
        }

        /// <summary>
        /// Generate page ranges from a given list of page numbers
        /// </summary>
        /// <param name="pdf">Pdf as blob</param>
        /// <param name="pageNumbers">List of page numbers to create ranges for</param>
        /// <returns>List of page ranges</returns>
        public IList<PageNumberRange> GetPageRanges(byte[] pdf, IList<BarcodeRecognitionResult> pages)
        {
            var ranges = new List<PageNumberRange>();
            var pageCount = pdfService.GetPageCount(pdf);

            foreach (var page in pages.OrderBy(x => x.Page))
            {
                var number = page.Page;

                var nextPageCount = pages.Where(x => x.Page > number).OrderBy(x => x.Page).Select(x => x.Page).FirstOrDefault();
                if (nextPageCount == 0)
                    ranges.Add(new PageNumberRange
                    {
                        StartPageNumber = number,
                        PageCount = pageCount - (number - 1),
                        Barcode = page.Barcode,
                        BarcodeType = page.BarcodeType
                    });
                else
                    ranges.Add(new PageNumberRange
                    {
                        StartPageNumber = number,
                        PageCount = nextPageCount - number,
                        Barcode = page.Barcode,
                        BarcodeType = page.BarcodeType
                    });
            }

            return ranges;
        }

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
                        return new List<PdfSplitResult> { };

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
                                result.Add(new PdfSplitResult 
                                {
                                    Pdf = targetStream.ToArray(),
                                    PageCount = range.PageCount,
                                    Barcode = range.Barcode,
                                    BarcodeType = range.BarcodeType
                                });
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
