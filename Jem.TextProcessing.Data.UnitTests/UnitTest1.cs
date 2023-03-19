namespace Jem.TextProcessing.Data.UnitTests;

[TestClass]
public class TextEntityTests
{
    [TestMethod]
    public void CreateTextEntities_CheckProperties()
    {
        // Create a single Page with a Block, Line, Word, and Symbol
        var symbol = new Symbol()
        {
            Text = "A",
            Rect = new TextRect() { Left = 100, Top = 100, Width = 10, Height = 10 },
            BaseLine = 90,
            Color = 0xFF0000 // red color
        };

        var word = new Word()
        {
            Text = "Apple",
            Rect = new TextRect() { Left = 50, Top = 80, Width = 50, Height = 10 },
            BaseLine = 70,
            Color = 0x00FF00, // green color
            Symbols = new List<Symbol>() { symbol }
        };

        var line = new Line()
        {
            Text = "The quick brown fox jumps over the lazy dog.",
            Rect = new TextRect() { Left = 10, Top = 50, Width = 500, Height = 10 },
            BaseLine = 40,
            Color = 0x0000FF, // blue color
            Words = new List<Word>() { word }
        };

        var block = new Block()
        {
            Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
            Rect = new TextRect() { Left = 0, Top = 0, Width = 500, Height = 100 },
            BaseLine = 30,
            Color = 0x000000, // black color
            Lines = new List<Line>() { line }
        };

        var page = new Page()
        {
            Blocks = new List<Block>() { block }
        };

        // Test some properties of the entities
        Assert.AreEqual("A", symbol.Text);
        Assert.AreEqual(100, symbol.Rect.Left);
        Assert.AreEqual(70, word.BaseLine);
        Assert.AreEqual(1, line.Words.Count);
        Assert.AreEqual("Apple", line.Words[0].Text);
        Assert.AreEqual(1, block.Lines.Count);
        Assert.AreEqual(40, block.Lines[0].BaseLine);
        Assert.AreEqual(1, page.Blocks.Count);
        Assert.AreEqual(0, page.Blocks[0].Rect.Top);
    }
}

