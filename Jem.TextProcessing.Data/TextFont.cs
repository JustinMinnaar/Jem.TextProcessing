using System.Diagnostics.CodeAnalysis;

namespace Jem.TextProcessing.Data;

public sealed class TextFont
{
    public required string FontName { get; set; }
    
    public float FontSize { get; set; }
    
    public List<TextSymbol> Symbols { get; set; } = new();
    
    public TextSymbol? this[string text]
    {
        get
        {
            return Symbols.Find(s => s.Text == text);
        }
    }

    public TextFont() { }

    [SetsRequiredMembers]
    public TextFont(string fontName, float fontSize, IEnumerable<TextSymbol> symbols)
    {
        FontName = fontName;
        FontSize = fontSize;
        Symbols.AddRange(symbols);
    }
}
