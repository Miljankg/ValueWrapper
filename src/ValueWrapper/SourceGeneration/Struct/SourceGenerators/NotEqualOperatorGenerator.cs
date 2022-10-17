using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct.SourceGenerators;

internal sealed class NotEqualOperatorGenerator : SourceGenerator<NotEqualOperator>
{
    public override SourceTemplate Generate(NotEqualOperator node, SourceGeneratorContext context)
    {
        var builder = new SourceTemplateBuilder();

        builder.AddLine(context.Level, $"public static bool operator !=({node.StructName} left, {node.StructName} right)");
        builder.AddLine(context.Level, "{");
        builder.AddLine(context.Level + 1, $"return !left.Equals(right);");
        builder.AddLine(context.Level, "}");
        
        return builder.Build();
    }
}