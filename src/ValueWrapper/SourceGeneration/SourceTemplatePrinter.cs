using System.Text;

namespace ValueWrapper.SourceGeneration;

internal sealed class SourceTemplatePrinter : ISourceTemplatePrinter
{
    public string Print(SourceTemplate template, SourceTemplatePrinterConfig config)
    {
        var sb = new StringBuilder();
        
        foreach (var line in template.Lines)
        {
            var prefix = GeneratePrefix(config, line);

            sb.Append(prefix);
            sb.AppendLine(line.Content);
        }

        return sb.ToString();
    }

    private static string GeneratePrefix(SourceTemplatePrinterConfig config, SourceTemplateLine line) 
        => string.Concat(Enumerable.Repeat(config.IndentationString, line.Level));
}