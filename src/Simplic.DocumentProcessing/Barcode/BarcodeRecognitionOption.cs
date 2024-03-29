﻿using System.Collections.Generic;

namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// Barcode recognition settings/options 
    /// </summary>
    public class BarcodeRecognitionOption
    {
        /// <summary>
        /// Gets or sets a list of barcode tyes. E.g. Code39, Code128
        /// </summary>
        public IList<string> BarcodeTypes { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets whether to convert the document to black-and-white before reading barcodes.
        /// </summary>
        public bool ConvertToBlackAndWhite { get; set; }

        /// <summary>
        /// Gets or sets the DPI that is used when converting a pdf to tiff before barcode-recognition
        /// </summary>
        public int PdfToTiffDPI { get; set; } = 300;
    }
}
