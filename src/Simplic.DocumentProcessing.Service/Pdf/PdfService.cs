using GdPicture14;
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
        /// Adds an empty page to the given pdf.
        /// </summary>
        /// <param name="pdf">Pdf</param>
        /// <param name="pageNumber">Index of the new page, starting with 1 which will insert an empty page as the first page</param>
        /// <returns>The resulting Pdf-blob</returns>
        public byte[] AddEmptyPage(byte[] pdf, int pageNumber)
        {
            using (var stream = new MemoryStream(pdf))
            {
                using (var pdfInstance = GdPictureHelper.GetPDFInstance())
                {
                    pdfInstance.LoadFromStream(stream);
                    pdfInstance.SelectPage(1);
                    float pageWidth = pdfInstance.GetPageWidth();
                    float pageHeight = pdfInstance.GetPageHeight();

                    pdfInstance.InsertPage(pageWidth, pageHeight, pageNumber);

                    using (var targetStream = new MemoryStream())
                    {
                        pdfInstance.SaveToStream(targetStream);
                        targetStream.Position = 0;

                        pdfInstance?.CloseDocument();
                        return targetStream.ToArray();
                    }
                }
            }
        }

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
        public byte[] AddTextAnnotation(byte[] pdf, string content, int pageNumber, byte textColorR, byte textColorG, byte textColorB, float top, float left)
        {
            int annotationWidth = 50;
            int annotationheight = 50;
            using (var stream = new MemoryStream(pdf))
            {
                using (var pdfInstance = GdPictureHelper.GetPDFInstance())
                {
                    pdfInstance.LoadFromStream(stream);
                    pdfInstance.SelectPage(pageNumber);
                    pdfInstance.SetOrigin(PdfOrigin.PdfOriginTopLeft);
                    pdfInstance.SetMeasurementUnit(PdfMeasurementUnit.PdfMeasurementUnitMillimeter);
                    pdfInstance.AddFreeTextAnnotation(left, top + annotationheight, annotationWidth, annotationheight, false, "", "", content, "Arial", 12, textColorR, textColorB, textColorG, 255, 255, 255, 1.0f);

                    using (var targetStream = new MemoryStream())
                    {
                        pdfInstance.SaveToStream(targetStream);
                        targetStream.Position = 0;

                        pdfInstance?.CloseDocument();
                        return targetStream.ToArray();
                    }
                }
            }
        }
    }
}
