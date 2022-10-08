using FluentAssertions;
using ValueWrapper.Tests.Integration.TestProject;
using Xunit;

namespace ValueWrapper.Tests.Integration;

public class StaticFactoryMethodTests
{
    public class From
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(99)]
        [InlineData(-1)]
        public void IntegerPassedValueCorrectlySet(int value)
        {
            // Arrange / Act.
            var testValue = TestValue1.From(value);
        
            // Assert.
            testValue.Value.Should().Be(value);
        }   
        
        [Theory]
        [InlineData(1d)]
        [InlineData(2.21d)]
        [InlineData(15d)]
        [InlineData(99.145125142d)]
        [InlineData(-1.123123d)]
        public void DoublePassedValueCorrectlySet(double value)
        {
            // Arrange / Act.
            var testValue = TestValue2.From(value);
        
            // Assert.
            testValue.Value.Should().Be(value);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("test1")]
        [InlineData("test2")]
        [InlineData("!@!%!%@!@%!@#")]
        public void StringPassedValueCorrectlySet(string value)
        {
            // Arrange / Act.
            var testValue = TestValue3.From(value);
        
            // Assert.
            testValue.Value.Should().Be(value);
        }
    }
}