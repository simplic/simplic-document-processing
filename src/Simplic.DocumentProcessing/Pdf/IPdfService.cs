﻿namespace Simplic.DocumentProcessing
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
        /// Adds a text annotation
        /// </summary>
        /// <param name="pdf">Pdf</param>
        /// <param name="pageNumber">Page number on which the annotation will be placed</param>
        /// <param name="annotation">The annotation which will be added</param>
        /// <returns>The modified Pdf-blob</returns>
        byte[] AddTextAnnotation(byte[] pdf, int pageNumber, PdfAnnotation annotation);
    }
}
