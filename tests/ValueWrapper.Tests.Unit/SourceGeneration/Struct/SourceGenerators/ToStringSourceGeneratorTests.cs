using FluentAssertions;
using ValueWrapper.SourceGeneration;
using ValueWrapper.SourceGeneration.Struct.SourceGenerators;
using ValueWrapper.SourceLayout;
using ValueWrapper.Tests.Unit.TestTools;
using Xunit;

namespace ValueWrapper.Tests.Unit.SourceGeneration.Struct.SourceGenerators;

public sealed class ToStringSourceGeneratorTests
{
    public class Generate
    {
        [Theory]
        [InlineData(0, true, "return Value?.ToString() ?? \"<NULL>\";")]
        [InlineData(1, false, "return Value.ToString();")]
        public void NoChildrenSourceGeneratedCorrectly(
            int level, 
            bool isNullable, 
            string expectedToStringStatement)
        {
            // Arrange.
            var valueProperty = new ToString(isNullable);

            var generator = new ToStringSourceGenerator();

            var ctx = new SourceGeneratorContext(level);

            // Act.
            var source = generator.Generate(valueProperty, ctx);

            // Assert.
            source.Lines.Should().HaveCount(4);
            source.Should().ContainLine(0, "public override string ToString()", level);
            source.Should().ContainLine(1, "{", level);
            source.Should().ContainLine(2, expectedToStringStatement, level + 1);
            source.Should().ContainLine(3, "}", level);
        }
    }
}