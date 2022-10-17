using FluentAssertions;
using ValueWrapper.SourceGeneration;
using ValueWrapper.SourceGeneration.Struct.SourceGenerators;
using ValueWrapper.SourceLayout;
using ValueWrapper.Tests.Unit.TestTools;
using Xunit;

namespace ValueWrapper.Tests.Unit.SourceGeneration.Struct.SourceGenerators;

public sealed class GetHashCodeSourceGeneratorTests
{
    public class Generate
    {
        [Theory]
        [InlineData(0, true, "return Value?.GetHashCode() ?? 0;")]
        [InlineData(1, false, "return Value.GetHashCode();")]
        public void NoChildrenSourceGeneratedCorrectly(
            int level,
            bool valueCanBeNull,
            string expectedStatement) 
        {
            // Arrange.
            var node = new GetHashCode(valueCanBeNull);

            var generator = new GetHashCodeGenerator();

            var ctx = new SourceGeneratorContext(level);

            // Act.
            var source = generator.Generate(node, ctx);

            // Assert.
            source.Lines.Should().HaveCount(4);
            source.Should().ContainLine(0, "public override int GetHashCode()", level);
            source.Should().ContainLine(1, "{", level);
            source.Should().ContainLine(2, expectedStatement, level + 1);
            source.Should().ContainLine(3, "}", level);
        }
    }
}