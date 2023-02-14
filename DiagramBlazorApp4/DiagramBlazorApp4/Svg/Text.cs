using System.Xml;
using Microsoft.Maui.Graphics.Skia;
using Microsoft.Maui.Graphics;
//using dwg = System.Drawing;
using SkiaSharp;
using DiagramBlazorApp4.Svg;

namespace SvgLayerSample.Svg {
    public class Text : SvgElement {
        public string Content { get; set; }
        public double TextWidth { get; set; }
        public double TextHeight { get; set; }

        //private dwg.Font _font = new dwg.Font(dwg.FontFamily.GenericSerif, 16);
        //public dwg.Font Font { get { return _font; }
        //    set {
        //        _font = value;
        //        (this.TextWidth, this.TextHeight) = CalculateTextWidth(this.Content);
        //    }
        //}

        private SKTypeface _font = _defaultTypeFace;
        public SKTypeface Font
        {
            get { return _font; }
            set
            {
                _font = value;
                (this.TextWidth, this.TextHeight) = CalculateTextWidth(this.Content);
            }
        }

        public float FontSize { get; set; } = 16;

        public Text(string text) {
            this.Content = text;
            (this.TextWidth, this.TextHeight) = CalculateTextWidth(this.Content);
        }

        public override string ToString() {
            return $"<text x='{X}' y='{Y}'>{Content}</text>";
        }

        private (float Width, float Height) CalculateTextWidth(string text) {
            //using (dwg.Bitmap bmp = new dwg.Bitmap(1, 1))
            //using (dwg.Graphics g = dwg.Graphics.FromImage(bmp)) {
            //    var measurements = g.MeasureString(text, this.Font);
            //    return (measurements.Width, measurements.Height);
            //}

            var fonts = SKFontManager.Default.FontFamilies.ToArray();
            SKPaint paint = new SKPaint();
            paint.TextEncoding = SKTextEncoding.Utf16;
            paint.IsAntialias = true;
            paint.Typeface = _defaultTypeFace;
            paint.TextSize = FontSize;
            paint.TextAlign = SKTextAlign.Left;
            //paint.SubpixelText = true;
            //paint.IsLinearText= true;

            SKRect rect = new SKRect();
            float width = paint.MeasureText(text, ref rect);
            return (width, rect.Height - rect.Top);
        }

        public override void WriteTo(XmlWriter writer) {
            writer.WriteRaw(this.ToString());
        }

        private static SKTypeface _defaultTypeFace = FontFactory.Roboto;
    }
}
