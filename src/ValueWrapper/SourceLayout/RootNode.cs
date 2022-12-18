using ValueWrapper.SourceGeneration;

namespace ValueWrapper.SourceLayout;

internal sealed class RootNode : ContainerNode
{
    public override TResult Accept<TResult, TParams>(ISourceNodeVisitor<TResult, TParams> visitor, TParams @params)
        => visitor.Visit(this, @params);
}