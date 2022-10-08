using FluentAssertions;
using ValueWrapper.Tests.Integration.TestProject;
using Xunit;

namespace ValueWrapper.Tests.Integration.GeneratedStructTests;

public class ConstructorTests
{
    public class Constructor
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
            var testValue = new TestValue1(value);
        
            // Assert.
            testValue.Value.Should().Be(value);
        }   
        
        [Fact]
        public void DefaultUsedDefaultIntegerSet()
        {
            // Arrange / Act.
            var testValue = default(TestValue1);
        
            // Assert.
            testValue.Value.Should().Be(0);
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
            var testValue = new TestValue2(value);
        
            // Assert.
            testValue.Value.Should().Be(value);
        }
        
        [Fact]
        public void DefaultUsedDefaultDoubleSet()
        {
            // Arrange / Act.
            var testValue = default(TestValue2);
        
            // Assert.
            testValue.Value.Should().Be(0);
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
            var testValue = new TestValue3(value);
        
            // Assert.
            testValue.Value.Should().Be(value);
        }
        
        [Fact]
        public void DefaultUsedDefaultStringValueSet()
        {
            // Arrange / Act.
            var testValue = default(TestValue3);
        
            // Assert.
            testValue.Value.Should().BeNull();
        }
    }
}