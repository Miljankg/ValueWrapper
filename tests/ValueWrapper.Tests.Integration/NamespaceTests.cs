using FluentAssertions;
using ValueWrapper.Tests.Integration.TestProject;
using ValueWrapper.Tests.Integration.TestProject.TestNamespace1;
using ValueWrapper.Tests.Integration.TestProject.TestNamespace1.TestNamespace2;
using Xunit;

namespace ValueWrapper.Tests.Integration;

public sealed class NamespaceTests
{
    [Fact]
    public void DefinedInRootNamespaceCorrect()
    {
        // Arrange.
        var testValue = new RandomValue1();

        // Act.
        var @namespace = testValue.GetType().Namespace;

        // Assert.
        @namespace.Should().Be("ValueWrapper.Tests.Integration.TestProject");
    }
    
    [Fact]
    public void DefinedInSubNamespaceCorrect()
    {
        // Arrange.
        var testValue = new RandomValue2();

        // Act.
        var @namespace = testValue.GetType().Namespace;

        // Assert.
        @namespace.Should().Be("ValueWrapper.Tests.Integration.TestProject.TestNamespace1");
    }
    
    [Fact]
    public void DefinedInSubOfSubNamespaceCorrect()
    {
        // Arrange.
        var testValue = new RandomValue3();

        // Act.
        var @namespace = testValue.GetType().Namespace;

        // Assert.
        @namespace.Should().Be("ValueWrapper.Tests.Integration.TestProject.TestNamespace1.TestNamespace2");
    }
}