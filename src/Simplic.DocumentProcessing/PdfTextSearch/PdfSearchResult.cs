using System.Collections;

namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// Represents a pdf search result
    /// </summary>
    public class PdfSearchResult
    {
        /// <summary>
        /// Gets or sets the page number
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the search text which was found on the given page
        /// </summary>
        public string SearchText { get; set; }
    }
}
