using GdPicture14;
using Simplic.DocumentProcessing.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplic.DocumentProcessing.Service
{
    /// <summary>
    /// Barcode recognition service 
    /// </summary>
    public class BarcodeRecognitionService : IBarcodeRecognitionService
    {
        private IPdfToTiffService pdfToTiffService;

        /// <summary>
        /// Initialize service
        /// </summary>
        /// <param name="pdfToTiffService">Tiff to pdf service</param>
        public BarcodeRecognitionService(IPdfToTiffService pdfToTiffService)
        {
            this.pdfToTiffService = pdfToTiffService;
        }

        /// <summary>
        /// Execute barcode recognition
        /// </summary>
        /// <param name="blob">Blob to test</param>
        /// <param name="fileExtension">file extension</param>
        /// <param name="options">Settings/options</param>
        /// <returns>List of detected barcodes</returns>
        public IList<BarcodeRecognitionResult> Process(byte[] blob, string fileExtension, BarcodeRecognitionOption options)
        {
            var result = new List<BarcodeRecognitionResult>();

            byte[] imgData = blob;
            if (fileExtension?.ToLower()?.Replace(".", "") == "pdf")
            {
                imgData = pdfToTiffService.Convert(blob);
            }

            var barcodeTypes = Barcode1DReaderType.Barcode1DReaderCode128 | Barcode1DReaderType.Barcode1DReaderEAN13 | Barcode1DReaderType.Barcode1DReaderEAN8;

            if (options.BarcodeTypes != null && options.BarcodeTypes.Any())
            {
                barcodeTypes = Barcode1DReaderType.Barcode1DReaderNone;

                foreach (var type in options.BarcodeTypes)
                {
                    if (string.IsNullOrWhiteSpace(type))
                        continue;

                    barcodeTypes = barcodeTypes | (Barcode1DReaderType)Enum.Parse(typeof(Barcode1DReaderType), $"Barcode1DReader{type}", true);
                }
            }

            using (var gdPictureImage = GdPictureHelper.GetImagingInstance())
            {
                int imageID = gdPictureImage.CreateGdPictureImageFromByteArray(imgData);
                gdPictureImage.TiffOpenMultiPageForWrite(true);

                if (!gdPictureImage.TiffIsMultiPage(imageID))
                {
                    int imagetmp = imageID;
                    imageID = gdPictureImage.TiffCreateMultiPageFromGdPictureImage(imagetmp);
                    gdPictureImage.ReleaseGdPictureImage(imagetmp);
                }

                int pageCount = gdPictureImage.TiffGetPageCount(imageID);

                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    gdPictureImage.TiffSelectPage(imageID, pageNumber);

                    var status = gdPictureImage.Barcode1DReaderDoScan(imageID, Barcode1DReaderScanMode.BestQuality, barcodeTypes, false, 1);
                    if (status == GdPictureStatus.OK)
                    {
                        var barcodeAmount = gdPictureImage.Barcode1DReaderGetBarcodeCount();

                        for (int i = 1; i <= barcodeAmount; i++)
                        {
                            result.Add(new BarcodeRecognitionResult
                            {
                                Barcode = gdPictureImage.Barcode1DReaderGetBarcodeValue(i),
                                Page = pageNumber,
                                BarcodeType = gdPictureImage.Barcode1DReaderGetBarcodeType(i).ToString()
                            });
                        }

                        gdPictureImage.Barcode1DReaderClear();
                    }
                }
                gdPictureImage.ReleaseGdPictureImage(imageID);
            }

            return result;
        }
    }
}
