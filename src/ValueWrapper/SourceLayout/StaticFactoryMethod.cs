using ValueWrapper.SourceGeneration;

namespace ValueWrapper.SourceLayout;

internal sealed class StaticFactoryMethod : SourceNode
{
    public StaticFactoryMethod(string structName, string methodName, string valueType)
    {
        StructName = structName;
        ValueType = valueType;
        MethodName = methodName;
    }

    public string StructName { get; }

    public string MethodName { get; }
    
    public string ValueType { get; }
    
    public override TResult Accept<TResult, TParams>(ISourceNodeVisitor<TResult, TParams> visitor, TParams @params)
        => visitor.Visit(this, @params);
}