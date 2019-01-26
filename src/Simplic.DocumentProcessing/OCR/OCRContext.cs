using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// OCR context option
    /// </summary>
    public enum OCRContext
    {
        OCRContextDocument = 0,

        OCRContextSingleColumn = 1,

        OCRContextSingleBlock = 2,

        OCRContextSingleBlockVertical = 3,

        OCRContextSingleLine = 4,

        OCRContextSingleWord = 5,

        OCRContextSingleWordCircle = 6,

        OCRContextSingleChar = 7,

        OCRContextSparseText = 8,

        OCRContextRawLine = 9,

        OCRContextSegmentationOnly = 10
    }
}
