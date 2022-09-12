using NSubstitute;
using ValueWrapper.SourceGeneration;
using ValueWrapper.SourceGeneration.Struct.SourceGenerators;
using ValueWrapper.SourceLayout;
using Xunit;

namespace ValueWrapper.Tests.Unit.SourceGeneration.Struct.SourceGenerators;

public sealed partial class StructSourceNodeVisitorTests
{
    public class VisitValueProperty
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void VisitInvokesCorrectGenerator(int level)
        {
            // Arrange.
            var generators = GetGenerators();
            
            var visitor = new StructSourceNodeVisitor(generators);

            var node = new ValueProperty(valueType: "test");
            
            // Act.
            visitor.Visit(node, level);

            // Assert.
            generators.PropertyGenerator.Received()
                .Generate(node, Arg.Is<SourceGeneratorContext>(ctx => ctx.Level == level));
        }
    }
}