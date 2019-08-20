namespace Simplic.DocumentProcessing
{
    public interface ITextAnnotationService
    {
        /// <summary>
        /// Add text annotation to pdf
        /// </summary>
        /// <param name="pdf">Pdf to add annotation to</param>
        /// <param name="textAnnotation">Annotation object</param>
        /// <returns>Pdf containing the annotation</returns>
        byte[] AddAnnotation(byte[] pdf, TextAnnotation textAnnotation);
    }
}
