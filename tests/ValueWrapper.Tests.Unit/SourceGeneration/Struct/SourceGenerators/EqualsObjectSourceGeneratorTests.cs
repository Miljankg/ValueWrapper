using FluentAssertions;
using ValueWrapper.SourceGeneration;
using ValueWrapper.SourceGeneration.Struct.SourceGenerators;
using ValueWrapper.SourceLayout;
using ValueWrapper.Tests.Unit.TestTools;
using Xunit;

namespace ValueWrapper.Tests.Unit.SourceGeneration.Struct.SourceGenerators;

public sealed class EqualsObjectSourceGeneratorTests
{
    public class Generate
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void NoChildrenSourceGeneratedCorrectly(int level) 
        {
            // Arrange.
            var node = new EqualsObject(structName: "testStruct");

            var generator = new EqualsObjectGenerator();

            var ctx = new SourceGeneratorContext(level);

            // Act.
            var source = generator.Generate(node, ctx);

            // Assert.
            source.Lines.Should().HaveCount(4);
            source.Should().ContainLine(0, $"public override bool Equals(object? obj)", level);
            source.Should().ContainLine(1, "{", level);
            source.Should().ContainLine(2, $"return obj is {node.StructName} other && Equals(other);", level + 1);
            source.Should().ContainLine(3, "}", level);
        }
    }
}