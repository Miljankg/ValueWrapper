using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration;

internal abstract class SourceGenerator<TSourceNode> : ISourceGenerator<TSourceNode>
    where TSourceNode : SourceNode
{
    public abstract SourceTemplate Generate(TSourceNode node, SourceGeneratorContext context);

    protected static void AddChildTemplates(SourceGeneratorContext context, SourceTemplateBuilder sourceBuilder)
    {
        var childSourceTemplates = context.ChildSourceTemplates.ToList();
        
        for (var i = 0; i < childSourceTemplates.Count; i++)
        {
            sourceBuilder.AddTemplate(childSourceTemplates[i]);
            
            // Skip appending new line if it is the last child item.
            if (i >= childSourceTemplates.Count - 1) continue;
            
            sourceBuilder.AddLine();
        }
    }
}