using Jem.TextProcessing.Data;

namespace Jem.TextProcessing.Analyzers;

/// <summary>
/// In this implementation, the WordAnalyzer class takes one parameter in its constructor: maxLineGap, which specifies the maximum vertical distance in units between words for them to be considered part of the same line. It has an AnalyzeWords method that takes an IEnumerable<TextWord> of words to analyze. It loops through each word and calls the IsNearWord method to determine if the word is near the previous word. If the word is near the previous word, it adds the word to the current line. If the word is not near the previous word, it creates a new line with the word and adds it to the line list.
/// The IsNearWord method checks if the vertical distance between the bottom of the current word and the bottom of the previous word is less than or equal to the maximum line gap.If it is, it returns true, indicating that the words are near each other and should be part of the same line.If it isn't, it returns false, indicating that the words are not near each other and should be part of a new line.
/// The CalculateLineRect method takes an IEnumerable<TextWord> of words and calculates the rect that encompasses all the words in the line. It does this by finding the minimum left and top coordinates of all the words and the maximum right and bottom coordinates of all the words.It then calculates the width and height of the rect and returns it.Finally, in the AnalyzeWords method, the rect is assigned to the Rect property of the line.
/// </summary>
public class WordAnalyzer
{
    private readonly double _maxLineGap;

    public WordAnalyzer(double maxLineGap)
    {
        _maxLineGap = maxLineGap;
    }

    public List<TextLine> AnalyzeWords(IEnumerable<TextWord> words)
    {
        var lineList = new List<TextLine>();

        TextLine? currentLine = null;

        foreach (var word in words)
        {
            if (currentLine == null)
            {
                currentLine = new TextLine { Words = { word } };
                lineList.Add(currentLine);
            }
            else if (IsNearWord(word, currentLine.Words.Last()))
            {
                currentLine.Words.Add(word);
            }
            else
            {
                currentLine = new TextLine { Words = { word } };
                lineList.Add(currentLine);
            }
        }

        foreach (var line in lineList)
        {
            line.Rect = CalculateLineRect(line.Words);
        }

        return lineList;
    }

    private bool IsNearWord(TextWord word1, TextWord word2)
    {
        return Math.Abs(word1.Rect.Bottom - word2.Rect.Bottom) <= _maxLineGap;
    }

    private RectangleF CalculateLineRect(IEnumerable<TextWord> words)
    {
        var left = words.Min(word => word.Rect.Left);
        var top = words.Min(word => word.Rect.Top);
        var right = words.Max(word => word.Rect.Right);
        var bottom = words.Max(word => word.Rect.Bottom);

        return new RectangleF
        {
            Left = left,
            Top = top,
            Width = right - left,
            Height = bottom - top
        };
    }
}
