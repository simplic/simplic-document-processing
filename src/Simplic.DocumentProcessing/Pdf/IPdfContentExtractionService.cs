using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// Pdf extraction service
    /// </summary>
    public interface IPdfContentExtractionService : IDisposable
    {
        /// <summary>
        /// Load pdf blob.
        /// </summary>
        /// <param name="pdf">Pdf blob</param>
        /// <returns>True if loading was successfull</returns>
        bool LoadPdf(byte[] pdf);

        /// <summary>
        /// Extract complete text. Uses the default options <see cref="DefaultOptions"/>
        /// </summary>
        /// <returns>Extraction result</returns>
        PdfContentExtractionResult ExtractText();

        /// <summary>
        /// Extract text using extraction options
        /// </summary>
        /// <param name="options">Option set</param>
        /// <returns>Extraction result</returns>
        PdfContentExtractionResult ExtractText(PdfContentExtractionOption options);

        /// <summary>
        /// Gets or sets the default options
        /// </summary>
        PdfContentExtractionOption DefaultOptions { get; set; }

        /// <summary>
        /// Gets the complete page count
        /// </summary>
        int PageCount { get; set; }
    }
}
