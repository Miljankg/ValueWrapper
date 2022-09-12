using FluentAssertions;
using ValueWrapper.SourceGeneration;
using ValueWrapper.SourceGeneration.Struct.SourceGenerators;
using ValueWrapper.SourceLayout;
using ValueWrapper.Tests.Unit.TestTools;
using Xunit;

namespace ValueWrapper.Tests.Unit.SourceGeneration.Struct.SourceGenerators;

public sealed class ValuePropertySourceGeneratorTests
{
    public class Generate
    {
        [Theory]
        [InlineData(0, "Integer")]
        [InlineData(1, "RandomType")]
        [InlineData(2, "string")]
        public void NoChildrenSourceGeneratedCorrectly(int level, string valueType)
        {
            // Arrange.
            var valueProperty = new ValueProperty(valueType);

            var generator = new ValuePropertySourceGenerator();

            var ctx = new SourceGeneratorContext(level);

            // Act.
            var source = generator.Generate(valueProperty, ctx);

            // Assert.
            source.Lines.Should().HaveCount(1);
            source.Should().ContainLine(0, $"public {valueType} Value {{ get; }}", level);
        }
    }
}