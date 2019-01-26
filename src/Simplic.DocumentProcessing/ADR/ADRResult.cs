using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// ADR result model
    /// </summary>
    public class ADRResult
    {
        /// <summary>
        /// Gets or sets the matching tag
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the matching accuracy
        /// </summary>
        public double Accuracy { get; set; }
    }
}
