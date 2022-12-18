using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ValueWrapper.Attributes;
using ValueWrapper.SourceGeneration;
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
            if (!ValidateStruct(context, @struct)) continue;
            
            var symbol = @struct.GetSymbol(compilation);
            
            if (symbol is null) continue;

            var attributeData = symbol.GetAttribute(compilation, typeof(ValueWrapperAttribute).FullName);

            if (attributeData is null) continue;
            
            var valueTypeInfo = GetValueType(attributeData, compilation);
            var config = CreateSourceConfig(symbol, valueTypeInfo);
            var source = GenerateSource(config);
            var sourceFileName = GenerateFilename(config);
            
            context.AddSource(sourceFileName, source);   
        }
    }

    private static bool ValidateStruct(SourceProductionContext context, StructDeclarationSyntax @struct)
    {
        if (!@struct.IsMarkedAsPartial())
        {
            context.ReportDiagnostic(Diagnostics.MustBePartial.For(@struct));
            return false;
        }

        return true;
    }

    private static StructGenerator.Config CreateSourceConfig(ISymbol symbol, ValueTypeInfo valueTypeInfo)
    {
        var namespaceName = symbol.GetNamespace();
        var structName = symbol.Name;
        var accessibility = symbol.DeclaredAccessibility;

        var config = new StructGenerator.Config
        {
            NamespaceName = namespaceName,
            StructName = structName,
            ValueTypeInfo = valueTypeInfo,
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

    private static ValueTypeInfo GetValueType(AttributeData attributeData, Compilation compilation)
    {
        var attributeParamValue = attributeData.GetParameterValue(position: 0) 
                                  ?? attributeData.GetParameterValue(ValueTypeParamName);
        
        var type = (INamedTypeSymbol) attributeParamValue!;

        var isValueType = type.IsValueType;
        var canBeNull = type.IsNullableValueType(compilation);
        var typeName = attributeParamValue!.ToString();
        
        return new ValueTypeInfo(typeName, isValueType, canBeNull);
    }

    private static string GenerateSource(StructGenerator.Config config)
    {
        var structGenerator = new StructGeneratorFactory().Create();
    
        return structGenerator.Generate(config);
    }

    private static string GenerateFilename(StructGenerator.Config config)
    {
        var fileNameGenerator = new SourceFileNameGenerator();

        return fileNameGenerator.GenerateFileName(config.NamespaceName, config.StructName);
    }
}