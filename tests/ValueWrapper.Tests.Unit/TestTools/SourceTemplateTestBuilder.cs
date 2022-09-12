using ValueWrapper.SourceGeneration;

namespace ValueWrapper.Tests.Unit.TestTools;

internal sealed class SourceTemplateTestBuilder
{
    public static readonly SourceTemplateTestBuilder Instance = new();

    public SourceTemplate WithLines(params SourceTemplateLine[] lines)
    {
        return new SourceTemplate(lines);
    }
}