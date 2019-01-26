using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing
{
    public class PdfContentExtractionResult
    {
        public string Text { get; set; }
        public IList<PdfContentExtractionRegionResult> RegionResults { get; set; } = new List<PdfContentExtractionRegionResult>();
        public bool ErrorOccured { get; set; }
        public IList<string> ErrorMessages { get; set; } = new List<string>();
    }

    public class PdfContentExtractionRegionResult
    {
        public string OptionName { get; set; }
        public int Page { get; set; }
        public string Text { get; set; }
        public float Top { get; set; }
        public float Left { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
    }
}
