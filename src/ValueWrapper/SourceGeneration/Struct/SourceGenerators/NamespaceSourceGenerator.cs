using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct.SourceGenerators;

internal sealed class NamespaceSourceGenerator : SourceGenerator<Namespace>
{
    public override SourceTemplate Generate(Namespace @namespace, SourceGeneratorContext context)
    {
        var sourceBuilder = new SourceTemplateBuilder();
        
        sourceBuilder.AddLine(context.Level, $"namespace {@namespace.Name}");
        sourceBuilder.AddLine(context.Level, "{");

        AddChildTemplates(context, sourceBuilder);
        
        sourceBuilder.AddLine(context.Level, "}");

        return sourceBuilder.Build();
    }
}