using FluentAssertions;
using ValueWrapper.SourceGeneration.Struct;
using Xunit;

namespace ValueWrapper.Tests.Unit.SourceGeneration.Struct.SourceGenerators;

public sealed class StructGeneratorFactoryTests
{
    public class Create
    {
        [Fact]
        public void CreatesCorrectGenerator()
        {
            // Arrange.
            var factory = new StructGeneratorFactory();

            // Act.
            var generator = factory.Create();

            // Assert.
            generator.Should().BeOfType<StructGenerator>();
        }
    }
}