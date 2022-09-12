using ValueWrapper.SourceGeneration;

namespace ValueWrapper.SourceLayout;

internal sealed class Constructor : SourceNode
{
    public Constructor(string structName, string valueType)
    {
        StructName = structName;
        ValueType = valueType;
    }

    public string StructName { get; }
    
    public string ValueType { get; }
    
    public override TResult Accept<TResult, TParams>(ISourceNodeVisitor<TResult, TParams> visitor, TParams @params)
        => visitor.Visit(this, @params);
}