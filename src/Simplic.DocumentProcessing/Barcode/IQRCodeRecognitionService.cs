using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// Service for reading qr-codes
    /// </summary>
    public interface IQRCodeRecognitionService
    {
        /// <summary>
        /// Execute barcode recognition
        /// </summary>
        /// <param name="blob">Blob to test</param>
        /// <param name="fileExtension">file extension</param>
        /// <returns>List of detected barcodes</returns>
        IList<BarcodeRecognitionResult> Process(byte[] blob, string fileExtension);
    }
}
