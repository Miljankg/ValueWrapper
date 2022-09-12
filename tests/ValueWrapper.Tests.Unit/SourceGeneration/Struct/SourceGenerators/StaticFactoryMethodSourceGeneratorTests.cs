using FluentAssertions;
using ValueWrapper.SourceGeneration;
using ValueWrapper.SourceGeneration.Struct.SourceGenerators;
using ValueWrapper.SourceLayout;
using ValueWrapper.Tests.Unit.TestTools;
using Xunit;

namespace ValueWrapper.Tests.Unit.SourceGeneration.Struct.SourceGenerators;

public sealed class StaticFactoryMethodGeneratorTests
{
    public class Generate
    {
        [Theory]
        [InlineData(0, "SomeStruct", "Integer", "From")]
        [InlineData(1, "Test", "RandomType", "Create")]
        [InlineData(2, "Hey", "string", "Make")]
        public void NoChildrenSourceGeneratedCorrectly(int level, string structName, string valueType, string methodName)
        {
            // Arrange.
            var method = new StaticFactoryMethod(structName, methodName, valueType);

            var generator = new StaticFactoryMethodSourceGenerator();

            var ctx = new SourceGeneratorContext(level);

            // Act.
            var source = generator.Generate(method, ctx);

            // Assert.
            source.Lines.Should().HaveCount(4);
            source.Should().ContainLine(0, $"public static {structName} {methodName}({valueType} value)", level);
            source.Should().ContainLine(1, "{", level);
            source.Should().ContainLine(2, $"return new {structName}(value);", level + 1);
            source.Should().ContainLine(3, "}", level);
        }
    }
}