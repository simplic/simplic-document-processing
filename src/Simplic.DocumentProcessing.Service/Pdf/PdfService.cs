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
                    return pdfInstance.GetPageCount();
                }
            }
        }
    }
}
