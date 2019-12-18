using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// Merge pdf service
    /// </summary>
    public interface IPdfMergeService
    {
        /// <summary>
        /// Merge pdfs and return a single pdf file
        /// </summary>
        /// <param name="pdfs">List of pdfs</param>
        /// <param name="options">Pdf merge options</param>
        /// <returns>Merged pdf</returns>
        byte[] Merge(IList<byte[]> pdfs, MergeOption options = null);
    }
}
