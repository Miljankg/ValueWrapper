using ValueWrapper.SourceGeneration;

namespace ValueWrapper.SourceLayout;

internal sealed class Equals : SourceNode
{
    public Equals(string structName, string valueTypeName)
    {
        StructName = structName;
        ValueTypeName = valueTypeName;
    }

    public string ValueTypeName { get; set; }

    public string StructName { get; }

    public override TResult Accept<TResult, TParams>(ISourceNodeVisitor<TResult, TParams> visitor, TParams @params)
    {
        return visitor.Visit(this, @params);
    }
}