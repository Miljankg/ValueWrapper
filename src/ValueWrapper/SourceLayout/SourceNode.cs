using ValueWrapper.SourceGeneration;

namespace ValueWrapper.SourceLayout;

internal abstract class SourceNode
{
    public abstract TResult Accept<TResult, TParams>(ISourceNodeVisitor<TResult, TParams> visitor, TParams @params);
}