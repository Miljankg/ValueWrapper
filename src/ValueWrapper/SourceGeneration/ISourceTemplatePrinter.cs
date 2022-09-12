namespace ValueWrapper.SourceGeneration;

internal interface ISourceTemplatePrinter
{
    string Print(SourceTemplate template, SourceTemplatePrinterConfig config);
}