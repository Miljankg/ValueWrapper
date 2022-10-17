using ValueWrapper.SourceGeneration;

namespace ValueWrapper.SourceLayout;

internal sealed class EqualsOperator : SourceNode
{
    public EqualsOperator(string structName)
    {
        StructName = structName;
    }

    public string StructName { get; }

    public override TResult Accept<TResult, TParams>(ISourceNodeVisitor<TResult, TParams> visitor, TParams @params)
    {
        return visitor.Visit(this, @params);
    }
}