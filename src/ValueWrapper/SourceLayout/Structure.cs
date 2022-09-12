using ValueWrapper.SourceGeneration;

namespace ValueWrapper.SourceLayout;

internal sealed class Structure : ContainerNode
{
    public Structure(string name, AccessModifier accessModifier)
    {
        Name = name;
        AccessModifier = accessModifier;
    }

    public string Name { get; }
    
    public AccessModifier AccessModifier { get; }
    
    public override TResult Accept<TResult, TParams>(ISourceNodeVisitor<TResult, TParams> visitor, TParams @params)
        => visitor.Visit(this, @params);
}