using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing
{
    public interface IOCRService : IDisposable
    {
        bool LoadImage(byte[] image);
        OCRResult ExtractText();
        OCRResult ExtractText(OCROption options);
        OCROption DefaultOptions { get; set; }
        int PageCount { get; set; }
        float ImageHeight { get; set; }
    }
}
