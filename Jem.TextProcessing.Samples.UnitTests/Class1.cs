using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jem.TextProcessing.Samples;
using Jem.TextProcessing.Data;

namespace Jem.TextProcessing.Samples.UnitTests;

[TestClass]
public class FontFactoryTests
{
    [TestMethod]
    public void CreateFont_CheckSymbolSizes()
    {
        // Create a font with Arial, 12pt
        var fontName = "Arial";
        var fontSize = 12;
        
        // Create a TextFont with common letters, digits, and punctuation
        var textFont = FontFactory.CreateFont(fontName, fontSize);

        // Validate some symbol sizes
        var symbolA = textFont["A"]!;
        Assert.AreEqual(8.0390625, symbolA.Rect.Width, 1); 
        Assert.AreEqual(8.58984375, symbolA.Rect.Height, 1); 

        var symbol5 = textFont["5"]!;
        Assert.AreEqual(5.6953125, symbol5.Rect.Width, 1); 
        Assert.AreEqual(8.619140625, symbol5.Rect.Height, 1);

        var symbolColon = textFont[":"]!;
        Assert.AreEqual(1.201171875, symbolColon.Rect.Width, 1); 
        Assert.AreEqual(6.22265625, symbolColon.Rect.Height, 1); 
    }
}
