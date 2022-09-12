namespace ValueWrapper.SourceGeneration;

internal sealed class SourceTemplateBuilder
{
    private readonly List<SourceTemplateLine> _lines = new();
    
    public void AddLine(SourceTemplateLine line)
    {
        _lines.Add(line);    
    }

    public void AddTemplate(SourceTemplate template)
    {
        foreach (var line in template.Lines)
        {
            AddLine(line);
        }
    }

    public SourceTemplate Build()
    {
        return new SourceTemplate(_lines.AsReadOnly());
    }
}