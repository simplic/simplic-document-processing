using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing
{
    public class OCROption
    {
        public const string Charset_Number = "1234567890,.-";
        public const string Charset_Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZäüöÄÜÖ";
        public const string Charset_Separator_Sentence = ".-/\\;,#+ ?!%&{}[]\"'*´()$§<>|`";
        public string LanguageValue
        {
            get;
            set;
        } = "deu";

        public OCRContext Context
        {
            get;
            set;
        } = OCRContext.OCRContextDocument;

        public OCRMode Mode
        {
            get;
            set;
        } = OCRMode.FavorAccuracy;

        public IList<OCRLanguage> Languages
        {
            get;
            set;
        } = new List<OCRLanguage> { OCRLanguage.German };

        public bool UseMainDictionary
        {
            get;
            set;
        } = true;

        public bool UseFreqWordsDictionary
        {
            get;
            set;
        } = false;

        public bool EnableOrientationDetection
        {
            get;
            set;
        } = false;

        public bool EnableSkewDetection
        {
            get;
            set;
        } = true;

        public int Left
        {
            get;
            set;
        }

        public int Top
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public string CharacterSet
        {
            get;
            set;
        }

        public string OptionName { get; set; }

        /// <summary>
        /// Gets or sets a list of pages to analyze. If no pages are inserted, all will be analyzed
        /// </summary>
        public IList<int> Pages { get; set; } = new List<int>();
        
        /// <summary>
        /// Gets or sets the ocr language directory. If nothing is set, the default directory will be used
        /// </summary>
        public string ResourceFolder
        {
            get;
            set;
        }
    }
}
