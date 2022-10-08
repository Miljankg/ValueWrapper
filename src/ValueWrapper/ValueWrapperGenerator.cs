using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ValueWrapper.Attributes;
using ValueWrapper.SourceGeneration.Struct;
using ValueWrapper.SourceGeneratorHelpers;
using ValueWrapper.SourceLayout;

namespace ValueWrapper;

[Generator]
public class ValueWrapperGenerator : IIncrementalGenerator
{
    private const string ValueTypeParamName = "ValueType";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var structProvider = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: (node, token) => node.IsAnnotatedWithAttribute(token),
                transform: (syntaxContext, token) => GetStructsForSourceGeneration(syntaxContext, typeof(ValueWrapperAttribute), token))
            .Where(sds => sds is not null)
            .Select((sds, _) => sds!);

        var compilationAndStructs = context.CompilationProvider.Combine(structProvider.Collect());

        context.RegisterSourceOutput(source: compilationAndStructs, action: GenerateSource);
    }

    private static StructDeclarationSyntax? GetStructsForSourceGeneration(GeneratorSyntaxContext context, Type attributeType, CancellationToken token)
    {
        var structDeclaration = (StructDeclarationSyntax)context.Node;

        return context.GetStructsMarkedWithAttribute(structDeclaration, attributeType.FullName!, token);
    }

    private static void GenerateSource(
        SourceProductionContext context,
        (Compilation, ImmutableArray<StructDeclarationSyntax>) structs)
    {
        GenerateSource(context, compilation: structs.Item1, structs: structs.Item2);
    }

    private static void GenerateSource(
        SourceProductionContext context, 
        Compilation compilation, 
        ImmutableArray<StructDeclarationSyntax> structs)
    {
        if (structs.IsDefaultOrEmpty) return;

        foreach (var @struct in structs)
        {
            var symbol = @struct.GetSymbol(compilation);
            
            if (symbol is null) return;

            var attributeData = symbol.GetAttribute(compilation, typeof(ValueWrapperAttribute).FullName);

            if (attributeData is null) return;

            var valueType = GetValueType(attributeData);

            if (valueType is null) return;

            var config = CreateSourceConfig(symbol, valueType);
            var source = GenerateSource(config);

            context.AddSource($"{config.StructName}.g.cs", source);   
        }
    }

    private static StructGenerator.Config CreateSourceConfig(ISymbol symbol, string valueType)
    {
        var namespaceName = symbol.GetNamespace();
        var structName = symbol.Name;
        var accessibility = symbol.DeclaredAccessibility;

        var config = new StructGenerator.Config
        {
            NamespaceName = namespaceName,
            StructName = structName,
            ValueTypeName = valueType,
            AccessModifier = GetAccessModifier(accessibility),
            IndentationString = "    "
        };

        return config;
    }

    private static AccessModifier GetAccessModifier(Accessibility accessibility)
    {
        return accessibility switch
        {
            Accessibility.Private => AccessModifier.Private,
            Accessibility.Internal => AccessModifier.Internal,
            Accessibility.Public => AccessModifier.Public,
            _ => AccessModifier.Unknown
        };
    }

    private static string? GetValueType(AttributeData attributeData)
    {
        var attributeParamValue = attributeData.GetParameterValue(position: 0) 
                                  ?? attributeData.GetParameterValue(ValueTypeParamName);
        
        return attributeParamValue?.ToString();
    }

    private static string GenerateSource(StructGenerator.Config config)
    {
        var structGenerator = new StructGeneratorFactory().Create();
    
        return structGenerator.Generate(config);
    }
}