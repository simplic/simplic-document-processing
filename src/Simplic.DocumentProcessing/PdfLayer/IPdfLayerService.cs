namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// Pdf layer service
    /// </summary>
    public interface IPdfLayerService
    {
        /// <summary>
        /// Add a layer to a pdf and return it
        /// </summary>
        /// <param name="pdf">Pdf to add the layer to</param>
        /// <param name="layer">Layer file as byte arrya</param>
        /// <param name="mode">Add mode (first page, each page, ...)</param>
        /// <returns>Pdf with layer</returns>
        byte[] MergeLayer(byte[] pdf, byte[] layer, PdfLayerMode mode);
    }
}
