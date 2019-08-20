using System.Drawing;

namespace Simplic.DocumentProcessing
{
    /// <summary>
    /// Represents a text annotation
    /// </summary>
    public class TextAnnotation
    {
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the position left
        /// </summary>
        public float Left { get; set; }

        /// <summary>
        /// Gets or sets the position top
        /// </summary>
        public float Top { get; set; }

        /// <summary>
        /// Gets or sets the width
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Gets or sets the height
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// Gets or sets the font size
        /// </summary>
        public float FontSize { get; set; } = 12;

        /// <summary>
        /// Get or sets the foreground color
        /// </summary>
        public System.Drawing.Color ForeColor { get; set; } = Color.Black;

        /// <summary>
        /// Gets or sets the font name
        /// </summary>
        public string FontName { get; set; } = "Arial";
    }
}
