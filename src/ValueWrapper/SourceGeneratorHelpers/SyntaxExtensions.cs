using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ValueWrapper.SourceGeneratorHelpers;

internal static class SyntaxExtensions
{
    public static bool IsMarkedAsPartial(this StructDeclarationSyntax syntaxNode)
    {
        return syntaxNode.Modifiers.Any(SyntaxKind.PartialKeyword);
    }
    
    public static bool IsAnnotatedWithAttribute(this SyntaxNode syntaxNode, CancellationToken _)
    {
        return syntaxNode is StructDeclarationSyntax { AttributeLists.Count: > 0 };
    }
    
    public static INamedTypeSymbol? GetSymbol(this SyntaxNode syntaxNode, Compilation compilation)
    {
        return compilation.GetSemanticModel(syntaxNode.SyntaxTree).GetDeclaredSymbol(syntaxNode) as INamedTypeSymbol;
    }

    public static StructDeclarationSyntax? GetStructsMarkedWithAttribute(
        this GeneratorSyntaxContext context,
        StructDeclarationSyntax structDeclarationSyntax, 
        string attributeName,
        CancellationToken token)
    {
        foreach (var attributeList in structDeclarationSyntax.AttributeLists)
        {
            token.ThrowIfCancellationRequested();
            
            foreach (var attributeSyntax in attributeList.Attributes)
            {
                token.ThrowIfCancellationRequested();
                
                var fullName = attributeSyntax.GetAttributeSymbol(context, token)?.GetFullName();

                if (fullName == attributeName)
                {
                    return structDeclarationSyntax;
                }
            }
        }

        return null;
    }

    private static ISymbol? GetAttributeSymbol(
        this AttributeSyntax attributeSyntax, 
        GeneratorSyntaxContext context, 
        CancellationToken token)
        => ModelExtensions.GetSymbolInfo(context.SemanticModel, attributeSyntax, token).Symbol as IMethodSymbol;
        

    private static string GetFullName(this ISymbol symbol) 
        => symbol.ContainingSymbol.ToDisplayString();
}