using FluentAssertions;
using ValueWrapper.Tests.Integration.TestProject;
using ValueWrapper.Tests.Integration.TestProject.TestNamespace1;
using Xunit;

// Intentional, for testing purposes.
// ReSharper disable EqualExpressionComparison
// ReSharper disable SuspiciousTypeConversion.Global

// Comparison made to same variable
#pragma warning disable CS1718

namespace ValueWrapper.Tests.Integration.GeneratedStructTests;

public partial class EqualityTests
{
    public class Double
    {
        [Theory]
        [InlineData(1.1d)]
        [InlineData(2d)]
        [InlineData(0.1d)]
        [InlineData(-1d)]
        [InlineData(-5.51525d)]
        public void EqualsIsTrueWhenValuesAndTypesAreSame(double value)
        {
            // Arrange.
            var x = TestValue2.From(value);
            var y = TestValue2.From(value);

            // Act / Assert.
            x.Equals(y).Should().BeTrue();
            y.Equals(x).Should().BeTrue();
            x.Equals(x).Should().BeTrue();
        }

        [Theory]
        [InlineData(1.1d)]
        [InlineData(2d)]
        [InlineData(0.1d)]
        [InlineData(-1d)]
        [InlineData(-5.51525d)]
        public void EqualsIsTrueWhenObjectsAreSame(double value)
        {
            // Arrange.
            object x = TestValue2.From(value);
            object y = TestValue2.From(value);

            // Act / Assert.
            x.Equals(y).Should().BeTrue();
            y.Equals(x).Should().BeTrue();
            x.Equals(x).Should().BeTrue();
        }

        [Theory]
        [InlineData(1.1d, 2d)]
        [InlineData(2d, -3.1151251d)]
        [InlineData(0.1d, 4.12414d)]
        [InlineData(-1d, 5.1)]
        [InlineData(-5.51525d, -4d)]
        public void EqualsIsFalseWhenValuesAreDifferent(double value1, double value2)
        {
            // Arrange.
            object x = TestValue2.From(value1);
            object y = TestValue2.From(value2);

            // Act / Assert.
            x.Equals(y).Should().BeFalse();
            y.Equals(x).Should().BeFalse();
        }

        [Theory]
        [InlineData(1.1d)]
        [InlineData(2d)]
        [InlineData(0.1d)]
        [InlineData(-1d)]
        [InlineData(-5.51525d)]
        public void EqualsIsFalseWhenTypesAreDifferent(double value)
        {
            // Arrange.
            var x = TestValue2.From(value);
            var y = RandomValue2.From(value);

            // Act / Assert.
            x.Equals(y).Should().BeFalse();
            y.Equals(x).Should().BeFalse();
        }

        [Theory]
        [InlineData(1.1d, 2d)]
        [InlineData(2d, -3.1151251d)]
        [InlineData(0.1d, 4.12414d)]
        [InlineData(-1d, 5.1)]
        [InlineData(-5.51525d, -4d)]
        public void EqualsIsFalseWhenValuesAndTypesAreDifferent(double value1, double value2)
        {
            // Arrange.
            var x = TestValue2.From(value1);
            var y = RandomValue2.From(value2);

            // Act / Assert.
            x.Equals(y).Should().BeFalse();
            y.Equals(x).Should().BeFalse();
        }
        
        [Theory]
        [InlineData(1.1d)]
        [InlineData(2d)]
        [InlineData(0.1d)]
        [InlineData(-1d)]
        [InlineData(-5.51525d)]
        public void EqualsOperatorIsTrueWhenValuesAndTypesAreSame(double value)
        {
            // Arrange.
            var x = TestValue2.From(value);
            var y = TestValue2.From(value);

            // Act / Assert.
            (x == y).Should().BeTrue();
            (y == x).Should().BeTrue();
            (x == x).Should().BeTrue();
        }
        
        [Theory]
        [InlineData(1.1d)]
        [InlineData(2d)]
        [InlineData(0.1d)]
        [InlineData(-1d)]
        [InlineData(-5.51525d)]
        public void NotEqualsOperatorIsFalseWhenValuesAndTypesAreSame(double value)
        {
            // Arrange.
            var x = TestValue2.From(value);
            var y = TestValue2.From(value);

            // Act / Assert.
            (x != y).Should().BeFalse();
            (y != x).Should().BeFalse();
            (x != x).Should().BeFalse();
        }

        [Theory]
        [InlineData(1.1d, 2d)]
        [InlineData(2d, -3.1151251d)]
        [InlineData(0.1d, 4.12414d)]
        [InlineData(-1d, 5.1)]
        [InlineData(-5.51525d, -4d)]
        public void EqualsOperatorIsFalseWhenValuesAreDifferent(double value1, double value2)
        {
            // Arrange.
            object x = TestValue2.From(value1);
            object y = TestValue2.From(value2);

            // Act / Assert.
            (x == y).Should().BeFalse();
            (y == x).Should().BeFalse();
        }
        
        [Theory]
        [InlineData(1.1d, 2d)]
        [InlineData(2d, -3.1151251d)]
        [InlineData(0.1d, 4.12414d)]
        [InlineData(-1d, 5.1)]
        [InlineData(-5.51525d, -4d)]
        public void NotEqualsOperatorIsTrueWhenValuesAreDifferent(double value1, double value2)
        {
            // Arrange.
            object x = TestValue2.From(value1);
            object y = TestValue2.From(value2);

            // Act / Assert.
            (x != y).Should().BeTrue();
            (y != x).Should().BeTrue();
        }
    }
}