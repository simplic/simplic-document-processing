using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// ADR service
    /// </summary>
    public interface IADRService : IDisposable
    {
        /// <summary>
        /// Create adr Configuration
        /// </summary>
        /// <param name="configuration">List of configuration objects</param>
        /// <returns>Configuration as byte-array</returns>
        byte[] CreateConfiguration(IList<ADRConfiguration> configuration);

        /// <summary>
        /// Match the blob
        /// </summary>
        /// <param name="configuration">ADR configuration</param>
        /// <param name="blob">Blob to match</param>
        /// <returns>Best match if exists, else null</returns>
        ADRResult Match(byte[] configuration, byte[] blob);
    }
}
