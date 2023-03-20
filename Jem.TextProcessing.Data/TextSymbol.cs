using System.Diagnostics.CodeAnalysis;

namespace Jem.TextProcessing.Data;

public class TextSymbol : TextBase
{
    public TextSymbol() : base() { }

    [SetsRequiredMembers]
    public TextSymbol(string text, TextRect rect, double baseLine, int color)
        : base(text, rect, baseLine, color)
    {
    }

}
