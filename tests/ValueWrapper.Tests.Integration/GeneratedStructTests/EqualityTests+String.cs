using FluentAssertions;
using ValueWrapper.Tests.Integration.TestProject;
using ValueWrapper.Tests.Integration.TestProject.TestNamespace1.TestNamespace2;
using Xunit;

// Intentional, for testing purposes.
// ReSharper disable EqualExpressionComparison
// ReSharper disable SuspiciousTypeConversion.Global

// Comparison made to same variable
#pragma warning disable CS1718

namespace ValueWrapper.Tests.Integration.GeneratedStructTests;

public partial class EqualityTests
{
    public class String
    {
        [Theory]
        [InlineData("test1")]
        [InlineData("test2")]
        [InlineData("test3")]
        [InlineData("")]
        [InlineData(null)]
        public void EqualsIsTrueWhenValuesAndTypesAreSame(string value)
        {
            // Arrange.
            var x = TestValue3.From(value);
            var y = TestValue3.From(value);

            // Act / Assert.
            x.Equals(y).Should().BeTrue();
            y.Equals(x).Should().BeTrue();
            x.Equals(x).Should().BeTrue();
        }

        [Theory]
        [InlineData("test1")]
        [InlineData("test2")]
        [InlineData("test3")]
        [InlineData("")]
        [InlineData(null)]
        public void EqualsIsTrueWhenObjectsAreSame(string value)
        {
            // Arrange.
            object x = TestValue3.From(value);
            object y = TestValue3.From(value);

            // Act / Assert.
            x.Equals(y).Should().BeTrue();
            y.Equals(x).Should().BeTrue();
            x.Equals(x).Should().BeTrue();
        }

        [Theory]
        [InlineData("test1", "something")]
        [InlineData("test2", "somethingElse")]
        [InlineData("test3", "")]
        [InlineData("test4", null)]
        [InlineData("", "test5")]
        [InlineData(null, "test6")]
        public void EqualsIsFalseWhenValuesAreDifferent(string value1, string value2)
        {
            // Arrange.
            object x = TestValue3.From(value1);
            object y = TestValue3.From(value2);

            // Act / Assert.
            x.Equals(y).Should().BeFalse();
            y.Equals(x).Should().BeFalse();
        }

        [Theory]
        [InlineData("test1")]
        [InlineData("test2")]
        [InlineData("test3")]
        [InlineData("")]
        [InlineData(null)]
        public void EqualsIsFalseWhenTypesAreDifferent(string value)
        {
            // Arrange.
            var x = TestValue3.From(value);
            var y = RandomValue3.From(value);

            // Act / Assert.
            x.Equals(y).Should().BeFalse();
            y.Equals(x).Should().BeFalse();
        }

        [Theory]
        [InlineData("test1", "something")]
        [InlineData("test2", "somethingElse")]
        [InlineData("test3", "")]
        [InlineData("test4", null)]
        [InlineData("", "test5")]
        [InlineData(null, "test6")]
        public void EqualsIsFalseWhenValuesAndTypesAreDifferent(string value1, string value2)
        {
            // Arrange.
            var x = TestValue3.From(value1);
            var y = RandomValue3.From(value2);

            // Act / Assert.
            x.Equals(y).Should().BeFalse();
            y.Equals(x).Should().BeFalse();
        }
        
        [Theory]
        [InlineData("test1")]
        [InlineData("test2")]
        [InlineData("test3")]
        [InlineData("")]
        [InlineData(null)]
        public void EqualsOperatorIsTrueWhenValuesAndTypesAreSame(string value)
        {
            // Arrange.
            var x = TestValue3.From(value);
            var y = TestValue3.From(value);

            // Act / Assert.
            (x == y).Should().BeTrue();
            (y == x).Should().BeTrue();
            (x == x).Should().BeTrue();
        }
        
        [Theory]
        [InlineData("test1")]
        [InlineData("test2")]
        [InlineData("test3")]
        [InlineData("")]
        [InlineData(null)]
        public void NotEqualsOperatorIsFalseWhenValuesAndTypesAreSame(string value)
        {
            // Arrange.
            var x = TestValue3.From(value);
            var y = TestValue3.From(value);

            // Act / Assert.
            (x != y).Should().BeFalse();
            (y != x).Should().BeFalse();
            (x != x).Should().BeFalse();
        }

        [Theory]
        [InlineData("test1", "something")]
        [InlineData("test2", "somethingElse")]
        [InlineData("test3", "")]
        [InlineData("test4", null)]
        [InlineData("", "test5")]
        [InlineData(null, "test6")]
        public void EqualsOperatorIsFalseWhenValuesAreDifferent(string value1, string value2)
        {
            // Arrange.
            object x = TestValue3.From(value1);
            object y = TestValue3.From(value2);

            // Act / Assert.
            (x == y).Should().BeFalse();
            (y == x).Should().BeFalse();
        }
        
        [Theory]
        [InlineData("test1", "something")]
        [InlineData("test2", "somethingElse")]
        [InlineData("test3", "")]
        [InlineData("test4", null)]
        [InlineData("", "test5")]
        [InlineData(null, "test6")]
        public void NotEqualsOperatorIsTrueWhenValuesAreDifferent(string value1, string value2)
        {
            // Arrange.
            object x = TestValue3.From(value1);
            object y = TestValue3.From(value2);

            // Act / Assert.
            (x != y).Should().BeTrue();
            (y != x).Should().BeTrue();
        }
    }
}