using ValueWrapper.SourceGeneration.Struct;

namespace ValueWrapper.Tests.Unit.TestTools;

public sealed class TestType
{
    public static TestType ValueType(string name)
    {
        return new TestType(name, isValueType: true, canBeNull: false);
    }

    public static TestType NullableValueType(string name)
    {
        return new TestType(name, isValueType: true, canBeNull: true);
    }
    
    public static TestType ReferenceType(string name)
    {
        return new TestType(name, isValueType: false, canBeNull: true);
    }
    
    public TestType(string name, bool isValueType, bool canBeNull)
    {
        Name = name;
        IsValueType = isValueType;
        CanBeNull = canBeNull;
    }

    public string Name { get; }
    
    public bool IsValueType { get; }
    
    public bool CanBeNull { get; }

    internal ValueTypeInfo ToInfo()
    {
        return new ValueTypeInfo(Name, IsValueType, CanBeNull);
    }
}