using ValueWrapper.SourceGeneration;

namespace ValueWrapper.SourceLayout;

internal sealed class NotEqualOperator : SourceNode
{
    public NotEqualOperator(string structName)
    {
        StructName = structName;
    }

    public string StructName { get; }

    public override TResult Accept<TResult, TParams>(ISourceNodeVisitor<TResult, TParams> visitor, TParams @params)
    {
        return visitor.Visit(this, @params);
    }
}