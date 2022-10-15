namespace ValueWrapper.SourceGeneration.Struct.SourceGenerators;

internal sealed class StructSourceNodeVisitorFactory : ISourceNodeVisitorFactory
{
    public IStructSourceNodeVisitor Create()
    {
        var generatorConfig = new StructSourceNodeVisitor.GeneratorConfig
        {
            NamespaceGenerator = new NamespaceSourceGenerator(),
            StructGenerator = new StructSourceGenerator(),
            FactoryMethodGenerator = new StaticFactoryMethodSourceGenerator(),
            ConstructorGenerator = new ConstructorSourceGenerator(),
            PropertyGenerator = new ValuePropertySourceGenerator(),
            ToStringGenerator = new ToStringSourceGenerator()
        };

        return new StructSourceNodeVisitor(generatorConfig);
    }
}