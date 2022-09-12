using FluentAssertions;
using ValueWrapper.SourceGeneration;
using ValueWrapper.SourceGeneration.Struct.SourceGenerators;
using ValueWrapper.SourceLayout;
using ValueWrapper.Tests.Unit.TestTools;
using Xunit;

namespace ValueWrapper.Tests.Unit.SourceGeneration.Struct.SourceGenerators;

public sealed class ConstructorSourceGeneratorTests
{
    public class Generate
    {
        [Theory]
        [InlineData(0, "SomeStruct", "Integer")]
        [InlineData(1, "Test", "RandomType")]
        [InlineData(2, "Hey", "string")]
        public void NoChildrenSourceGeneratedCorrectly(int level, string structName, string valueType)
        {
            // Arrange.
            var constructor = new Constructor(structName, valueType);

            var generator = new ConstructorSourceGenerator();

            var ctx = new SourceGeneratorContext(level);

            // Act.
            var source = generator.Generate(constructor, ctx);

            // Assert.
            source.Lines.Should().HaveCount(4);
            source.Should().ContainLine(0, $"public {structName}({valueType} value)", level);
            source.Should().ContainLine(1, "{", level);
            source.Should().ContainLine(2, "Value = value;", level + 1);
            source.Should().ContainLine(3, "}", level);
        }
    }
}