using Microsoft.CodeAnalysis;

namespace ValueWrapper.SourceGeneratorHelpers;

internal static class SymbolExtensions
{
    public static AttributeData? GetAttribute(this ISymbol symbol, Compilation compilation, string attributeName)
    {
        var attribute = compilation.GetTypeByMetadataName(attributeName);

        if (attribute is null) return null;
        
        return symbol
            .GetAttributes()
            .FirstOrDefault(a => attribute.Equals(a.AttributeClass, SymbolEqualityComparer.Default));
    }

    public static string GetNamespace(this ISymbol symbol)
        => symbol.ContainingNamespace?.ToDisplayString() ?? string.Empty;
}