namespace Jem.TextProcessing.Data;

public static class TextDataExtensions
{
    public static TextBlock AddBlock(this TextPage page, TextFont font, double left, double top, List<string> lines, double lineHeight)
    {
        var block = new TextBlock();
        page.Blocks.Add(block);

        var blockRect = new RectangleF();
        foreach (var lineText in lines)
        {
            var lineRect = block.AddLine(font, lineText, lineHeight);
            lineRect.Top += blockRect.Height;
            blockRect.Height += lineRect.Height;
            blockRect.Width = Math.Max(blockRect.Width, lineRect.Width);
        }

        blockRect.Left = left;
        blockRect.Top = top;
        block.Rect = blockRect;

        return block;
    }

    public static RectangleF AddLine(this TextBlock block, TextFont font, string lineText, double lineHeight)
    {
        var line = new TextLine();
        block.Lines.Add(line);

        var lineRect = new RectangleF();
        var words = lineText.Split(' ');
        foreach (var wordText in words)
        {
            var wordRect = line.AddWord(font, wordText);
            wordRect.Left += lineRect.Width;
            lineRect.Width += wordRect.Width;
            lineRect.Height = Math.Max(lineRect.Height, wordRect.Height);
        }

        lineRect.Top = block.Rect.Height;
        line.Rect = lineRect;

        return lineRect;
    }

    public static RectangleF AddWord(this TextLine line, TextFont font, string wordText)
    {
        var word = new TextWord();
        line.Words.Add(word);

        var wordRect = new RectangleF();
        foreach (var symbolText in wordText)
        {
            var symbol = font.Symbols.FirstOrDefault(s => s.Text == symbolText.ToString());
            if (symbol != null)
            {
                var symbolCopy = symbol.Copy();
                word.Symbols.Add(symbolCopy);
                symbolCopy.Rect.Left += wordRect.Width;
                wordRect.Width += symbolCopy.Rect.Width;
                wordRect.Height = Math.Max(wordRect.Height, symbolCopy.Rect.Height);
            }
        }

        return wordRect;
    }

    public static RectangleF AddSymbols(this TextWord word, TextFont font, string wordText)
    {
        var symbolRect = new RectangleF();
        foreach (var symbolText in wordText)
        {
            var symbol = font.Symbols.FirstOrDefault(s => s.Text == symbolText.ToString());
            if (symbol != null)
            {
                var symbolCopy = symbol.Copy();
                word.Symbols.Add(symbolCopy);
                symbolCopy.Rect.Left += symbolRect.Width;
                symbolRect.Width += symbolCopy.Rect.Width;
                symbolRect.Height = Math.Max(symbolRect.Height, symbolCopy.Rect.Height);
            }
        }

        return symbolRect;
    }
}
