using GdPicture14;
using Simplic.DocumentProcessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing.Service
{
    public class PdfToTiffService : IPdfToTiffService
    {
        public byte[] Convert(byte[] data)
        {
            byte[] pdf = null;

            using (var pdfInstance = GdPictureHelper.GetPDFInstance())
            {
                using (var gdPictureImaging = GdPictureHelper.GetImagingInstance())
                {
                    int imageId = gdPictureImaging.CreateGdPictureImageFromByteArray(data);
                    if (gdPictureImaging.GetStat() == GdPictureStatus.OK)
                    {
                        float resolution = System.Math.Max(200, gdPictureImaging.GetVerticalResolution(imageId));
                        var state = pdfInstance.NewPDF(embeddOCRText);

                        if (state == GdPictureStatus.OK)
                        {
                            for (int i = 1; i <= gdPictureImaging.GetPageCount(imageId); i++)
                            {
                                if (gdPictureImaging.SelectPage(imageId, i) == GdPictureStatus.OK)
                                {
                                    var addImageResult = pdfInstance.AddImageFromGdPictureImage(imageId, false, true);
                                }
                            }

                            pdfInstance.OcrPages("*", 1, language, GdPictureHelper.OCRDirectory, "", resolution, 0, true);

                            using (var stream = new MemoryStream())
                            {
                                pdfInstance.SaveToStream(stream);
                                stream.Position = 0;
                                pdf = stream.ToArray();
                            }
                        }
                        else
                        {
                            throw new Exception($"Culd not convert document. State: {state}");
                        }
                    }
                    else
                    {
                        throw new Exception("Could not create gdpicture imaging instance");
                    }

                    // Close pdf document
                    pdfInstance?.CloseDocument();

                    // Release gdpicture image
                    gdPictureImaging.ReleaseGdPictureImage(imageId);
                }
            }

            return pdf;
        }
    }
}
