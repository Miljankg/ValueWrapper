using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct;

internal sealed class StructLayoutConfig
{
    public string NamespaceName { get; init; } = null!;

    public string StructName { get; init; } = null!;
        
    public ValueTypeInfo ValueTypeInfo { get; init; } = null!;
    
    public string FactoryMethodName { get; init; } = null!;
        
    public AccessModifier AccessModifier { get; init; }
}