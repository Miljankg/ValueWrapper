using ValueWrapper.SourceGeneration;

namespace ValueWrapper.SourceLayout;

internal sealed class Directive : SourceNode
{
    public static readonly Directive NullableEnable = new("NullableEnable");
    
    public Directive(string name)
    {
        Name = name;
    }

    public string Name { get; }
    
    public override TResult Accept<TResult, TParams>(ISourceNodeVisitor<TResult, TParams> visitor, TParams @params)
        => visitor.Visit(this, @params);
}