using ValueWrapper.SourceGeneration.Struct.SourceGenerators;
using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct;

internal sealed class StructGenerator
{
    private readonly IStructLayoutGenerator _layoutGenerator;
    private readonly IStructSourceNodeVisitor _visitor;
    private readonly ISourceTemplatePrinter _printer;

    public StructGenerator(
        IStructLayoutGenerator layoutGenerator,
        IStructSourceNodeVisitor visitor,
        ISourceTemplatePrinter printer)
    {
        _layoutGenerator = layoutGenerator;
        _visitor = visitor;
        _printer = printer;
    }
    
    public string Generate(Config config)
    {
        var sourceNode = AssembleStructLayout(config);

        var sourceTemplate = GenerateSourceTemplate(sourceNode);

        var source = PrintSource(sourceTemplate, config);

        return source;
    }

    private RootNode AssembleStructLayout(Config config)
    {
        var layoutConfig = new StructLayoutConfig
        {
            NamespaceName = config.NamespaceName,
            StructName = config.StructName,
            ValueTypeInfo = config.ValueTypeInfo,
            FactoryMethodName = "From",
            AccessModifier = config.AccessModifier
        };

        return _layoutGenerator.GenerateLayout(layoutConfig);
    }

    private SourceTemplate GenerateSourceTemplate(RootNode node)
    {
        const int initialLevel = 0;
        
        return _visitor.Visit(node, initialLevel);
    }

    private string PrintSource(SourceTemplate sourceTemplate, Config config)
    {
        var printerConfig = new SourceTemplatePrinterConfig
        {
            IndentationString = config.IndentationString
        };

        return _printer.Print(sourceTemplate, printerConfig);
    }

    public sealed class Config
    {
        public string NamespaceName { get; init; } = null!;

        public string StructName { get; init; } = null!;
        
        public ValueTypeInfo ValueTypeInfo { get; init; } = null!;
        
        public string IndentationString { get; init; } = null!;
        
        public AccessModifier AccessModifier { get; init; }
    }
}