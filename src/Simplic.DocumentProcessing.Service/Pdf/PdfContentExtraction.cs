using GdPicture14;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing.Service
{
    /// <summary>
    /// PDf content extraction service
    /// </summary>
    public class PdfContentExtractionService : IPdfContentExtractionService
    {
        private GdPicturePDF pdfInstance;

        /// <summary>
        /// Initialize service
        /// </summary>
        public PdfContentExtractionService()
        {
            pdfInstance = GdPictureHelper.GetPDFInstance();
        }

        /// <summary>
        /// Load pdf blob.
        /// </summary>
        /// <param name="pdf">Pdf blob</param>
        /// <returns>True if loading was successfull</returns>
        public bool LoadPdf(byte[] pdf)
        {
            Dispose();

            pdfInstance.LoadFromStream(new MemoryStream(pdf));
            PageCount = pdfInstance.GetPageCount();

            return true;
        }

        /// <summary>
        /// Extract complete text. Uses the default options <see cref="DefaultOptions"/>
        /// </summary>
        /// <returns>Extraction result</returns>
        [HandleProcessCorruptedStateExceptions]
        public PdfContentExtractionResult ExtractText()
        {
            return ExtractText(DefaultOptions);
        }

        /// <summary>
        /// Extract text using extraction options
        /// </summary>
        /// <param name="options">Option set</param>
        /// <returns>Extraction result</returns>
        [HandleProcessCorruptedStateExceptions]
        public PdfContentExtractionResult ExtractText(PdfContentExtractionOption options)
        {
            try
            {
                var result = new PdfContentExtractionResult();
                var sb = new StringBuilder();

                for (int i = 1; i <= PageCount; i++)
                {
                    try
                    {
                        if (options.Pages.Count == 0 || options.Pages.Contains(i))
                        {
                            var region = new PdfContentExtractionRegionResult
                            {
                                Page = i,
                                Height = options.Height,
                                Width = options.Width,
                                Top = options.Top,
                                Left = options.Left
                            };

                            result.RegionResults.Add(region);

                            var status = pdfInstance.SelectPage(i);
                            if (status == GdPictureStatus.OK)
                            {
                                var text = "";

                                if (options.Left == 0 && options.Top == 0 && options.Width == 0 && options.Height == 0)
                                    text = pdfInstance.GetPageText();
                                else
                                    text = pdfInstance.GetPageTextArea(options.Left, options.Top, options.Width, options.Height);

                                status = pdfInstance.GetStat();
                                if (status == GdPictureStatus.OK)
                                {
                                    sb.AppendLine(text);
                                    region.Text = text;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        result.ErrorMessages.Add(ex.ToString());
                        result.ErrorOccured = true;
                    }
                }

                result.Text = sb.ToString();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in content extraction", ex);
            }
        }

        /// <summary>
        /// Dispose service
        /// </summary>
        public void Dispose()
        {
            try
            {
                pdfInstance?.CloseDocument();
            }
            catch
            {
                /* swallow */
            }

            pdfInstance?.Dispose();
        }

        /// <summary>
        /// Gets or sets the default options
        /// </summary>
        public PdfContentExtractionOption DefaultOptions
        {
            get;
            set;
        } = new PdfContentExtractionOption();

        /// <summary>
        /// Gets the complete page count
        /// </summary>
        public int PageCount
        {
            get;
            set;
        }
    }
}
