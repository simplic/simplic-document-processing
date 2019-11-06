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
    }
}
