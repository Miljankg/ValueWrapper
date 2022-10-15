using Microsoft.CodeAnalysis;

namespace ValueWrapper.SourceGeneratorHelpers;

internal static class SymbolExtensions
{
    public static bool IsNullableValueType(this INamedTypeSymbol symbol, Compilation compilation)
    {
        if (!symbol.IsValueType) return false;
        
        var nullable = compilation.GetTypeByMetadataName("System.Nullable`1");

        return SymbolEqualityComparer.Default.Equals(symbol.ConstructedFrom, nullable);
    }
    
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