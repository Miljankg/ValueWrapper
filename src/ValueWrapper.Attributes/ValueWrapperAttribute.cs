namespace ValueWrapper.Attributes;

[AttributeUsage(AttributeTargets.Struct)]
public sealed class ValueWrapperAttribute : Attribute
{
    public ValueWrapperAttribute(Type valueType)
    {
        ValueType = valueType;
    }

    public Type ValueType { get; }
}