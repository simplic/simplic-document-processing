namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// General pdf service
    /// </summary>
    public interface IPdfService
    {
        /// <summary>
        /// Get page count
        /// </summary>
        /// <param name="pdf">Pdf</param>
        /// <returns>Page count</returns>
        int GetPageCount(byte[] pdf);
    }
}
