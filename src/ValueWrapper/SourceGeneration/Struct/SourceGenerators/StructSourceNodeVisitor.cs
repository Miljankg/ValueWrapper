using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct.SourceGenerators;

internal sealed class StructSourceNodeVisitor : IStructSourceNodeVisitor
{
    private readonly ISourceGenerator<Namespace> _namespaceGenerator;
    private readonly ISourceGenerator<Structure> _structGenerator;
    private readonly ISourceGenerator<Constructor> _constructorGenerator;
    private readonly ISourceGenerator<StaticFactoryMethod> _factoryMethodGenerator;
    private readonly ISourceGenerator<ValueProperty> _propertyGenerator;

    public StructSourceNodeVisitor(GeneratorConfig generatorConfig)
    {
        _namespaceGenerator = generatorConfig.NamespaceGenerator;
        _structGenerator = generatorConfig.StructGenerator;
        _constructorGenerator = generatorConfig.ConstructorGenerator;
        _factoryMethodGenerator = generatorConfig.FactoryMethodGenerator;
        _propertyGenerator = generatorConfig.PropertyGenerator;
    }
    
    public SourceTemplate Visit(Namespace @namespace, int level)
    {
        var childSourceTemplates = GenerateChildSourceTemplates(@namespace, level);

        var generatorCtx = new SourceGeneratorContext(childSourceTemplates, level);

        return _namespaceGenerator.Generate(@namespace, generatorCtx);
    }

    public SourceTemplate Visit(Structure @struct, int level)
    {
        var childSourceTemplates = GenerateChildSourceTemplates(@struct, level);

        var generatorCtx = new SourceGeneratorContext(childSourceTemplates, level);

        return _structGenerator.Generate(@struct, generatorCtx);
    }

    public SourceTemplate Visit(Constructor constructor, int level)
    {
        var generatorCtx = new SourceGeneratorContext(level);

        return _constructorGenerator.Generate(constructor, generatorCtx);
    }

    public SourceTemplate Visit(StaticFactoryMethod staticFactoryMethod, int level)
    {
        var generatorCtx = new SourceGeneratorContext(level);
        
        return _factoryMethodGenerator.Generate(staticFactoryMethod, generatorCtx);
    }

    public SourceTemplate Visit(ValueProperty valueProperty, int level)
    {
        var generatorCtx = new SourceGeneratorContext(level);
        
        return _propertyGenerator.Generate(valueProperty, generatorCtx);
    }
    
    private IEnumerable<SourceTemplate> GenerateChildSourceTemplates(ContainerNode node, int currentLevel)
    {
        return node
            .Children
            .Select(child => child.Accept(this, currentLevel + 1))
            .ToList();
    }

    public sealed class GeneratorConfig
    {
        public ISourceGenerator<Namespace> NamespaceGenerator { get; init; } = null!;

        public ISourceGenerator<Structure> StructGenerator { get; init; } = null!;

        public ISourceGenerator<StaticFactoryMethod> FactoryMethodGenerator { get; init; } = null!;

        public ISourceGenerator<Constructor> ConstructorGenerator { get; init; } = null!;

        public ISourceGenerator<ValueProperty> PropertyGenerator { get; init; } = null!;
    }
}