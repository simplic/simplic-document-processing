using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing
{
    public class OCRResult
    {
        public string Text { get; set; }
        public IList<OCRRegionResult> RegionResults { get; set; } = new List<OCRRegionResult>();
        public bool ErrorOccured { get; set; }
        public IList<string> ErrorMessages { get; set; } = new List<string>();
    }

    public class OCRRegionResult
    {
        public string OptionName { get; set; }
        public int Page { get; set; }
        public string Text { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}
