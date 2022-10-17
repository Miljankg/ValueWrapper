using ValueWrapper.SourceGeneration;

namespace ValueWrapper.SourceLayout;

internal sealed class GetHashCode : SourceNode
{
    public GetHashCode(bool valueCanBeNull)
    {
        ValueCanBeNull = valueCanBeNull;
    }

    public bool ValueCanBeNull { get; }

    public override TResult Accept<TResult, TParams>(ISourceNodeVisitor<TResult, TParams> visitor, TParams @params)
    {
        return visitor.Visit(this, @params);
    }
}