using Jem.TextProcessing.Data;

namespace Jem.TextProfiling.Data;

public class ProIdentifier : ProStencil
{
    public List<TextSymbol> Symbols { get; set; } = new();

    public void AddSymbols(IEnumerable<TextSymbol> symbols)
    {
        foreach (var symbol in symbols)
        {
            var copy = symbol.Copy();
            Symbols.Add(copy);
        }
    }

}
