using System.Collections.Generic;

namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// Pdf split service
    /// </summary>
    public interface IPdfSplitService
    {
        /// <summary>
        /// Split pdf by page range
        /// </summary>
        /// <param name="pdf">Pdf to split</param>
        /// <param name="ranges">List of page ranges</param>
        /// <returns>List of splitted pdfs</returns>
        IList<PdfSplitResult> Split(byte[] pdf, IList<PageNumberRange> ranges);
    }
}
