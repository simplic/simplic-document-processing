using System.Collections.Generic;

namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// Pdf split service
    /// </summary>
    public interface IPdfSplitService
    {
        /// <summary>
        /// Generate page ranges from a given list of page numbers
        /// </summary>
        /// <param name="pdf">Pdf as blob</param>
        /// <param name="pages">List of page numbers to create ranges for</param>
        /// <returns>List of page ranges</returns>
        IList<PageNumberRange> GetPageRanges(byte[] pdf, IList<BarcodeRecognitionResult> pages);

        /// <summary>
        /// Split pdf by page range
        /// </summary>
        /// <param name="pdf">Pdf to split</param>
        /// <param name="ranges">List of page ranges</param>
        /// <returns>List of splitted pdfs</returns>
        IList<PdfSplitResult> Split(byte[] pdf, IList<PageNumberRange> ranges);

        /// <summary>
        /// Split pdf by page number
        /// </summary>
        /// <param name="pdf">Pdf to split</param>
        /// <param name="ranges">List of page number</param>
        /// <returns>Splitted pdfs</returns>
        PdfSplitResult SplitByPageNumbers(byte[] pdf, IList<int> pageNumbers);
    }
}
