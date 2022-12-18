using NSubstitute;
using ValueWrapper.SourceGeneration;
using ValueWrapper.SourceGeneration.Struct.SourceGenerators;
using ValueWrapper.SourceLayout;
using Xunit;

namespace ValueWrapper.Tests.Unit.SourceGeneration.Struct.SourceGenerators;

public sealed partial class StructSourceNodeVisitorTests
{
    public class VisitRootNode
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

            var node = new RootNode();
            
            // Act.
            visitor.Visit(node, level);

            // Assert.
            generators.RootNodeGenerator.Received()
                .Generate(node, Arg.Is<SourceGeneratorContext>(ctx => ctx.Level == level - 1));
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

            var node = new RootNode();

            var child1 = new Structure(name: "testChild1", AccessModifier.Public);
            var child2 = new Structure(name: "testChild2", AccessModifier.Internal);
            var child3 = new Namespace(name: "testChild3");
            
            node.Add(child1);
            node.Add(child2);
            node.Add(child3);
            
            // Act.
            visitor.Visit(node, level);

            // Assert.
            generators.StructGenerator.Received()
                .Generate(child1, Arg.Is<SourceGeneratorContext>(ctx => ctx.Level == level));
            
            generators.StructGenerator.Received()
                .Generate(child2, Arg.Is<SourceGeneratorContext>(ctx => ctx.Level == level));
            
            generators.NamespaceGenerator.Received()
                .Generate(child3, Arg.Is<SourceGeneratorContext>(ctx => ctx.Level == level));
            
            generators.RootNodeGenerator.Received()
                .Generate(node, Arg.Is<SourceGeneratorContext>(ctx => ctx.Level == level - 1));
        }
    }
}