namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// Pdf split result
    /// </summary>
    public class PdfSplitResult
    {
        /// <summary>
        /// Gets or sets the new pdf page as byte array
        /// </summary>
        public byte[] Pdf { get; set; }

        /// <summary>
        /// Gets or sets the page count of the new pdf
        /// </summary>
        public int PageCount { get; set; }
    }
}
