using FluentAssertions;
using ValueWrapper.SourceGeneration;
using ValueWrapper.Tests.Unit.TestTools;
using Xunit;

namespace ValueWrapper.Tests.Unit.SourceGeneration;

public sealed class SourceTemplatePrinterTests
{
    public class Printer
    {
        [Theory]
        [InlineData("  ")]
        [InlineData("    ")]
        [InlineData("xxx")]
        public void PassedTemplatePrintedCorrectly(string indentation)
        {
            // Arrange.
            var sourceTemplate = new SourceTemplateTestBuilder()
                .WithLines(
                    new SourceTemplateLine("TestLine1", 0),
                    new SourceTemplateLine("TestLine2", 5),
                    new SourceTemplateLine("TestLine3", 2),
                    new SourceTemplateLine("TestLine4", 3)
                );
            
            var printer = new SourceTemplatePrinter();

            var config = new SourceTemplatePrinterConfig
            {
                IndentationString = indentation
            };

            // Act.
            var source = printer.Print(sourceTemplate, config);

            // Assert.
            source.Should().Be(
@$"TestLine1
{indentation}{indentation}{indentation}{indentation}{indentation}TestLine2
{indentation}{indentation}TestLine3
{indentation}{indentation}{indentation}TestLine4
"
            );
        }
    }
}