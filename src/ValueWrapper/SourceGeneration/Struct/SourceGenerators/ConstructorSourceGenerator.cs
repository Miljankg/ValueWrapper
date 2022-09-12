using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct.SourceGenerators;

internal sealed class ConstructorSourceGenerator : SourceGenerator<Constructor>
{
    public override SourceTemplate Generate(Constructor constructor, SourceGeneratorContext context)
    {
        var builder = new SourceTemplateBuilder();

        builder.AddLine(context.Level, $"public {constructor.StructName}({constructor.ValueType} value)");
        builder.AddLine(context.Level, "{");
        builder.AddLine(context.Level + 1, "Value = value;");
        builder.AddLine(context.Level, "}");
        
        return builder.Build();
    }
}