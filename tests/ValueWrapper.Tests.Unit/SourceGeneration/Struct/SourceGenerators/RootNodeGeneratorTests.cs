using System;
using FluentAssertions;
using ValueWrapper.SourceGeneration;
using ValueWrapper.SourceGeneration.Struct.SourceGenerators;
using ValueWrapper.SourceLayout;
using ValueWrapper.Tests.Unit.TestTools;
using Xunit;

namespace ValueWrapper.Tests.Unit.SourceGeneration.Struct.SourceGenerators;

public sealed class RootNodeGeneratorTests
{
    public class Generate
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void NoChildrenSourceGeneratedCorrectly(int level)
        {
            // Arrange.
            var root = new RootNode();

            var generator = new RootNodeGenerator();

            var ctx = new SourceGeneratorContext(Array.Empty<SourceTemplate>(), level);

            // Act.
            var source = generator.Generate(root, ctx);

            // Assert.
            source.Lines.Should().BeEmpty();
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void ChildrenPresentSourceGeneratedCorrectly(int level)
        {
            // Arrange.
            var @namespace = new RootNode();

            var generator = new RootNodeGenerator();
            
            // Child templates.
            var childTemplate1 = SourceTemplateTestBuilder.Instance.WithLines(
                new SourceTemplateLine("TestLine1", 11), 
                new SourceTemplateLine("TestLine2", 12));
            
            var childTemplate2 = SourceTemplateTestBuilder.Instance.WithLines(
                new SourceTemplateLine("TestLine3", 13));
            
            var childTemplate3 = SourceTemplateTestBuilder.Instance.WithLines(
                new SourceTemplateLine("TestLine4", 14), 
                new SourceTemplateLine("TestLine5", 15));

            var ctx = new SourceGeneratorContext(new []
            {
                childTemplate1,
                childTemplate2,
                childTemplate3
            }, level);

            // Act.
            var source = generator.Generate(@namespace, ctx);
            
            // Assert.
            source.Should().ContainLine(0, "TestLine1", 11);
            source.Should().ContainLine(1, "TestLine2", 12);
            source.Should().ContainLine(2, "", 0);
            source.Should().ContainLine(3, "TestLine3", 13);
            source.Should().ContainLine(4, "", 0);
            source.Should().ContainLine(5, "TestLine4", 14);
            source.Should().ContainLine(6, "TestLine5", 15);
        }
    }
}