using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration;

internal interface ISourceGenerator<in TSourceNode>
    where TSourceNode : SourceNode
{
    SourceTemplate Generate(TSourceNode node, SourceGeneratorContext context);
}