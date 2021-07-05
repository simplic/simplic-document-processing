using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// Barcode recognition settings/options 
    /// </summary>
    public class BarcodeRecognitionOption
    {
        public IList<string> BarcodeTypes { get; set; } = new List<string>();
        public bool ConvertToBackAndWhite { get; set; } = true;
    }
}
