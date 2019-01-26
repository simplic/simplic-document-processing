using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing
{
    public class PdfContentExtractionOption
    {
        public float Height { get; set; }
        public float Left { get; set; }

        /// <summary>
        /// Gets or sets a list of pages to analyze. If no pages are inserted, all will be analyzed
        /// </summary>
        public IList<int> Pages { get; set; } = new List<int>();
        public float Top { get; set; }
        public float Width { get; set; }
    }
}
