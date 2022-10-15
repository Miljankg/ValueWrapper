using ValueWrapper.SourceGeneration;

namespace ValueWrapper.SourceLayout;

internal sealed class ToString : SourceNode
{
    public ToString(bool isNullable)
    {
        IsNullable = isNullable;
    }

    public bool IsNullable { get; }
    
    public override TResult Accept<TResult, TParams>(ISourceNodeVisitor<TResult, TParams> visitor, TParams @params)
        => visitor.Visit(this, @params);
}