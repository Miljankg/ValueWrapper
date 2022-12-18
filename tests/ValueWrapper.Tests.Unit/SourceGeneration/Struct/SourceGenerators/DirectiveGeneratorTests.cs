using System;
using FluentAssertions;
using ValueWrapper.SourceGeneration;
using ValueWrapper.SourceGeneration.Struct.SourceGenerators;
using ValueWrapper.SourceLayout;
using ValueWrapper.Tests.Unit.TestTools;
using Xunit;

namespace ValueWrapper.Tests.Unit.SourceGeneration.Struct.SourceGenerators;

public sealed class DirectiveGeneratorTests
{
    public class Generate
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void CorrectSourceGeneratedForNullableEnable(int level)
        {
            // Arrange.
            var directive = Directive.NullableEnable;

            var generator = new DirectiveGenerator();

            var ctx = new SourceGeneratorContext(Array.Empty<SourceTemplate>(), level);

            // Act.
            var source = generator.Generate(directive, ctx);

            // Assert.
            source.Should().ContainLine(index: 0, lineContent: "#nullable enable", level);
        }
        
        [Theory]
        [InlineData(0, "test directive1")]
        [InlineData(1, "test directive2")]
        [InlineData(2, "test directive3")]
        public void ExceptionThrownForUnknownDirective(int level, string directiveName)
        {
            // Arrange.
            var directive = new Directive(directiveName);

            var generator = new DirectiveGenerator();

            var ctx = new SourceGeneratorContext(Array.Empty<SourceTemplate>(), level);

            // Act.
            Action func = () => generator.Generate(directive, ctx);

            // Assert.
            func.Should().ThrowExactly<InvalidOperationException>();
        }
    }
}