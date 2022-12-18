using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct.SourceGenerators;

internal class DirectiveGenerator : ISourceGenerator<Directive>
{
    public SourceTemplate Generate(Directive node, SourceGeneratorContext context)
    {
        var builder = new SourceTemplateBuilder();

        if (node.Name == Directive.NullableEnable.Name)
        {
            builder.AddLine(context.Level, "#nullable enable");
            
            return builder.Build();
        }

        throw new InvalidOperationException(message: $"Invalid directive passed '{node.Name}'");
    }
}