using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct.SourceGenerators;

internal sealed class EqualsGenerator : SourceGenerator<Equals>
{
    public override SourceTemplate Generate(Equals node, SourceGeneratorContext context)
    {
        var builder = new SourceTemplateBuilder();

        builder.AddLine(context.Level, $"public bool Equals({node.StructName} other)");
        builder.AddLine(context.Level, "{");
        builder.AddLine(context.Level + 1, $"return EqualityComparer<{node.ValueTypeName}>.Default.Equals(Value, other.Value);");
        builder.AddLine(context.Level, "}");
        
        return builder.Build();
    }
}