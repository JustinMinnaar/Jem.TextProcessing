using Jem.TextProcessing.Data;

namespace Jem.TextProcessing.Analyzers;

public class LineAnalyzer
{
    private readonly double _proximityThreshold;

    public LineAnalyzer(double proximityThreshold)
    {
        _proximityThreshold = proximityThreshold;
    }

    public List<TextBlock> AnalyzeLines(IEnumerable<TextLine> lines)
    {
        var blockList = new List<TextBlock>();

        foreach (var line in lines)
        {
            var nearestBlock = FindNearestBlock(line, blockList);

            if (nearestBlock != null)
            {
                nearestBlock.Lines.Add(line);
            }
            else
            {
                blockList.Add(new TextBlock { Lines = { line } });
            }
        }

        return blockList;
    }

    private TextBlock? FindNearestBlock(TextLine line, List<TextBlock> blockList)
    {
        foreach (var block in blockList)
        {
            if (IsNearBlock(line, block))
            {
                return block;
            }
        }

        return null;
    }

    private bool IsNearBlock(TextLine line, TextBlock block)
    {
        foreach (var blockLine in block.Lines)
        {
            if (IsNearLine(line, blockLine))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsNearLine(TextLine line1, TextLine line2)
    {
        var top1 = Math.Min(line1.Rect.Top, line1.Rect.Top + line1.Rect.Height);
        var bottom1 = Math.Max(line1.Rect.Top, line1.Rect.Top + line1.Rect.Height);
        var top2 = Math.Min(line2.Rect.Top, line2.Rect.Top + line2.Rect.Height);
        var bottom2 = Math.Max(line2.Rect.Top, line2.Rect.Top + line2.Rect.Height);

        var topDistance = Math.Abs(top1 - top2);
        var bottomDistance = Math.Abs(bottom1 - bottom2);
        var verticalDistance = Math.Min(topDistance, bottomDistance);

        var horizontalDistance = Math.Abs(line1.Rect.Left - line2.Rect.Left);

        return verticalDistance <= _proximityThreshold && horizontalDistance <= _proximityThreshold;
    }
}
