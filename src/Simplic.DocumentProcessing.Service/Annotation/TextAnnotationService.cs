using GdPicture14;
using System.IO;

namespace Simplic.DocumentProcessing.Service
{
    /// <summary>
    /// Implementation of the text annotation service
    /// </summary>
    public class TextAnnotationService : ITextAnnotationService
    {
        /// <summary>
        /// Add text annotation to pdf
        /// </summary>
        /// <param name="pdf">Pdf to add annotation to</param>
        /// <param name="textAnnotation">Annotation object</param>
        /// <returns>Pdf containing the annotation</returns>
        public byte[] AddAnnotation(byte[] pdf, TextAnnotation textAnnotation)
        {
            using (var pdfInstance = GdPictureHelper.GetPDFInstance())
            {
                pdfInstance.LoadFromStream(new MemoryStream(pdf));

                if (textAnnotation.Page == 0)
                    textAnnotation.Page++;

                pdfInstance.SelectPage(textAnnotation.Page);

                using (var annotationManager = new AnnotationManager())
                {
                    annotationManager.InitFromGdPicturePDF(pdfInstance);

                    var annotation = annotationManager.AddTextAnnot(textAnnotation.Left, textAnnotation.Top, textAnnotation.Width, textAnnotation.Height, textAnnotation.Text);
                    annotation.FontSize = textAnnotation.FontSize;
                    annotation.FontName = textAnnotation.FontName;
                    annotation.ForeColor = textAnnotation.ForeColor;
                    
                    annotationManager.BurnAnnotationsToPage(false);

                    using (var memoryStream = new MemoryStream())
                    {
                        annotationManager.SaveDocumentToPDF(memoryStream);
                        memoryStream.Position = 0;
                        return memoryStream.ToArray();
                    }
                }
            }
        }
    }
}
