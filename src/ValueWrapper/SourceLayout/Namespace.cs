using ValueWrapper.SourceGeneration;

namespace ValueWrapper.SourceLayout;

internal sealed class Namespace : ContainerNode
{
    public Namespace(string name)
    {
        Name = name;
    }

    public string Name { get; }
    
    public override TResult Accept<TResult, TParams>(ISourceNodeVisitor<TResult, TParams> visitor, TParams @params)
        => visitor.Visit(this, @params);
}