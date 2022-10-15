using System.Globalization;
using FluentAssertions;
using ValueWrapper.Tests.Integration.TestProject;
using Xunit;

namespace ValueWrapper.Tests.Integration.GeneratedStructTests;

public class ToStringTests
{
    [Theory]
    [InlineData(1d)]
    [InlineData(2.21d)]
    [InlineData(15d)]
    [InlineData(99.145125142d)]
    [InlineData(-1.123123d)]
    public void StringIsCorrectWhenCreatedWithDouble(double value)
    {
        // Arrange.
        var testValue = TestValue2.From(value);
        
        // Act.
        var str = testValue.ToString();
        
        // Assert.
        str.Should().Be(value.ToString(CultureInfo.InvariantCulture));
    }
    
    [Fact]
    public void StringIsCorrectWhenCreatedWithDefaultDouble()
    {
        // Arrange.
        var testValue = default(TestValue2);
        
        // Act.
        var str = testValue.ToString();
        
        // Assert.
        str.Should().Be("0");
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(99)]
    [InlineData(-1)]
    public void StringIsCorrectWhenCreatedWithInt(int value)
    {
        // Arrange.
        var testValue = TestValue1.From(value);
        
        // Act.
        var str = testValue.ToString();
        
        // Assert.
        str.Should().Be(value.ToString(CultureInfo.InvariantCulture));
    }
    
    [Fact]
    public void StringIsCorrectWhenCreatedWithDefaultInt()
    {
        // Arrange.
        var testValue = default(TestValue1);
        
        // Act.
        var str = testValue.ToString();
        
        // Assert.
        str.Should().Be("0");
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("test1")]
    [InlineData("test2")]
    [InlineData("!@!%!%@!@%!@#")]
    public void StringIsCorrectWhenCreatedWithString(string value)
    {
        // Arrange.
        var testValue = new TestValue3(value);
        
        // Act.
        var str = testValue.ToString();
        
        // Assert.
        str.Should().Be(value);
    }
        
    [Fact]
    public void StringIsCorrectWhenCreatedWithDefaultString()
    {
        // Arrange.
        var testValue = default(TestValue3);
        
        // Act.
        var str = testValue.ToString();
        
        // Assert.
        str.Should().Be("<NULL>");
    }
}