using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing
{
    public interface IPdfToTiffService
    {
        byte[] Convert(byte[] data);
    }
}
