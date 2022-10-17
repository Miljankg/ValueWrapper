using FluentAssertions;
using ValueWrapper.Tests.Integration.TestProject;
using Xunit;

// Intentional, for testing purposes.
// ReSharper disable EqualExpressionComparison
// ReSharper disable SuspiciousTypeConversion.Global

// Comparison made to same variable
#pragma warning disable CS1718 

namespace ValueWrapper.Tests.Integration.GeneratedStructTests;

public partial class EqualityTests
{
    public class Integer
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public void EqualsIsTrueWhenValuesAndTypesAreSame(int value)
        {
            // Arrange.
            var x = TestValue1.From(value);
            var y = TestValue1.From(value);

            // Act / Assert.
            x.Equals(y).Should().BeTrue();
            y.Equals(x).Should().BeTrue();
            x.Equals(x).Should().BeTrue();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public void EqualsIsTrueWhenObjectsAreSame(int value)
        {
            // Arrange.
            object x = TestValue1.From(value);
            object y = TestValue1.From(value);

            // Act / Assert.
            x.Equals(y).Should().BeTrue();
            y.Equals(x).Should().BeTrue();
            x.Equals(x).Should().BeTrue();
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, -3)]
        [InlineData(0, 4)]
        [InlineData(-1, 5)]
        [InlineData(-5, -4)]
        public void EqualsIsFalseWhenValuesAreDifferent(int value1, int value2)
        {
            // Arrange.
            object x = TestValue1.From(value1);
            object y = TestValue1.From(value2);

            // Act / Assert.
            x.Equals(y).Should().BeFalse();
            y.Equals(x).Should().BeFalse();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public void EqualsIsFalseWhenTypesAreDifferent(int value)
        {
            // Arrange.
            var x = TestValue1.From(value);
            var y = RandomValue1.From(value);

            // Act / Assert.
            x.Equals(y).Should().BeFalse();
            y.Equals(x).Should().BeFalse();
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, -3)]
        [InlineData(0, 4)]
        [InlineData(-1, 5)]
        [InlineData(-5, -4)]
        public void EqualsIsFalseWhenValuesAndTypesAreDifferent(int value1, int value2)
        {
            // Arrange.
            var x = TestValue1.From(value1);
            var y = RandomValue1.From(value2);

            // Act / Assert.
            x.Equals(y).Should().BeFalse();
            y.Equals(x).Should().BeFalse();
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public void EqualsOperatorIsTrueWhenValuesAndTypesAreSame(int value)
        {
            // Arrange.
            var x = TestValue1.From(value);
            var y = TestValue1.From(value);

            // Act / Assert.
            (x == y).Should().BeTrue();
            (y == x).Should().BeTrue();
            (x == x).Should().BeTrue();
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public void NotEqualsOperatorIsFalseWhenValuesAndTypesAreSame(int value)
        {
            // Arrange.
            var x = TestValue1.From(value);
            var y = TestValue1.From(value);

            // Act / Assert.
            (x != y).Should().BeFalse();
            (y != x).Should().BeFalse();
            (x != x).Should().BeFalse();
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, -3)]
        [InlineData(0, 4)]
        [InlineData(-1, 5)]
        [InlineData(-5, -4)]
        public void EqualsOperatorIsFalseWhenValuesAreDifferent(int value1, int value2)
        {
            // Arrange.
            object x = TestValue1.From(value1);
            object y = TestValue1.From(value2);

            // Act / Assert.
            (x == y).Should().BeFalse();
            (y == x).Should().BeFalse();
        }
        
        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, -3)]
        [InlineData(0, 4)]
        [InlineData(-1, 5)]
        [InlineData(-5, -4)]
        public void NotEqualsOperatorIsTrueWhenValuesAreDifferent(int value1, int value2)
        {
            // Arrange.
            object x = TestValue1.From(value1);
            object y = TestValue1.From(value2);

            // Act / Assert.
            (x != y).Should().BeTrue();
            (y != x).Should().BeTrue();
        }
    }
}