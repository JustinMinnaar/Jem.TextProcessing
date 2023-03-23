namespace Jem.TextProfiling.Data;

public class ProProfile : Pro
{
    public List<ProTemplate> Templates { get; set; } = new();

    public ProTemplate AddTemplate(string name)
    {
        var template = new ProTemplate { Name = name };
        Templates.Add(template);
        return template;
    }
}
