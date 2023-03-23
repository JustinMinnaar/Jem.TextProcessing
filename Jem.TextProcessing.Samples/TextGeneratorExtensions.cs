using Jem.TextProcessing.Data;

using System.Drawing;

namespace Jem.TextProcessing.Samples;

public  class TextPageGenerator
{
    public float currentX = 0;
    public float currentY = 0;
    public TextWord? currentWord;
    public TextFont? currentFont; 

    public void SetFont(TextFont font) => currentFont = font;

    public static TextBlock AddBlock(this TextPage page, TextFont font,
        float left, float top, List<string> lines, double lineHeight)
    {
        
        var block = new TextBlock();

        float width = 0f;
        float height = 0f;
        foreach (var lineText in lines)
        {
            var line = AddLine(block, font, lineText, lineHeight);
            width = Math.Max(block.Rect.Width, line.Rect.Width);
            height += line.Rect.Height;
        }
        block.Rect = new RectangleF(left, top, width, height);

        page.Blocks.Add(block);
        return block;
    }

    public static TextLine AddLine(this TextBlock block, TextFont font,
        string lineText, float left, float top, double lineHeight)
    {
        var line = new TextLine();
        line.Rect = new RectangleF(left, top, 0, 0);

        var words = lineText.Split(' ');
        float  width = 0f;
        float height = 0f;
        foreach (var word in words)
        {
            var size = AddWord(line, font, word);
            width += size.Width;
            height = Math.Max(height, size.Height);
        }
        line.Rect.Top = block.Rect.Height;
        line.Rect.Left = 0;

        block.Lines.Add(line);
        block.Rect.Height += lineHeight;
        return line;
    }

    public static TextWord AddWord(this TextLine line, TextFont font, string wordText)
    {
        var word = new TextWord();
        word.Rect = new RectangleF() { Left = line.Rect!.Left, Top = 0, Width = 0, Height = 0 };

        var symbols = AddSymbols(word, font, wordText);
        word.Rect.Width = symbols.Width;
        word.Rect.Height = symbols.Height;

        line.Words.Add(word);
        return word.Rect;
    }

    public static RectangleF AddSymbols(string wordText)
    {
        var rect = new RectangleF()
        {
            Left = word.Rect!.Width,
            Top = 0,
            Width = 0,
            Height = 0
        };

        foreach (var ch in wordText)
        {
            var symbol = font[ch.ToString()];
            if (symbol != null)
            {
                var symbolRect = new RectangleF()
                {
                    Left = rect.Width,
                    Top = symbol.Rect!.Top,
                    Width = symbol.Rect!.Width,
                    Height = symbol.Rect!.Height
                };
                rect.Width += symbol.Rect.Width;
                rect.Height = Math.Max(rect.Height, symbol.Rect.Height);

                word.Symbols.Add(new TextSymbol
                {
                    Text = ch.ToString(),
                    Rect = symbolRect
                });
            }
        }
        word.Rect.Width = rect.Width;
        word.Rect.Height = rect.Height;
        return rect;
    }


}
