using Jem.TextProcessing.Data;

namespace Jem.TextProcessing.Samples;

public class TextGenerator
{
    public static TextBlock AddBlock(TextPage page, TextFont font, double left, double top, List<string> lines, double lineHeight)
    {
        var block = new TextBlock
        {
            Rect = new TextRect { Left = left, Top = top, Width = 0, Height = 0 },
        };
        foreach (var line in lines)
        {
            var size = AddLine(block, font, line, lineHeight);
            block.Rect.Width = Math.Max(block.Rect.Width, size.Width);
            block.Rect.Height += size.Height;
        }
        page.Blocks.Add(block);
        return block;
    }

    public static TextRect AddLine(TextBlock block, TextFont font, string lineText, double lineHeight)
    {
        var line = new TextLine();
        line.Rect = new TextRect() { Left = 0, Top = block.Rect!.Top, Width = 0, Height = 0 };

        var words = lineText.Split(' ');
        foreach (var word in words)
        {
            var size = AddWord(line, font, word);
            line.Rect.Width += size.Width;
            line.Rect.Height = Math.Max(line.Rect.Height, size.Height);
        }
        line.Rect.Top = block.Rect.Height;
        line.Rect.Left = 0;

        block.Lines.Add(line);
        block.Rect.Height += lineHeight;
        return line.Rect;
    }

    public static TextRect AddWord(TextLine line, TextFont font, string wordText)
    {
        var word = new TextWord();
        word.Rect = new TextRect() { Left = line.Rect!.Left, Top = 0, Width = 0, Height = 0 };

        var symbols = AddSymbols(word, font, wordText);
        word.Rect.Width = symbols.Width;
        word.Rect.Height = symbols.Height;

        line.Words.Add(word);
        return word.Rect;
    }

    public static TextRect AddSymbols(TextWord word, TextFont font, string wordText)
    {
        var rect = new TextRect()
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
                var symbolRect = new TextRect()
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
