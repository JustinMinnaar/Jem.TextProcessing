namespace Jem.TextProfiling.Data;

public class Pro
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }

    public override string? ToString()
    {
        return Name;
    }
}
