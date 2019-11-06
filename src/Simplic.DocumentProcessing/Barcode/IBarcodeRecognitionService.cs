using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// Barcode recognition service
    /// </summary>
    public interface IBarcodeRecognitionService
    {
        /// <summary>
        /// Execute barcode recognition
        /// </summary>
        /// <param name="blob">Blob to test</param>
        /// <param name="fileExtension">file extension</param>
        /// <param name="options">Settings/options</param>
        /// <returns>List of detected barcodes</returns>
        IList<BarcodeRecognitionResult> Process(byte[] blob, string fileExtension, BarcodeRecognitionOption options);
    }
}
