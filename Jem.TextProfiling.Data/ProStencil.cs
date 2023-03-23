using System.Drawing;

namespace Jem.TextProfiling.Data;

/// <summary>
/// The Rect property is a RectangleF that represents the rectangle that encompasses the stencil. The RelativeTo property is a string that represents the ID of the stencil that the current stencil is positioned relative to.
/// </summary>
public abstract class ProStencil : Pro
{
    public RectangleF Rect { get; set; }
    public string? RelativeTo { get; set; }
}
