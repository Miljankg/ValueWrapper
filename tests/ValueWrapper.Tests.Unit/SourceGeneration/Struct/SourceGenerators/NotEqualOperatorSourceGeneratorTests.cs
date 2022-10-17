using FluentAssertions;
using ValueWrapper.SourceGeneration;
using ValueWrapper.SourceGeneration.Struct.SourceGenerators;
using ValueWrapper.SourceLayout;
using ValueWrapper.Tests.Unit.TestTools;
using Xunit;

namespace ValueWrapper.Tests.Unit.SourceGeneration.Struct.SourceGenerators;

public sealed class NotEqualOperatorSourceGeneratorTests
{
    public class Generate
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void NoChildrenSourceGeneratedCorrectly(int level) 
        {
            // Arrange.
            var node = new NotEqualOperator(structName: "testStruct");

            var generator = new NotEqualOperatorGenerator();

            var ctx = new SourceGeneratorContext(level);

            // Act.
            var source = generator.Generate(node, ctx);

            // Assert.
            source.Lines.Should().HaveCount(4);
            source.Should().ContainLine(0, $"public static bool operator !=({node.StructName} left, {node.StructName} right)", level);
            source.Should().ContainLine(1, "{", level);
            source.Should().ContainLine(2, "return !left.Equals(right);", level + 1);
            source.Should().ContainLine(3, "}", level);
        }
    }
}