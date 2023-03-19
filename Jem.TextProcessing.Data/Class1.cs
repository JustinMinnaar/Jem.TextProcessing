using System.Diagnostics.CodeAnalysis;

namespace Jem.TextProcessing.Data;

public abstract class TextBase
{
    protected TextBase()
    {
    }

    [SetsRequiredMembers]
    protected TextBase(string text, TextRect rect, double baseLine, int color)
    {
        Text = text;
        Rect = rect;
        BaseLine = baseLine;
        Color = color;
    }

    public required string Text { get; set; }
    public required TextRect Rect { get; set; }
    public required double BaseLine { get; set; }
    public required int Color { get; set; }
}

public class Page
{
    public List<Block> Blocks { get; set; } = new();
}

public class Block : TextBase
{
    public List<Line> Lines { get; set; } = new();
}

public class Line : TextBase
{
    public List<Word> Words { get; set; } = new();
}

public class Word : TextBase
{
    public List<Symbol> Symbols { get; set; } = new();
}

public class Symbol : TextBase
{
    public Symbol() : base() { }

    [SetsRequiredMembers]
    public Symbol(string text, TextRect rect, double baseLine, int color)
        : base(text, rect, baseLine, color)
    {
    }
}

public class TextRect
{
    public double Left { get; set; }
    public double Top { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
}

public class TextFont
{
    public required string FontName { get; set; }
    
    public float FontSize { get; set; }
    
    public List<Symbol> Symbols { get; set; } = new();
    
    public Symbol? this[string text]
    {
        get
        {
            return Symbols.Find(s => s.Text == text);
        }
    }

    public TextFont() { }

    [SetsRequiredMembers]
    public TextFont(string fontName, float fontSize, IEnumerable<Symbol> symbols)
    {
        FontName = fontName;
        FontSize = fontSize;
        Symbols.AddRange(symbols);
    }
}
