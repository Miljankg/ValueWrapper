using ValueWrapper.SourceGeneration;

namespace ValueWrapper.SourceLayout;

internal sealed class ValueProperty : SourceNode
{
    public ValueProperty(string valueType)
    {
        ValueType = valueType;
    }

    public string ValueType { get; }
    
    public override TResult Accept<TResult, TParams>(ISourceNodeVisitor<TResult, TParams> visitor, TParams @params)
        => visitor.Visit(this, @params);
}