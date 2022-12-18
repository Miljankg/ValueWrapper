using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct.SourceGenerators;

internal sealed class StructSourceNodeVisitor : IStructSourceNodeVisitor
{
    private readonly ISourceGenerator<Directive> _directiveGenerator;
    private readonly ISourceGenerator<Namespace> _namespaceGenerator;
    private readonly ISourceGenerator<Structure> _structGenerator;
    private readonly ISourceGenerator<Constructor> _constructorGenerator;
    private readonly ISourceGenerator<StaticFactoryMethod> _factoryMethodGenerator;
    private readonly ISourceGenerator<ValueProperty> _propertyGenerator;
    private readonly ISourceGenerator<ToString> _toStringGenerator;
    private readonly ISourceGenerator<GetHashCode> _getHashCodeGenerator;
    private readonly ISourceGenerator<Equals> _equalsGenerator;
    private readonly ISourceGenerator<EqualsObject> _equalsObjectGenerator;
    private readonly ISourceGenerator<EqualsOperator> _equalsOperatorGenerator;
    private readonly ISourceGenerator<NotEqualOperator> _notEqualOperatorGenerator;
    private readonly ISourceGenerator<RootNode> _rootNodeGenerator;

    public StructSourceNodeVisitor(GeneratorConfig generatorConfig)
    {
        _rootNodeGenerator = generatorConfig.RootNodeGenerator;
        _directiveGenerator = generatorConfig.DirectiveGenerator;
        _namespaceGenerator = generatorConfig.NamespaceGenerator;
        _structGenerator = generatorConfig.StructGenerator;
        _constructorGenerator = generatorConfig.ConstructorGenerator;
        _factoryMethodGenerator = generatorConfig.FactoryMethodGenerator;
        _propertyGenerator = generatorConfig.PropertyGenerator;
        _toStringGenerator = generatorConfig.ToStringGenerator;
        _getHashCodeGenerator = generatorConfig.GetHashCodeGenerator;
        _equalsGenerator = generatorConfig.EqualsGenerator;
        _equalsObjectGenerator = generatorConfig.EqualsObjectGenerator;
        _equalsOperatorGenerator = generatorConfig.EqualsOperatorGenerator;
        _notEqualOperatorGenerator = generatorConfig.NotEqualOperatorGenerator;
    }

    public SourceTemplate Visit(RootNode root, int level)
    {
        // Intentionally reduce level as RootNode has no content, so there is no need for indentation.
        // TODO: This concern should maybe be addressed within the generator itself.
        level -= 1;
        
        var childSourceTemplates = GenerateChildSourceTemplates(root, level);

        var generatorCtx = new SourceGeneratorContext(childSourceTemplates, level);

        return _rootNodeGenerator.Generate(root, generatorCtx);
    }

    public SourceTemplate Visit(Directive directive, int level)
    {
        var generatorCtx = new SourceGeneratorContext(level);

        return _directiveGenerator.Generate(directive, generatorCtx);
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

    public SourceTemplate Visit(ToString toString, int level)
    {
        var generatorCtx = new SourceGeneratorContext(level);

        return _toStringGenerator.Generate(toString, generatorCtx);
    }

    public SourceTemplate Visit(GetHashCode getHashCode, int level)
    {
        var generatorCtx = new SourceGeneratorContext(level);
        
        return _getHashCodeGenerator.Generate(getHashCode, generatorCtx);
    }

    public SourceTemplate Visit(Equals equals, int level)
    {
        var generatorCtx = new SourceGeneratorContext(level);

        return _equalsGenerator.Generate(equals, generatorCtx);
    }

    public SourceTemplate Visit(EqualsObject equalsObject, int level)
    {
        var generatorCtx = new SourceGeneratorContext(level);

        return _equalsObjectGenerator.Generate(equalsObject, generatorCtx);
    }

    public SourceTemplate Visit(EqualsOperator equalsOperator, int level)
    {
        var generatorCtx = new SourceGeneratorContext(level);

        return _equalsOperatorGenerator.Generate(equalsOperator, generatorCtx);
    }

    public SourceTemplate Visit(NotEqualOperator notEqualOperator, int level)
    {
        var generatorCtx = new SourceGeneratorContext(level);

        return _notEqualOperatorGenerator.Generate(notEqualOperator, generatorCtx);
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
        public ISourceGenerator<RootNode> RootNodeGenerator { get; init; } = null!;
        
        public ISourceGenerator<Directive> DirectiveGenerator { get; init; } = null!;

        public ISourceGenerator<Namespace> NamespaceGenerator { get; init; } = null!;

        public ISourceGenerator<Structure> StructGenerator { get; init; } = null!;

        public ISourceGenerator<StaticFactoryMethod> FactoryMethodGenerator { get; init; } = null!;

        public ISourceGenerator<Constructor> ConstructorGenerator { get; init; } = null!;

        public ISourceGenerator<ValueProperty> PropertyGenerator { get; init; } = null!;

        public ISourceGenerator<ToString> ToStringGenerator { get; init; } = null!;

        public ISourceGenerator<GetHashCode> GetHashCodeGenerator { get; init; } = null!;

        public ISourceGenerator<Equals> EqualsGenerator { get; init; } = null!;

        public ISourceGenerator<EqualsObject> EqualsObjectGenerator { get; init; } = null!;

        public ISourceGenerator<EqualsOperator> EqualsOperatorGenerator { get; init; } = null!;

        public ISourceGenerator<NotEqualOperator> NotEqualOperatorGenerator { get; init; } = null!;
    }
}