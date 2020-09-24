using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// Provides methods to convert pdf files to tiff
    /// </summary>
    public interface IPdfToTiffService
    {
        /// <summary>
        /// Convert pdf to tiff
        /// </summary>
        /// <param name="data">Pdf as byte array (blob)</param>
        /// <returns>Tiff as byte array (blob)</returns>
        byte[] Convert(byte[] data);
    }
}
