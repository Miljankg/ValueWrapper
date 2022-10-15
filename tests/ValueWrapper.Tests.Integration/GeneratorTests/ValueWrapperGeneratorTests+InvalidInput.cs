using ValueWrapper.Tests.Integration.GeneratorTests.Helpers;
using ValueWrapper.Tests.Integration.GeneratorTests.Helpers.AssertionExtensions;
using VerifyXunit;
using Xunit;

namespace ValueWrapper.Tests.Integration.GeneratorTests;

public partial class ValueWrapperGeneratorTests
{
    [UsesVerify]
    public class InvalidInput
    {
        [Fact]
        public void NonPartialStructProducesError()
        {
            // Arrange.
            var source = DefaultStruct.MarkAsNonPartial();
            
            // Act.
            var output = Compile.Source(source);
            
            // Assert.
            output.Should().HaveDiagnostic(Diagnostics.MustBePartial.For(output.GetInputSyntaxNode()));
        }
        
        [Theory]
        [InlineData("")]
        [InlineData("SomeRandomNonExistingType")]
        public void StructWithInvalidTypeDoesNotProduceError(string valueType)
        {
            // Arrange.
            var source = DefaultStruct.WithValueType(valueType);
            
            // Act.
            var output = Compile.Source(source);
            
            // Assert.
            output.Should().BeCorrectFor(source);
        }
    }
}