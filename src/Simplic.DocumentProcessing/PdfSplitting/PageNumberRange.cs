namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// Represents a page number range
    /// </summary>
    public class PageNumberRange
    {
        /// <summary>
        /// Gets or sets the start page
        /// </summary>
        public int StartPageNumber { get; set; }

        /// <summary>
        /// Gets or sets the page count
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Gets or sets the optional barcode
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        /// Gets or sets the optional barcode type
        /// </summary>
        public string BarcodeType { get; set; }
    }
}