using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace ValueWrapper.Tests.Unit;

[UsedImplicitly]
public class ValueWrapperGeneratorTests
{
    [Fact]
    public void SampleTest()
    {
        // Arrange.
        var generator = new NamespaceCodeGenerator("testNamespace");
        
        // Act.
        var code = generator.Generate();
        
        // Assert.
        code.Should().Be("test");
    }
}