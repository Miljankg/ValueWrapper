using System.Text.RegularExpressions;

namespace ValueWrapper.SourceGeneration;

internal sealed class SourceFileNameGenerator
{
    private const string InvalidCharReplacement = "_";
    
    private static readonly Regex RemoveInvalidChars = new(
        pattern: $"[{Regex.Escape(new string(Path.GetInvalidFileNameChars()))}]",
        options: RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.CultureInvariant);
    
    public string GenerateFileName(string @namespace, string typeName)
    {
        var originalFileName = $@"{@namespace}.{typeName}.g.cs";
        var sanitizedFileName = RemoveInvalidChars.Replace(originalFileName, InvalidCharReplacement);

        return sanitizedFileName;
    }
}