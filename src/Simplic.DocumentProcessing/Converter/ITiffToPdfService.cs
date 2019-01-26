using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing
{
    public interface ITiffToPdfService
    {
        byte[] Convert(byte[] data, bool embeddOCRText = true, string language = "deu");
    }
}
