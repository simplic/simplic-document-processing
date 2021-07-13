using System.Drawing;

namespace Simplic.DocumentProcessing
{
    public class PdfAnnotation
    {
        /// <summary>
        /// Left position of this annotation in millimeters
        /// </summary>
        public float Left { get; set; }
        
        /// <summary>
        /// Top position of this annotation in millimeters
        /// </summary>
        public float Top { get; set; }
        
        /// <summary>
        /// Width of this annotation in millimeters
        /// </summary>
        public float Width { get; set; }
        
        /// <summary>
        /// Height of this annotation in millimeters
        /// </summary>
        public float Height { get; set; }
        
        /// <summary>
        /// Whether or not the annotation has borders
        /// </summary>
        public bool HasBorder { get; set; } = false;
        
        /// <summary>
        /// The actual content of the annotation
        /// </summary>
        public string Content { get; set; } = "";
        
        /// <summary>
        /// Fontname which defaults to "Arial"
        /// </summary>
        public string FontName { get; set; } = "Arial";
        
        /// <summary>
        /// Font size which defaults to 12
        /// </summary>
        public float FontSize { get; set; } = 12;
        
        /// <summary>
        /// Font Color
        /// </summary>
        public Color FontColor { get; set; } = Color.Black;
        
        /// <summary>
        /// The backgroundcolor which will fill the background of this annotation
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.White;
        
        /// <summary>
        /// Opacity, reaching from 0 (transparent) to 1 (opaque)
        /// </summary>
        public float Opacity { get; set; } = 1.0f;
    }
}
