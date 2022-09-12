using ValueWrapper.SourceGeneration.Struct.SourceGenerators;

namespace ValueWrapper.SourceGeneration;

internal interface ISourceNodeVisitorFactory
{
    IStructSourceNodeVisitor Create();
}