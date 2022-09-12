using NSubstitute;
using ValueWrapper.SourceGeneration;
using ValueWrapper.SourceGeneration.Struct.SourceGenerators;
using ValueWrapper.SourceLayout;
using Xunit;

namespace ValueWrapper.Tests.Unit.SourceGeneration.Struct.SourceGenerators;

public sealed partial class StructSourceNodeVisitorTests
{
    public class VisitStruct
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void VisitWithoutChildrenInvokesCorrectGenerator(int level)
        {
            // Arrange.
            var generators = GetGenerators();
            
            var visitor = new StructSourceNodeVisitor(generators);

            var node = new Structure(name: "test", AccessModifier.Public);
            
            // Act.
            visitor.Visit(node, level);

            // Assert.
            generators.StructGenerator.Received()
                .Generate(node, Arg.Is<SourceGeneratorContext>(ctx => ctx.Level == level));
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void VisitWithChildrenInvokesCorrectGenerator(int level)
        {
            // Arrange.
            var generators = GetGenerators();
            
            var visitor = new StructSourceNodeVisitor(generators);

            var node = new Structure(name: "test", AccessModifier.Internal);

            var child1 = new Constructor(structName: "test", valueType: "test");
            var child2 = new StaticFactoryMethod(structName: "test", methodName: "test", valueType: "test");
            var child3 = new ValueProperty(valueType: "test");

            node.Add(child1);
            node.Add(child2);
            node.Add(child3);
            
            // Act.
            visitor.Visit(node, level);

            // Assert.
            generators.ConstructorGenerator.Received()
                .Generate(child1, Arg.Is<SourceGeneratorContext>(ctx => ctx.Level == level + 1));
            
            generators.FactoryMethodGenerator.Received()
                .Generate(child2, Arg.Is<SourceGeneratorContext>(ctx => ctx.Level == level + 1));
            
            generators.PropertyGenerator.Received()
                .Generate(child3, Arg.Is<SourceGeneratorContext>(ctx => ctx.Level == level + 1));
            
            generators.StructGenerator.Received()
                .Generate(node, Arg.Is<SourceGeneratorContext>(ctx => ctx.Level == level));
        }
    }
}