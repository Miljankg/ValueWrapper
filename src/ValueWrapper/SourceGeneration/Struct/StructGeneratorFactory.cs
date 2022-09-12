using ValueWrapper.SourceGeneration.Struct.SourceGenerators;

namespace ValueWrapper.SourceGeneration.Struct;

internal sealed class StructGeneratorFactory
{
    public StructGenerator Create()
    {
        var layoutGenerator = new StructLayoutGenerator();
        var visitor = new StructSourceNodeVisitorFactory().Create();
        var printer = new SourceTemplatePrinter();
            
        return new StructGenerator(layoutGenerator, visitor, printer);
    }
}