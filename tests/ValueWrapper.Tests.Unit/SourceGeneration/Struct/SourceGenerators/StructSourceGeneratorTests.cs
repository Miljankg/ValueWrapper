using System;
using FluentAssertions;
using ValueWrapper.SourceGeneration;
using ValueWrapper.SourceGeneration.Struct.SourceGenerators;
using ValueWrapper.SourceLayout;
using ValueWrapper.Tests.Unit.TestTools;
using Xunit;

namespace ValueWrapper.Tests.Unit.SourceGeneration.Struct.SourceGenerators;

public sealed class StructSourceGeneratorTests
{
    public class Generate
    {
        [Theory]
        [InlineData(0, "TestStruct", AccessModifierTestFactory.PublicAsString)]
        [InlineData(1, "RandomStruct", AccessModifierTestFactory.InternalAsString)]
        [InlineData(2, "Struct", AccessModifierTestFactory.PrivateAsString)]
        public void NoChildrenSourceGeneratedCorrectly(int level, string structName, string accessModifier)
        {
            // Arrange.
            var @struct = new Structure(structName, AccessModifierTestFactory.FromString(accessModifier));

            var generator = new StructSourceGenerator();

            var ctx = new SourceGeneratorContext(Array.Empty<SourceTemplate>(), level);

            // Act.
            var source = generator.Generate(@struct, ctx);

            // Assert.
            source.Lines.Should().HaveCount(3);
            source.Should().ContainLine(0, $"{accessModifier} readonly partial struct {structName} : IEquatable<{structName}>", level);
            source.Should().ContainLine(1, "{", level);
            source.Should().ContainLine(2, "}", level);
        }
        
        [Theory]
        [InlineData(0, "TestStruct", "public")]
        [InlineData(1, "RandomStruct", "internal")]
        [InlineData(2, "Struct", "private")]
        public void ChildrenPresentSourceGeneratedCorrectly(int level, string structName, string accessModifier)
        {
            // Arrange.
            var @struct = new Structure(structName, AccessModifierTestFactory.FromString(accessModifier));

            var generator = new StructSourceGenerator();
            
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
            var source = generator.Generate(@struct, ctx);
            
            // Assert.
            source.Should().ContainLine(0, $"{accessModifier} readonly partial struct {structName} : IEquatable<{structName}>", level);
            source.Should().ContainLine(1, "{", level);
            source.Should().ContainLine(2, "TestLine1", 11);
            source.Should().ContainLine(3, "TestLine2", 12);
            source.Should().ContainLine(4, "", 0);
            source.Should().ContainLine(5, "TestLine3", 13);
            source.Should().ContainLine(6, "", 0);
            source.Should().ContainLine(7, "TestLine4", 14);
            source.Should().ContainLine(8, "TestLine5", 15);
            source.Should().ContainLine(9, "}", level);
        }
    }
}