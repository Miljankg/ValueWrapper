using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct.SourceGenerators;

internal sealed class ValuePropertySourceGenerator : SourceGenerator<ValueProperty>
{
    public override SourceTemplate Generate(ValueProperty node, SourceGeneratorContext context)
    {
        var builder = new SourceTemplateBuilder();

        builder.AddLine(context.Level, $"public {node.ValueType} Value {{ get; }}");
        
        return builder.Build();
    }
}