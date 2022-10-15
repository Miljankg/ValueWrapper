using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct.SourceGenerators;

internal sealed class ToStringSourceGenerator : SourceGenerator<ToString>
{
    public override SourceTemplate Generate(ToString node, SourceGeneratorContext context)
    {
        var builder = new SourceTemplateBuilder();

        var toStringStatement = node.IsNullable
            ? "return Value?.ToString() ?? \"<NULL>\";"
            : "return Value.ToString();";

        builder.AddLine(context.Level, "public override string ToString()");
        builder.AddLine(context.Level, "{");
        builder.AddLine(context.Level + 1, toStringStatement);
        builder.AddLine(context.Level, "}");
        
        return builder.Build();
    }
}