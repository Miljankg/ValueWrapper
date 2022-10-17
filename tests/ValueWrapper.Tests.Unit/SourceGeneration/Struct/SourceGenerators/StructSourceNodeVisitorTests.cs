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
            EqualsGenerator = Substitute.For<ISourceGenerator<Equals>>(),
            EqualsObjectGenerator = Substitute.For<ISourceGenerator<EqualsObject>>(),
            EqualsOperatorGenerator = Substitute.For<ISourceGenerator<EqualsOperator>>(),
            NotEqualOperatorGenerator = Substitute.For<ISourceGenerator<NotEqualOperator>>(),
            GetHashCodeGenerator = Substitute.For<ISourceGenerator<GetHashCode>>(),
            ToStringGenerator = Substitute.For<ISourceGenerator<ToString>>()
        };
    }
}