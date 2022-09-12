using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ValueWrapper;

internal sealed class ValueWrapperReceiver : ISyntaxContextReceiver
{
    private readonly List<TypeGenerationInfo> _targetTypes = new();
    
    public IReadOnlyList<TypeGenerationInfo> TargetTypes => _targetTypes;

    public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
    {
        if (context.Node is TypeDeclarationSyntax typeDeclarationSyntax)
        {
            if (TryGetAttribute(context, typeDeclarationSyntax, "ValueWrapper", out var attribute))
            {
                var typeGenerationInfo = new TypeGenerationInfo(typeDeclarationSyntax, attribute.ValueType);
                
                _targetTypes.Add(typeGenerationInfo);
            }
        }
    }

    private static bool TryGetAttribute(GeneratorSyntaxContext ctx, TypeDeclarationSyntax cdecl, string attributeName,
        out AttributeInfo? attributeInfo)
    {
// #if DEBUG
//         if (!Debugger.IsAttached)
//         {
//             Debugger.Launch();
//         }
// #endif
        attributeInfo = null;
        
        if (ctx.SemanticModel.GetDeclaredSymbol(cdecl) is not INamedTypeSymbol namedTypeSymbol)
        {
            return false;
        }

        var attribute = namedTypeSymbol.GetAttributes().Single();

        var valueType = attribute.ConstructorArguments.Single().Value.ToString();
        
        if (valueType is not null)
        {
            attributeInfo = new AttributeInfo(valueType);    
        }
        else
        {
            attributeInfo = null;
        }

        return valueType is not null;
    }
}

internal sealed class AttributeInfo
{
    public AttributeInfo(string valueType)
    {
        ValueType = valueType;
    }

    public string ValueType { get; }
}


internal sealed class TypeGenerationInfo
{
    public TypeGenerationInfo(TypeDeclarationSyntax typeDeclarationSyntax, string valueType)
    {
        TypeDeclarationSyntax = typeDeclarationSyntax;
        ValueType = valueType;
    }

    public TypeDeclarationSyntax TypeDeclarationSyntax { get; }
    
    public string ValueType { get; }
}