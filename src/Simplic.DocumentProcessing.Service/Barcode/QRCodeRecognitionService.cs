using GdPicture14;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing.Service
{
    /// <summary>
    /// Service for reading qr-codes
    /// </summary>
    public class QRCodeRecognitionService : IQRCodeRecognitionService
    {
        private IPdfToTiffService pdfToTiffService;

        /// <summary>
        /// Initialize service
        /// </summary>
        /// <param name="pdfToTiffService">Tiff to pdf service</param>
        public QRCodeRecognitionService(IPdfToTiffService pdfToTiffService)
        {
            this.pdfToTiffService = pdfToTiffService;
        }

        /// <summary>
        /// Execute barcode recognition
        /// </summary>
        /// <param name="blob">Blob to test</param>
        /// <param name="fileExtension">file extension</param>
        /// <returns>List of detected barcodes</returns>
        public IList<BarcodeRecognitionResult> Process(byte[] blob, string fileExtension)
        {
            var results = new List<BarcodeRecognitionResult>();

            byte[] imgData = blob;
            if (fileExtension?.ToLower()?.Replace(".", "") == "pdf")
            {
                imgData = pdfToTiffService.Convert(blob);
            }

            using (var gdPictureImage = GdPictureHelper.GetImagingInstance())
            {
                int imageID = 0;

                try
                {
                    imageID = gdPictureImage.CreateGdPictureImageFromByteArray(imgData);
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
                        if (gdPictureImage.BarcodeQRReaderDoScan(imageID, BarcodeQRReaderScanMode.BestQuality) == GdPictureStatus.OK)
                        {
                            int barcodeCount = gdPictureImage.BarcodeQRReaderGetBarcodeCount();

                            for (int i = 1; i <= barcodeCount; i++)
                            {
                                results.Add(new BarcodeRecognitionResult
                                {
                                    Barcode = gdPictureImage.BarcodeQRReaderGetBarcodeValue(i),
                                    BarcodeType = "QR",
                                    Page = pageNumber
                                });
                            }
                        }
                    }

                    gdPictureImage.BarcodeQRReaderClear();
                    gdPictureImage.ReleaseGdPictureImage(imageID);
                }
                catch
                {
                    if (imageID != 0)
                    {
                        gdPictureImage.BarcodeQRReaderClear();
                        gdPictureImage.ReleaseGdPictureImage(imageID);
                    }

                    throw;
                }
            }

            return results;
        }
    }
}
