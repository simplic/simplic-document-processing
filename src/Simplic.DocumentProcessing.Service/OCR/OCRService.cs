using GdPicture14;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing.Service
{
    /// <summary>
    /// OCR service
    /// </summary>
    public class OCRService : IOCRService
    {
        private GdPictureImaging imaging;
        private GdPictureOCR ocr;
        private int imageId;
        private bool isMultipageImage;

        /// <summary>
        /// Load image
        /// </summary>
        /// <param name="image">Image object</param>
        /// <returns>True if loading was successfull</returns>
        public bool LoadImage(byte[] image)
        {
            Dispose();

            imaging = GdPictureHelper.GetImagingInstance();
            ocr = GdPictureHelper.GetOCRInstance();
            imageId = imaging.CreateGdPictureImageFromByteArray(image);

            isMultipageImage = imaging.TiffIsMultiPage(imageId);

            if (isMultipageImage)
            {
                PageCount = imaging.TiffGetPageCount(imageId);
            }
            else
            {
                PageCount = imaging.GetPageCount(imageId);
            }

            ImageHeight = imaging.GetHeight(imageId);

            return imageId != 0;
        }

        /// <summary>
        /// Extract complete text
        /// </summary>
        /// <returns>OCR result</returns>
        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        public OCRResult ExtractText()
        {
            return ExtractText(DefaultOptions);
        }

        /// <summary>
        /// Extract text using explicit options
        /// </summary>
        /// <param name="options">OCR options</param>
        /// <returns>OCR result</returns>
        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        public OCRResult ExtractText(OCROption options)
        {
            var result = new OCRResult();
            var contentBuilder = new StringBuilder();

            ocr.SetImage(imageId);

            ApplySettings(ocr, options ?? DefaultOptions);

            var pages = new List<int>();

            if (options.Pages.Count == 0)
            {
                for (int i = 1; i <= PageCount; i++)
                    pages.Add(i);
            }
            else
            {
                pages.AddRange(options.Pages);
            }

            foreach (var page in pages)
            {
                GdPictureStatus status;
                if (isMultipageImage)
                    status = imaging.TiffSelectPage(imageId, page);
                else
                    status = imaging.SelectPage(imageId, page);

                if (status != GdPictureStatus.OK)
                {
                    result.ErrorOccured = true;
                    result.ErrorMessages.Add($"Error during page selection. Page: {page}");
                    continue;
                }

                if (ocr.SetImage(imageId) != GdPictureStatus.OK)
                {
                    result.ErrorOccured = true;
                    result.ErrorMessages.Add($"Error during setting image. Page: {page}");
                    continue;
                }

                var resultId = ocr.RunOCR();
                string text = ocr.GetOCRResultText(resultId);

                // Add result
                var regionResult = new OCRRegionResult
                {
                    OptionName = options.OptionName,
                    Height = options.Height,
                    Left = options.Left,
                    Page = page,
                    Top = options.Top,
                    Width = options.Width,
                    Text = text
                };
                result.RegionResults.Add(regionResult);

                contentBuilder.AppendLine(text);
            }

            result.Text = contentBuilder.ToString();
            return result;
        }

        /// <summary>
        /// Dispose service
        /// </summary>
        public void Dispose()
        {
            if (imageId != 0)
                imaging?.ReleaseGdPictureImage(imageId);
            
            ocr?.Dispose();
            imaging?.Dispose();
            imageId = 0;
        }

        /// <summary>
        /// Apply ocr settings
        /// </summary>
        /// <param name="ocr"></param>
        /// <param name="options"></param>
        private void ApplySettings(GdPictureOCR ocr, OCROption options)
        {
            if (string.IsNullOrWhiteSpace(options.ResourceFolder))
                options.ResourceFolder = GdPictureHelper.OCRDirectory;

            ocr.Context = (GdPicture14.OCRContext)(int)options.Context;
            ocr.LanguageModelPenaltyNonDictWords = 0.15f;
            ocr.LanguageModelPenaltyNonFreqDictWords = 0.1f;
            ocr.ResourceFolder = options.ResourceFolder;
            ocr.LoadMainDictionary = options.UseMainDictionary;
            ocr.LoadFreqWordsDictionary = options.UseFreqWordsDictionary;
            ocr.EnableOrientationDetection = options.EnableOrientationDetection;
            ocr.EnableSkewDetection = options.EnableSkewDetection;

            ocr.ResetSelectedDictionaries();

            if (options.Languages != null)
            {
                foreach (var language in options.Languages)
                {
                    ocr.AddLanguage((GdPicture14.OCRLanguage)(int)language);
                }
            }
            else
            {
                //using german by default.
                ocr.AddLanguage(GdPicture14.OCRLanguage.German);
            }

            ocr.OCRMode = (GdPicture14.OCRMode)(int)options.Mode;
            ocr.CharacterSet = options.CharacterSet;

            if (options.Left != 0 && options.Top != 0 && options.Width != 0 && options.Height != 0)
                ocr.SetROI(options.Left, options.Top, options.Width, options.Height);
            else
                ocr.ResetROI();
        }

        /// <summary>
        /// Gets or sets the current OCR settings. Settings are assigned by default
        /// </summary>
        public OCROption DefaultOptions { get; set; } = new OCROption();
        public int PageCount { get; set; }
        public float ImageHeight { get; set; }
    }
}
