namespace ValueWrapper.SourceGeneration;

internal sealed class SourceTemplate
{
    public static SourceTemplate Empty => new SourceTemplate(Array.Empty<SourceTemplateLine>());
    
    public SourceTemplate(IEnumerable<SourceTemplateLine> lines)
    {
        Lines = lines;
    }

    public IEnumerable<SourceTemplateLine> Lines { get; }
}