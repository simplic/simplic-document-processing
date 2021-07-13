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

        /// <summary>
        /// Adds an empty page.
        /// </summary>
        /// <param name="pdf">Pdf</param>
        /// <param name="pageNumber">Index of the new page, starting with 1 which will insert an empty page as the first page</param>
        /// <returns>The resulting Pdf-blob</returns>
        byte[] AddEmptyPage(byte[] pdf, int pageNumber);

        /// <summary>
        /// Adds a text annotation, background behind the text will always be white opaque
        /// </summary>
        /// <param name="pdf">Pdf</param>
        /// <param name="content">The actual string which will be inserted into the annotaiton</param>
        /// <param name="pageNumber">Page number on which the annotation will be placed</param>
        /// <param name="textColorR">Red value of the text</param>
        /// <param name="textColorG">Gree value of the text</param>
        /// <param name="textColorB">Blue value of the text</param>
        /// <param name="top">Margin from the top of the document in millimeters</param>
        /// <param name="left">margin from the left of the document in millimeters</param>
        /// <returns></returns>
        byte[] AddTextAnnotation(byte[] pdf, string content, int pageNumber, byte textColorR, byte textColorG, byte textColorB, float top, float left);
    }
}
