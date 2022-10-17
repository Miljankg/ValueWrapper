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
            ToStringGenerator = new ToStringSourceGenerator(),
            EqualsGenerator = new EqualsGenerator(),
            EqualsObjectGenerator = new EqualsObjectGenerator(),
            EqualsOperatorGenerator = new EqualsOperatorGenerator(),
            NotEqualOperatorGenerator = new NotEqualOperatorGenerator(),
            GetHashCodeGenerator = new GetHashCodeGenerator()
        };

        return new StructSourceNodeVisitor(generatorConfig);
    }
}