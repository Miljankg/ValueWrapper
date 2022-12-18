using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct.SourceGenerators;

internal sealed class RootNodeGenerator : SourceGenerator<RootNode>
{
    public override SourceTemplate Generate(RootNode node, SourceGeneratorContext context)
    {
        var sourceBuilder = new SourceTemplateBuilder();

        AddChildTemplates(context, sourceBuilder);

        return sourceBuilder.Build();
    }
}