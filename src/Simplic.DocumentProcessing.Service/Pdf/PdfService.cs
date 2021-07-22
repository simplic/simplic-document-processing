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
        /// Adds a text annotation
        /// </summary>
        /// <param name="pdf">Pdf</param>
        /// <param name="pageNumber">Page number on which the annotation will be placed</param>
        /// <param name="annotation">The annotation which will be added</param>
        /// <returns>The modified Pdf-blob</returns>
        public byte[] AddTextAnnotation(byte[] pdf, int pageNumber, PdfAnnotation annotation)
        {
            using (var stream = new MemoryStream(pdf))
            {
                using (var pdfInstance = GdPictureHelper.GetPDFInstance())
                {
                    pdfInstance.LoadFromStream(stream);
                    pdfInstance.SelectPage(pageNumber);
                    pdfInstance.SetOrigin(PdfOrigin.PdfOriginTopLeft);
                    pdfInstance.SetMeasurementUnit(PdfMeasurementUnit.PdfMeasurementUnitMillimeter);
                    pdfInstance.AddFreeTextAnnotation(annotation.Left, annotation.Top + annotation.Height, annotation.Width, annotation.Height, annotation.HasBorder, "", "", annotation.Content, annotation.FontName, annotation.FontSize, annotation.FontColor.R, annotation.FontColor.G, annotation.FontColor.B, annotation.BackgroundColor.R, annotation.BackgroundColor.G, annotation.BackgroundColor.B, annotation.Opacity);

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
        /// Rotates a Pdf
        /// </summary>
        /// <param name="pdf">The pdf</param>
        /// <param name="pageNumber">Index of the page to rotate</param>
        /// <param name="degree">Clockwise rotation</param>
        /// <returns>The rotated Pdf</returns>
        public byte[] RotatePdf(byte[] pdf, int pageNumber, double degree)
        {
            using (var stream = new MemoryStream(pdf))
            {
                using (var pdfInstance = GdPictureHelper.GetPDFInstance())
                {
                    pdfInstance.LoadFromStream(stream);
                    pdfInstance.SelectPage(pageNumber);
                    pdfInstance.RotatePageEx((float)-degree);

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
        /// Estimates a pages text rotation
        /// </summary>
        /// <param name="pdf">The pdf</param>
        /// <param name="pageNumber">Index page to use for estimating the rotation</param>
        /// <returns>The estimated text rotation</returns>
        public double GetPdfPageTextRotation(byte[] pdf, int pageNumber)
        {
            using (var stream = new MemoryStream(pdf))
            {
                using (var pdfInstance = GdPictureHelper.GetPDFInstance())
                {
                    pdfInstance.LoadFromStream(stream);
                    pdfInstance.SelectPage(pageNumber);
                    return pdfInstance.GuessPageTextRotation();
                }
            }
        }
    }
}
