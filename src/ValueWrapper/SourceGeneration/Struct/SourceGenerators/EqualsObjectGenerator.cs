using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct.SourceGenerators;

internal sealed class EqualsObjectGenerator : SourceGenerator<EqualsObject>
{
    public override SourceTemplate Generate(EqualsObject node, SourceGeneratorContext context)
    {
        var builder = new SourceTemplateBuilder();

        builder.AddLine(context.Level, "public override bool Equals(object? obj)");
        builder.AddLine(context.Level, "{");
        builder.AddLine(context.Level + 1, $"return obj is {node.StructName} other && Equals(other);");
        builder.AddLine(context.Level, "}");
        
        return builder.Build();
    }
}