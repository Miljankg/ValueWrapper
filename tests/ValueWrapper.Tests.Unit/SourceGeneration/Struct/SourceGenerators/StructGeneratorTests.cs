using NSubstitute;
using ValueWrapper.SourceGeneration;
using ValueWrapper.SourceGeneration.Struct;
using ValueWrapper.SourceGeneration.Struct.SourceGenerators;
using ValueWrapper.SourceLayout;
using ValueWrapper.Tests.Unit.TestTools;
using Xunit;

namespace ValueWrapper.Tests.Unit.SourceGeneration.Struct.SourceGenerators;

public sealed class StructGeneratorTests
{
    public class Generate
    {
        [Theory]
        [InlineData("TestNamespace1", "TestStruct1", "TestValueType1", AccessModifierTestFactory.PublicAsString)]
        [InlineData("TestNamespace2", "TestStruct2", "TestValueType2", AccessModifierTestFactory.InternalAsString)]
        [InlineData("TestNamespace3", "TestStruct3", "TestValueType3", AccessModifierTestFactory.PrivateAsString)]
        public void InvokesLayoutGeneratorCorrectly(
            string namespaceName, 
            string structName, 
            string valueTypeName, 
            string accessModifier)
        {
            // Arrange.
            var layoutGenerator = Substitute.For<IStructLayoutGenerator>();
            var visitor = Substitute.For<IStructSourceNodeVisitor>();
            var printer = Substitute.For<ISourceTemplatePrinter>();
            
            var generator = new StructGenerator(layoutGenerator, visitor, printer);

            var config = new StructGenerator.Config
            {
                NamespaceName = namespaceName,
                StructName = structName,
                ValueTypeInfo =  TestType.ValueType(valueTypeName).ToInfo(),
                IndentationString = "TestString",
                AccessModifier = AccessModifierTestFactory.FromString(accessModifier)
            };
            
            // Act.
            generator.Generate(config);

            // Assert.
            layoutGenerator.Received().GenerateLayout(
                Arg.Is<StructLayoutConfig>(c =>
                    c.NamespaceName == config.NamespaceName &&
                    c.StructName == config.StructName &&
                    c.ValueTypeInfo == config.ValueTypeInfo &&
                    c.AccessModifier == config.AccessModifier));
        }
        
        [Fact]
        public void InvokesVisitorCorrectly()
        {
            // Arrange.
            var layoutGenerator = Substitute.For<IStructLayoutGenerator>();
            var visitor = Substitute.For<IStructSourceNodeVisitor>();
            var printer = Substitute.For<ISourceTemplatePrinter>();

            // Set up Layout Generator.
            var namespaceNode = new RootNode();

            layoutGenerator.GenerateLayout(Arg.Any<StructLayoutConfig>()).Returns(namespaceNode);
            
            var generator = new StructGenerator(layoutGenerator, visitor, printer);

            var config = new StructGenerator.Config
            {
                NamespaceName = "TestNamespace",
                StructName = "TestStructName",
                ValueTypeInfo = TestType.ValueType("TestValueTypeName").ToInfo(),
                IndentationString = "TestString",
                AccessModifier = AccessModifier.Public
            };
            
            // Act.
            generator.Generate(config);

            // Assert.
            visitor.Received().Visit(Arg.Is(namespaceNode), Arg.Is(0));
        }
        
        [Theory]
        [InlineData("IndentationString1")]
        [InlineData("IndentationString2")]
        [InlineData("IndentationString3")]
        public void InvokesPrinterCorrectly(string indentationString)
        {
            // Arrange.
            var layoutGenerator = Substitute.For<IStructLayoutGenerator>();
            var visitor = Substitute.For<IStructSourceNodeVisitor>();
            var printer = Substitute.For<ISourceTemplatePrinter>();

            // Set up Layout Generator.
            var namespaceNode = new RootNode();

            layoutGenerator.GenerateLayout(Arg.Any<StructLayoutConfig>()).Returns(namespaceNode);
            
            // Set up Visitor.
            var sourceTemplate = SourceTemplate.Empty;
            var initialLevel = 0;
            
            visitor.Visit(namespaceNode, initialLevel).Returns(sourceTemplate);
            
            var generator = new StructGenerator(layoutGenerator, visitor, printer);

            var config = new StructGenerator.Config
            {
                NamespaceName = "TestNamespace",
                StructName = "TestStructName",
                ValueTypeInfo = TestType.ValueType("TestValueTypeName").ToInfo(),
                IndentationString = indentationString,
                AccessModifier = AccessModifier.Public
            };
            
            // Act.
            generator.Generate(config);

            // Assert.
            printer.Received().Print(
                Arg.Is(sourceTemplate), 
                Arg.Is<SourceTemplatePrinterConfig>(c => c.IndentationString == config.IndentationString));
        }
    }
}