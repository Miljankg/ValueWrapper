namespace ValueWrapper.SourceGeneration;

internal static class SourceTemplateBuilderExtensions
{
    public static void AddLine(this SourceTemplateBuilder builder, int level, string line)
    {
        builder.AddLine(new SourceTemplateLine(line, level));
    }
    
    public static void AddLine(this SourceTemplateBuilder builder)
    {
        builder.AddLine(level: 0, line: string.Empty);
    }
}