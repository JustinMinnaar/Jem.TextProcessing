using Jem.TextProcessing.Data;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Jem.TextProcessing.Analyzers;

/// <summary>
/// In this implementation, the SymbolAnalyzer class takes three parameters in its constructor: maxOverlap, maxGap, and maxVerticalShift, which specify the maximum overlap, gap, and vertical shift in units between symbols for them to be considered part of the same word. It has an AnalyzeSymbols method that takes an IEnumerable<TextSymbol> of symbols to analyze. It loops through each symbol and calls the IsNearSymbol method to determine if the symbol is near the previous symbol. If the symbol is near the previous symbol, it adds the symbol to the current word. If the symbol is not near the previous symbol, it creates a new word with the symbol and adds it to the word list.
/// The IsNearSymbol method calculates the overlap, gap, and vertical shift between two symbols and checks if any of these values are less than or equal to their corresponding maximum values.If any value is less than or equal to its corresponding maximum value, it returns true, indicating that the symbols are near each other and should be part of the same word. If none of the values are less than or equal to their corresponding maximum values, it returns false, indicating that the symbols are not near each other and should be part of a new word.
/// </summary>
public class SymbolAnalyzer
{
    private readonly double _maxOverlap;
    private readonly double _maxGap;
    private readonly double _maxVerticalShift;
    public SymbolAnalyzer(double maxOverlap, double maxGap, double maxVerticalShift)
    {
        _maxOverlap = maxOverlap;
        _maxGap = maxGap;
        _maxVerticalShift = maxVerticalShift;
    }

    public List<TextWord> AnalyzeSymbols(IEnumerable<TextSymbol> symbols)
    {
        var wordList = new List<TextWord>();

        TextWord? currentWord = null;

        foreach (var symbol in symbols)
        {
            if (currentWord == null)
            {
                currentWord = new TextWord { Symbols = { symbol } };
                wordList.Add(currentWord);
            }
            else if (IsNearSymbol(symbol, currentWord.Symbols.Last()))
            {
                currentWord.Symbols.Add(symbol);
            }
            else
            {
                currentWord = new TextWord { Symbols = { symbol } };
                wordList.Add(currentWord);
            }
        }

        return wordList;
    }

    private bool IsNearSymbol(TextSymbol symbol1, TextSymbol symbol2)
    {
        var left1 = symbol1.Rect?.Left;
        var left2 = symbol2.Rect?.Left + symbol2.Rect.Width;

        var overlap = Math.Max(0, left2 - left1);
        var gap = Math.Max(0, left1 - left2);
        var verticalShift = Math.Abs(symbol1.BaseLine ?? 0f - symbol2.BaseLine ?? 0f);

        return overlap >= (_maxOverlap * symbol1.Rect.Width)
            || gap <= (_maxGap * symbol1.Rect.Width)
            || verticalShift <= _maxVerticalShift;
    }
}
