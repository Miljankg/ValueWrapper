namespace ValueWrapper.SourceGeneration.Struct;

internal sealed class ValueTypeInfo
{
    public ValueTypeInfo(string typeName, bool isValueType, bool canBeNull)
    {
        TypeName = typeName;
        IsValueType = isValueType;
        CanBeNull = canBeNull;
    }

    public string TypeName { get; }

    public bool IsValueType { get; }

    public bool CanBeNull { get; }
}