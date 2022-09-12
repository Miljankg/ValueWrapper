namespace ValueWrapper.SourceGeneration;

internal sealed class SourceTemplateLine
{
    public SourceTemplateLine(string content, int level)
    {
        Content = content;
        Level = level;
    }

    public string Content { get; }
    
    public int Level { get; }
}