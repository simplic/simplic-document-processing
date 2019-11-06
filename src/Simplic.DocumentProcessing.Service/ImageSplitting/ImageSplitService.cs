using Simplic.DocumentProcessing.Service;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Simplic.DocumentProcessing.Service
{
    /// <summary>
    /// Pdf split service
    /// </summary>
    public class ImageSplitService : IImageSplitService
    {
        /// <summary>
        /// Split pdf by page range
        /// </summary>
        /// <param name="image">Pdf to split</param>
        /// <param name="ranges">List of page ranges</param>
        /// <returns>List of splitted pdfs</returns>
        public IList<ImageSplitResult> Split(byte[] image, IList<PageNumberRange> ranges)
        {
            var result = new List<ImageSplitResult>();

            using (var gdPictureImage = GdPictureHelper.GetImagingInstance())
            {
                int imageId = gdPictureImage.CreateGdPictureImageFromByteArray(image);
                int singlePageImageId = 0;

                if (!gdPictureImage.TiffIsMultiPage(imageId))
                {
                    singlePageImageId = imageId;
                    imageId = gdPictureImage.TiffCreateMultiPageFromGdPictureImage(imageId);
                }

                if (singlePageImageId != 0)
                    gdPictureImage.ReleaseGdPictureImage(singlePageImageId);

                if (ranges == null || ranges.Count == 0)
                    return new List<ImageSplitResult> { new ImageSplitResult { Image = image, PageCount = gdPictureImage.TiffGetPageCount(imageId) } };

                foreach (var range in ranges.Where(x => x.PageCount > 0 && x.StartPageNumber + (x.PageCount - 1) <= gdPictureImage.TiffGetPageCount(imageId)))
                {
                    var newImageId = gdPictureImage.TiffCreateMultiPageFromGdPictureImage(imageId);
                    for (int i = 1; i <= gdPictureImage.TiffGetPageCount(newImageId); i++)
                    {
                        gdPictureImage.TiffDeletePage(newImageId, i);
                    }

                    for (int i = 0; i < range.PageCount; i++)
                    {
                        gdPictureImage.SelectPage(imageId, range.StartPageNumber + i);
                        gdPictureImage.TiffAppendPageFromGdPictureImage(newImageId, imageId);
                    }

                    using (var targetStream = new MemoryStream())
                    {
                        gdPictureImage.SaveAsStream(newImageId, targetStream, GdPicture14.DocumentFormat.DocumentFormatTIFF, 4);
                        targetStream.Position = 0;
                        result.Add(new ImageSplitResult { Image = targetStream.ToArray(), PageCount = range.PageCount });

                        gdPictureImage.ReleaseGdPictureImage(newImageId);
                    }
                }

                gdPictureImage.ReleaseGdPictureImage(imageId);
            }

            return result;
        }
    }
}
