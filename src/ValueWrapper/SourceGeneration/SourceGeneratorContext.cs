namespace ValueWrapper.SourceGeneration;

internal sealed class SourceGeneratorContext
{
    public SourceGeneratorContext(int level)
        : this(Array.Empty<SourceTemplate>(), level)
    {
    }
    
    public SourceGeneratorContext(IEnumerable<SourceTemplate> childSourceTemplates, int level)
    {
        ChildSourceTemplates = childSourceTemplates;
        Level = level;
    }

    public IEnumerable<SourceTemplate> ChildSourceTemplates { get; }
    
    public int Level { get; }
}