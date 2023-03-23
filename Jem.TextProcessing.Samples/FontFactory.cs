using Jem.TextProcessing.Data;

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Jem.TextProcessing.Samples;
public class FontFactory
{
    public TextFont CreateFont(string fontName, float fontSize)
    {
        var symbols = new List<TextSymbol>();

        using var bitmap = new Bitmap(1, 1);
        using var graphics = Graphics.FromImage(bitmap);
        using var font = new Font(fontName, fontSize);

        var spaceSymbol = CreateSymbol(" ", font);
        symbols.Add(spaceSymbol);

        double maxWidth = 0f;

        // Common letters
        for (char c = 'A'; c <= 'Z'; c++)
            maxWidth = CreateSymbol(symbols, font, maxWidth, c);

        // Common digits
        for (char c = '0'; c <= '9'; c++)
            maxWidth = CreateSymbol(symbols, font, maxWidth, c);

        // Common punctuation
        var punctuation = "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
        foreach (var c in punctuation)
            maxWidth = CreateSymbol(symbols, font, maxWidth, c);

        // Calculate left offset
        var leftOffset = (maxWidth - spaceSymbol.Rect!.Width) / 2;
        spaceSymbol.Rect.Left = leftOffset;
        foreach (var symbol in symbols.Where(s => s.Text != " "))
        {
            leftOffset = (maxWidth - symbol.Rect!.Width) / 2;
            symbol.Rect.Left = leftOffset;
        }

        return new TextFont(fontName, fontSize, symbols);
    }

    private static double CreateSymbol(List<TextSymbol> symbols, Font font, double maxWidth, char c)
    {
        {
            var text = c.ToString();
            var symbol = CreateSymbol(text, font);
            symbols.Add(symbol);
            maxWidth = Math.Max(maxWidth, symbol.Rect!.Width);
        }

        return maxWidth;
    }

    static TextSymbol CreateSymbol(string text, Font font)
    {
        using var bitmap = new Bitmap(1, 1);
        using var graphics = Graphics.FromImage(bitmap);

        var size = graphics.MeasureString(text, font);
        var position = new PointF(0, 0);
        var format = StringFormat.GenericTypographic;
        var path = new GraphicsPath();
        path.AddString(text, font.FontFamily, (int)font.Style, font.Size, position, format);

        var bounds = path.GetBounds();
        var rect = new Data.RectangleF()
        {
            Left = 0,
            Top = 0, // font.Height - font.FontFamily.GetCellAscent(FontStyle.Regular),
            Width = bounds.Width,
            Height = bounds.Height
        };

        var ascent = font.FontFamily.GetCellAscent(font.Style);
        var emHeight = font.FontFamily.GetEmHeight(font.Style);
        var baseLine = (int)Math.Round(ascent / (double)emHeight * size.Height);
        var symbol = new TextSymbol(text, rect, baseLine, 0);

        return symbol;
    }
}