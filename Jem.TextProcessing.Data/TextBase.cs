using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reflection;
using System.Runtime.Intrinsics.X86;

namespace Jem.TextProcessing.Data;

public abstract class TextBase
{
    protected TextBase()
    {
    }

    protected TextBase(string text, RectangleF rect, TextFont? font, double baseLine, int color)
    {
        Text = text;
        Rect = rect;
        Font = font;
        BaseLine = baseLine;
        Color = color;
    }

    public string Text { get; set; } = string.Empty;
    public RectangleF Rect { get; set; }
    public TextFont? Font { get; set; }
    /// <summary>
    /// The baseline of a font is the imaginary line upon which characters sit. It is the line that defines the height of lowercase letters like "x" and "o", as well as uppercase letters like "H" and "M". The baseline is used as a reference point for positioning characters and is an important concept in typography.
    /// In the context of the TextBase class, the BaseLine property can be used to represent the baseline of a symbol or character.The BaseLine property should be set to a value that reflects the position of the baseline relative to the top of the symbol's bounding box.
    /// The actual baseline value for a font can be determined by inspecting the font metrics.For example, in the System.Drawing namespace, you can use the FontFamily.GetCellAscent method to get the ascent(distance from the baseline to the top of the font's em square) and FontFamily.GetEmHeight to get the height of the font's em square.The difference between these two values gives you the position of the baseline relative to the top of the em square.
    /// </summary>
    public double BaseLine { get; set; }
    public int Color { get; set; }
}
