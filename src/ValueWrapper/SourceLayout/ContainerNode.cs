namespace ValueWrapper.SourceLayout;

internal abstract class ContainerNode : SourceNode
{
    private readonly List<SourceNode> _children = new();

    public IEnumerable<SourceNode> Children => _children.AsReadOnly();

    public void Add(SourceNode child)
    {
        _children.Add(child);   
    }
}