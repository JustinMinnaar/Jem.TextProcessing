using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Jem.TextProcessing.Data;

public class TextSymbol : TextBase
{
    public TextSymbol() : base() { }

    [SetsRequiredMembers]
    public TextSymbol(string text, RectangleF rect, TextFont? font, double baseLine, int color)
        : base(text, rect, font, baseLine, color)
    {
    }

    /// <summary>
    /// The Copy method creates a new TextSymbol object and sets its properties based on the properties of the current TextSymbol object. The Text property is simply copied, while the Rect property is copied by creating a new TextRect object and copying its Left, Top, Width, and Height properties individually. Finally, the BaseLine and Color properties are copied directly. The method then returns the newly created TextSymbol object.
    /// </summary>
    /// <returns></returns>
    public TextSymbol Copy()
    {
        return new TextSymbol
        {
            Text = Text,
            Rect = new RectangleF
            {
                Left = Rect.Left,
                Top = Rect.Top,
                Width = Rect.Width,
                Height = Rect.Height
            },
            BaseLine = BaseLine,
            Color = Color
        };
    }
}
