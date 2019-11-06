namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// Barcode recognition result
    /// </summary>
    public class BarcodeRecognitionResult
    {
        /// <summary>
        /// Gets or sets the barcode
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        /// Gets or sets the barcode type
        /// </summary>
        public string BarcodeType { get; set; }

        /// <summary>
        /// Gets or sets the page
        /// </summary>
        public int Page { get; set; }
    }
}
