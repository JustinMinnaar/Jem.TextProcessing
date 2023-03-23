using Jem.TextProcessing.Data;

namespace Jem.TextProcessing.Analyzers;

public class FontAnalyzer
{
    private readonly List<TextFont> _sampleFonts;

    public FontAnalyzer(IEnumerable<TextFont> sampleFonts)
    {
        _sampleFonts = sampleFonts.ToList();
    }

    public void AnalyzeFonts(IEnumerable<TextSymbol> symbols)
    {
        foreach (var symbol in symbols)
        {
            var matchedFont = FindMatchingFont(symbol);
            if (matchedFont != null)
            {
                symbol.Font = matchedFont;
            }
        }
    }

    private TextFont? FindMatchingFont(TextSymbol symbol)
    {
        foreach (var font in _sampleFonts)
        {
            if (IsMatch(symbol, font))
            {
                return font;
            }
        }

        return null;
    }

    private bool IsMatch(TextSymbol symbol, TextFont font)
    {
        foreach (var sampleSymbol in font.Symbols)
        {
            if (IsSymbolMatch(symbol, sampleSymbol))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsSymbolMatch(TextSymbol symbol, TextSymbol sampleSymbol, double variance = 0.1)
    {
        var widthDiff = Math.Abs(symbol.Rect!.Width - sampleSymbol.Rect!.Width);
        var heightDiff = Math.Abs(symbol.Rect.Height - sampleSymbol.Rect.Height);
        var sizeDiff = Math.Sqrt(widthDiff * widthDiff + heightDiff * heightDiff);
        var sizeVariance = variance * Math.Sqrt(sampleSymbol.Rect.Width * sampleSymbol.Rect.Width + sampleSymbol.Rect.Height * sampleSymbol.Rect.Height);

        if (sizeDiff > sizeVariance)
            return false;

        var diff = Math.Abs((double)(symbol.BaseLine! - sampleSymbol.BaseLine!));
        if (diff > 1)
        {
            return false;
        }

        // Check if the color is a close match (within a certain threshold)
        var colorDiff = Math.Abs((int)(symbol.Color! - sampleSymbol.Color!));
        if (colorDiff > 20)
        {
            return false;
        }

        // Add more matching criteria here if needed

        return true;
    }

}
