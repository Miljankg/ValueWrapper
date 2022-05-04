using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ValueWrapper;

internal sealed class ValueWrapperReceiver : ISyntaxContextReceiver
{
    private readonly List<TypeDeclarationSyntax> _targetTypes = new();
    
    public IReadOnlyList<TypeDeclarationSyntax> TargetTypes => _targetTypes;

    public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
    {
        if (context.Node is TypeDeclarationSyntax typeDeclarationSyntax)
        {
            if (IsDecoratedWithAttribute(typeDeclarationSyntax, "ValueWrapper"))
            {
                _targetTypes.Add(typeDeclarationSyntax);
            }
        }
    }
    
    private static bool IsDecoratedWithAttribute(TypeDeclarationSyntax cdecl, string attributeName) =>
        cdecl.AttributeLists
            .SelectMany(x => x.Attributes)
            .Any(x => string.Equals(x.Name.ToString(), attributeName, StringComparison.OrdinalIgnoreCase));
}