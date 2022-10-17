using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct.SourceGenerators;

internal sealed class GetHashCodeGenerator : SourceGenerator<GetHashCode>
{
    public override SourceTemplate Generate(GetHashCode node, SourceGeneratorContext context)
    {
        var builder = new SourceTemplateBuilder();

        builder.AddLine(context.Level, "public override int GetHashCode()");
        builder.AddLine(context.Level, "{");
        builder.AddLine(context.Level + 1, GenerateHashCodeStatement(node.ValueCanBeNull));
        builder.AddLine(context.Level, "}");
        
        return builder.Build();
    }

    private static string GenerateHashCodeStatement(bool valueCanBeNull)
    {
        return valueCanBeNull 
            ? "return Value?.GetHashCode() ?? 0;" 
            : "return Value.GetHashCode();";
    }
}