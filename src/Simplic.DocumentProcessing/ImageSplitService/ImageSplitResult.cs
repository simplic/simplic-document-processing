namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// Pdf split result
    /// </summary>
    public class ImageSplitResult
    {
        /// <summary>
        /// Gets or sets the new image as byte array
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Gets or sets the page count of the new pdf
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
