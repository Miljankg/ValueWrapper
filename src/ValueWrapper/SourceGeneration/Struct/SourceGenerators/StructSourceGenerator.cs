using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct.SourceGenerators;

internal sealed class StructSourceGenerator : SourceGenerator<Structure>
{
    public override SourceTemplate Generate(Structure @struct, SourceGeneratorContext context)
    {
        var sourceBuilder = new SourceTemplateBuilder();
        
        sourceBuilder.AddLine(context.Level, $"{GetAccessModifierString(@struct.AccessModifier)} readonly partial struct {@struct.Name}");
        sourceBuilder.AddLine(context.Level, "{");
        
        AddChildTemplates(context, sourceBuilder);
        
        sourceBuilder.AddLine(context.Level, "}");

        return sourceBuilder.Build();
    }

    private static string GetAccessModifierString(AccessModifier accessModifier)
    {
        return accessModifier switch
        {
            AccessModifier.Public => "public",
            AccessModifier.Internal => "internal",
            AccessModifier.Private => "private",
            _ => throw new NotSupportedException($"Access Modifier '{accessModifier}' is not supported.")
        };
    }
}