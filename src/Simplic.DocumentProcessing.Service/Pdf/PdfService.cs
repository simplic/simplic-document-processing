using Simplic.DocumentProcessing.Service;
using System.IO;

namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// General pdf service
    /// </summary>
    public class PdfService : IPdfService
    {
        /// <summary>
        /// Get page count
        /// </summary>
        /// <param name="pdf">Pdf</param>
        /// <returns>Page count</returns>
        public int GetPageCount(byte[] pdf)
        {
            using (var stream = new MemoryStream(pdf))
            {
                using (var pdfInstance = GdPictureHelper.GetPDFInstance())
                {
                    pdfInstance.LoadFromStream(stream);
                    var count = pdfInstance.GetPageCount();

                    pdfInstance?.CloseDocument();

                    return count;
                }
            }
        }

        /// <summary>
        /// Adds an empty page.
        /// </summary>
        /// <param name="pdf">Pdf</param>
        /// <param name="pageNumber">Index of the new page, starting with 1 which will insert an empty page as the first page</param>
        /// <returns>The resulting Pdf-blob</returns>
        public byte[] AddEmptyPage(byte[] pdf, int pageNumber)
        {
            var pdfToTiffService = CommonServiceLocator.ServiceLocator.Current.GetInstance<PdfToTiffService>();
            using (var stream = new MemoryStream(pdf))
            {
                using (var pdfInstance = GdPictureHelper.GetPDFInstance())
                {
                    pdfInstance.LoadFromStream(stream);
                    pdfInstance.SelectPage(1);
                    float pageWidth = pdfInstance.GetPageWidth();
                    float pageHeight = pdfInstance.GetPageHeight();

                    pdfInstance.InsertPage(pageWidth, pageHeight, pageNumber);
                    byte[] blob = pdfToTiffService.Convert(pdf);
                    pdfInstance?.CloseDocument();
                    return blob;
                }
            }
        }
    }
}
