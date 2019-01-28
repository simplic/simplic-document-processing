using System.Collections.Generic;

namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// Text search service
    /// </summary>
    public interface IPdfTextSearchService
    {

        /// <summary>
        /// Search text within a pdf
        /// </summary>
        /// <param name="pdf">Pdf as byte array</param>
        /// <param name="searchTexts">Search texts</param>
        /// <param name="caseSensitive">Gets or sets whether the search process is case sensitive</param>
        /// <returns>Search result list</returns>
        IList<PdfSearchResult> Search(byte[] pdf, IList<string> searchTexts, bool caseSensitive = true);
    }
}
