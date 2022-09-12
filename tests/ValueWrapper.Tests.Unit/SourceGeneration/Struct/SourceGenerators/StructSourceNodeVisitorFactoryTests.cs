using FluentAssertions;
using ValueWrapper.SourceGeneration.Struct.SourceGenerators;
using Xunit;

namespace ValueWrapper.Tests.Unit.SourceGeneration.Struct.SourceGenerators;

public sealed class StructSourceNodeVisitorFactoryTests
{
    public class Create
    {
        [Fact]
        public void CreatesCorrectVisitor()
        {
            // Arrange.
            var factory = new StructSourceNodeVisitorFactory();

            // Act.
            var visitor = factory.Create();

            // Assert.
            visitor.Should().BeOfType<StructSourceNodeVisitor>();
        }
    }
}