using FluentAssertions;
using ValueWrapper.Tests.Integration.TestProject;
using Xunit;

namespace ValueWrapper.Tests.Integration;

public class Test
{
    [Fact]
    public void Test1()
    {
        // Arrange.
        var value = TestStruct.From();
        
        // Act.

        // Assert.
        value.Should().Be(null);
    }
}