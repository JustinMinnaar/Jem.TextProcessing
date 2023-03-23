using System.Drawing;

namespace Jem.TextProfiling.Data;

public class ProTemplate : Pro
{
    public List<ProStencil> Stencils { get; set; } = new();

    public ProIdentifier AddIdentifier(string name, RectangleF rect, string? relativeTo = null)
    {
        var identifier = new ProIdentifier { Name = name, Rect = rect, RelativeTo = relativeTo };
        Stencils.Add(identifier);
        return identifier;
    }

    public ProExtractor AddExtractor(string name, RectangleF rect, string? relativeTo = null)
    {
        var extractor = new ProExtractor { Name = name, Rect = rect, RelativeTo = relativeTo };
        Stencils.Add(extractor);
        return extractor;
    }
}
