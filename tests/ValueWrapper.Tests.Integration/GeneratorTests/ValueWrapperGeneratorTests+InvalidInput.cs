using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
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
        public void DifferentAccessModifiersHandledCorrectly()
        {
            // Arrange.
            var source = DefaultStruct.MarkAsNonPartial();
            
            // Act.
            var output = Compile.Source(source);
            
            // Assert.
            output.Should().HaveDiagnostic(Diagnostics.MustBePartial.For(output.GetInputSyntaxNode()));
        }
    }
}