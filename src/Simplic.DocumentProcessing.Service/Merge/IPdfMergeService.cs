using GdPicture14;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing.Service
{
    /// <summary>
    /// Merge pdf service
    /// </summary>
    public class PdfMergeService : IPdfMergeService
    {
        /// <summary>
        /// Merge pdfs and return a single pdf file
        /// </summary>
        /// <param name="pdfs">List of pdfs</param>
        /// <param name="options">Pdf merge options</param>
        /// <returns>Merged pdf</returns>
        public byte[] Merge(IList<byte[]> pdfs, MergeOption options = null)
        {
            if (!pdfs.Any())
                throw new InvalidOperationException("Could not merge 0 pdfs.");

            if (pdfs.Count == 1)
                return pdfs.First();

            if (options == null)
                options = new MergeOption();

            var pdfObjects = new List<GdPicturePDF>();

            foreach(var pdf in pdfs)
            {
                var sourcePdf = GdPictureHelper.GetPDFInstance();
                sourcePdf.LoadFromStream(new MemoryStream(pdf));
            }

            using (var targetPdf = GdPictureHelper.GetPDFInstance())
            {
                var pdf = targetPdf.MergeDocuments(pdfObjects.ToArray());

                using (var stream = new MemoryStream())
                {
                    pdf.SaveToStream(stream);

                    // Dispose existing PDFs
                    foreach (var _pdf in pdfObjects)
                        _pdf.Dispose();

                    return stream.ToArray();
                }                
            }
        }
    }
}
