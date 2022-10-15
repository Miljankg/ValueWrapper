using NSubstitute;
using ValueWrapper.SourceGeneration;
using ValueWrapper.SourceGeneration.Struct.SourceGenerators;
using ValueWrapper.SourceLayout;

namespace ValueWrapper.Tests.Unit.SourceGeneration.Struct.SourceGenerators;

public sealed partial class StructSourceNodeVisitorTests
{
    private static StructSourceNodeVisitor.GeneratorConfig GetGenerators()
    {
        return new StructSourceNodeVisitor.GeneratorConfig
        {
            NamespaceGenerator = Substitute.For<ISourceGenerator<Namespace>>(),
            StructGenerator = Substitute.For<ISourceGenerator<Structure>>(),
            FactoryMethodGenerator = Substitute.For<ISourceGenerator<StaticFactoryMethod>>(),
            ConstructorGenerator = Substitute.For<ISourceGenerator<Constructor>>(),
            PropertyGenerator = Substitute.For<ISourceGenerator<ValueProperty>>(),
            ToStringGenerator = Substitute.For<ISourceGenerator<ToString>>()
        };
    }
}