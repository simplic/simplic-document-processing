using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// ADR configuration model
    /// </summary>
    public class ADRConfiguration
    {
        /// <summary>
        /// Gets or sets the tag which represents the document
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the document blob (tiff)
        /// </summary>
        public byte[] Blob { get; set; }
    }
}
