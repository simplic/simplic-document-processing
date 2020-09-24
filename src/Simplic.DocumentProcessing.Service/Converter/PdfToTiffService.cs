using GdPicture14;
using System.IO;

namespace Simplic.DocumentProcessing.Service
{
    /// <summary>
    /// Provides methods to convert pdf files to tiff
    /// </summary>
    public class PdfToTiffService : IPdfToTiffService
    {
        /// <summary>
        /// Convert pdf to tiff
        /// </summary>
        /// <param name="data">Pdf as byte array (blob)</param>
        /// <returns>Tiff as byte array (blob)</returns>
        public byte[] Convert(byte[] data)
        {
            //We assume that GdPicture has been correctly installed and unlocked.
            using (var converter = GdPictureHelper.GetGdPictureDocumentConverterInstance())
            {
                using (var inStream = new MemoryStream(data))
                {
                    GdPictureStatus status = converter.LoadFromStream(inStream, GdPicture14.DocumentFormat.DocumentFormatPDF);
                    if (status == GdPictureStatus.OK)
                    {
                        using (var stream = new MemoryStream())
                        {
                            status = converter.SaveAsTIFF(stream, TiffCompression.TiffCompressionAUTO);
                            return stream.ToArray();
                        }
                    }
                }
            }

            return null;
        }
    }
}
