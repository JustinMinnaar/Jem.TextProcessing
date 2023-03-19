using Jem.TextProcessing.Data;

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Jem.TextProcessing.Samples;

public class FontFactory
{
    public static TextFont CreateFont(string fontName, float fontSize)
    {
        var symbols = new List<Symbol>();

        using (var bitmap = new Bitmap(1, 1))
        using (var graphics = Graphics.FromImage(bitmap))
        using (var font = new Font(fontName, fontSize))
        {
            var baseline = graphics.MeasureString("A", font).Height;
            var spaceSymbol = CreateSymbol(" ", font, baseline);
            symbols.Add(spaceSymbol);

            double maxWidth = 0f;

            // Common letters
            for (char c = 'A'; c <= 'Z'; c++)
            {
                var text = c.ToString();
                var symbol = CreateSymbol(text, font, baseline);
                symbols.Add(symbol);
                maxWidth = Math.Max(maxWidth, symbol.Rect.Width);
            }

            // Common digits
            for (char c = '0'; c <= '9'; c++)
            {
                var text = c.ToString();
                var symbol = CreateSymbol(text, font, baseline);
                symbols.Add(symbol);
                maxWidth = Math.Max(maxWidth, symbol.Rect.Width);
            }

            // Common punctuation
            var punctuation = "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
            foreach (var c in punctuation)
            {
                var text = c.ToString();
                var symbol = CreateSymbol(text, font, baseline);
                symbols.Add(symbol);
                maxWidth = Math.Max(maxWidth, symbol.Rect.Width);
            }

            // Calculate left offset
            var leftOffset = (maxWidth - spaceSymbol.Rect.Width) / 2;
            spaceSymbol.Rect.Left = leftOffset;
            foreach (var symbol in symbols.Where(s => s.Text != " "))
            {
                leftOffset = (maxWidth - symbol.Rect.Width) / 2;
                symbol.Rect.Left = leftOffset;
            }
        }

        return new TextFont(fontName, fontSize, symbols);
    }

    private static Symbol CreateSymbol(string text, Font font, float baseline)
    {
        using var bitmap = new Bitmap(1, 1) ;
        using var graphics = Graphics.FromImage(bitmap);
        
        var size = graphics.MeasureString(text, font);
        var position = new PointF(0, 0);
        var format = StringFormat.GenericTypographic;
        var path = new GraphicsPath();
        path.AddString(text, font.FontFamily, (int)font.Style, font.Size, position, format);
        var bounds = path.GetBounds();
        var rect = new TextRect() { Left = 0, Top = baseline - bounds.Height, Width = bounds.Width, Height = bounds.Height };
        
        var symbol = new Symbol(text, rect, baseline, 0);
        return symbol;
    }
}