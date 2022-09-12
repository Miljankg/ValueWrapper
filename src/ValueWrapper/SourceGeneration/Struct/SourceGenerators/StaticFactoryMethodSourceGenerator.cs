using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct.SourceGenerators;

internal sealed class StaticFactoryMethodSourceGenerator : SourceGenerator<StaticFactoryMethod>
{
    public override SourceTemplate Generate(StaticFactoryMethod method, SourceGeneratorContext context)
    {
        var builder = new SourceTemplateBuilder();

        builder.AddLine(context.Level, $"public static {method.StructName} {method.MethodName}({method.ValueType} value)");
        builder.AddLine(context.Level, "{");
        builder.AddLine(context.Level + 1, $"return new {method.StructName}(value);");
        builder.AddLine(context.Level, "}");
        
        return builder.Build();
    }
}