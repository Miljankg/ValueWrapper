using FluentAssertions;
using ValueWrapper.Tests.Integration.TestProject;
using Xunit;

namespace ValueWrapper.Tests.Integration.GeneratedStructTests;

public class GetHashCodeTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-99)]
    public void IntegerHashCodeIsCorrectWhenCreatedFromValue(int value)
    {
        // Act.
        var testValue = TestValue1.From(value);
        
        // Assert.
        var hashCode = testValue.GetHashCode();
        
        // Arrange.
        hashCode.Should().Be(value.GetHashCode());
    }
    
    [Fact] 
    public void IntegerHashCodeIsCorrectWhenCreatedWithoutValue()
    {
        // Act.
        var testValue = default(TestValue1);
        
        // Assert.
        var hashCode = testValue.GetHashCode();
        
        // Arrange.
        hashCode.Should().Be(0);
    }
    
    [Theory]
    [InlineData(1.1d)]
    [InlineData(2.123d)]
    [InlineData(3d)]
    [InlineData(0d)]
    [InlineData(-1.125)]
    [InlineData(-99.0001)]
    public void DoubleHashCodeIsCorrectWhenCreatedFromValue(double value)
    {
        // Act.
        var testValue = TestValue2.From(value);
        
        // Assert.
        var hashCode = testValue.GetHashCode();
        
        // Arrange.
        hashCode.Should().Be(value.GetHashCode());
    }
    
    [Fact] 
    public void DoubleHashCodeIsCorrectWhenCreatedWithoutValue()
    {
        // Act.
        var testValue = default(TestValue2);
        
        // Assert.
        var hashCode = testValue.GetHashCode();
        
        // Arrange.
        hashCode.Should().Be(0);
    }
    
    [Theory]
    [InlineData("test1")]
    [InlineData("test2")]
    [InlineData("test3")]
    [InlineData("")]
    public void StringHashCodeIsCorrectWhenCreatedFromValue(string value)
    {
        // Act.
        var testValue = TestValue3.From(value);
        
        // Assert.
        var hashCode = testValue.GetHashCode();
        
        // Arrange.
        hashCode.Should().Be(value.GetHashCode());
    }
    
    [Fact] 
    public void StringHashCodeIsCorrectWhenCreatedWithoutValue()
    {
        // Act.
        var testValue = default(TestValue3);
        
        // Assert.
        var hashCode = testValue.GetHashCode();
        
        // Arrange.
        hashCode.Should().Be(0);
    }
}