using GdPicture14;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Simplic.DocumentProcessing.Service
{
    /// <summary>
    /// Text search service
    /// </summary>
    public class PdfTextSearchService : IPdfTextSearchService
    {
        private GdPicturePDF pdfInstance;

        /// <summary>
        /// Initialize service
        /// </summary>
        public PdfTextSearchService()
        {
            pdfInstance = GdPictureHelper.GetPDFInstance();
        }

        /// <summary>
        /// Search text within a pdf
        /// </summary>
        /// <param name="pdf">Pdf as byte array</param>
        /// <param name="searchTexts">Search texts</param>
        /// <param name="caseSensitive">Gets or sets whether the search process is case sensitive</param>
        /// <returns>Search result list</returns>
        public IList<PdfSearchResult> Search(byte[] pdf, IList<string> searchTexts, bool caseSensitive = true)
        {
            searchTexts = searchTexts?.Where(x => !string.IsNullOrWhiteSpace(x))?.ToList() ?? new List<string>();

            if (!searchTexts.Any())
                return new List<PdfSearchResult>();

            pdfInstance.LoadFromStream(new MemoryStream(pdf));
            var result = new List<PdfSearchResult>();

            for (int i = 1; i <= pdfInstance.GetPageCount(); i++)
            {
                var text = pdfInstance.GetPageText();

                foreach (var searchText in searchTexts)
                {
                    var match = false;

                    if (caseSensitive)
                        match = text.Contains(searchText);
                    else
                        match = text.ToLower().Contains(searchText.ToLower());

                    if (match)
                        result.Add(new PdfSearchResult { PageNumber = i, SearchText = searchText });
                }
            }

            return result;
        }

        /// <summary>
        /// Dispose service
        /// </summary>
        public void Dispose()
        {
            pdfInstance?.Dispose();
        }
    }
}
