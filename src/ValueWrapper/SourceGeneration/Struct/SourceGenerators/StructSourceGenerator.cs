using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct.SourceGenerators;

internal sealed class StructSourceGenerator : SourceGenerator<Structure>
{
    public override SourceTemplate Generate(Structure @struct, SourceGeneratorContext context)
    {
        var sourceBuilder = new SourceTemplateBuilder();
        
        sourceBuilder.AddLine(context.Level, GenerateStructDefinition(@struct));
        sourceBuilder.AddLine(context.Level, "{");
        
        AddChildTemplates(context, sourceBuilder);
        
        sourceBuilder.AddLine(context.Level, "}");

        return sourceBuilder.Build();
    }

    private static string GenerateStructDefinition(Structure @struct)
    {
        return $"{GetAccessModifierString(@struct.AccessModifier)} readonly partial struct {@struct.Name} " +
               $": IEquatable<{@struct.Name}>";
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