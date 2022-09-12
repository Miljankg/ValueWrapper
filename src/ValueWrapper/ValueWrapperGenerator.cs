using Microsoft.CodeAnalysis;
using ValueWrapper.SourceGeneration.Struct;
using ValueWrapper.SourceLayout;

namespace ValueWrapper;

[Generator]
public sealed class ValueWrapperGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new ValueWrapperReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
// #if DEBUG
//         if (!Debugger.IsAttached)
//         {
//             Debugger.Launch();
//         }
// #endif
        if (context.SyntaxContextReceiver is not ValueWrapperReceiver receiver) return;
        
        foreach (var targetType in receiver.TargetTypes)
        {
            var symbol = context
                .Compilation
                .GetSemanticModel(targetType.TypeDeclarationSyntax.SyntaxTree)
                .GetDeclaredSymbol(targetType.TypeDeclarationSyntax);
            
            var @namespace = symbol.ContainingNamespace?.ToDisplayString() ?? "NoNamespace";
            var @struct = symbol.Name ?? "NoName";

            var source = _GenerateSource(@namespace, @struct, targetType.ValueType);

            context.AddSource($"{@struct}.g.cs", source);   
        }
    }

    private static string _GenerateSource(string @namespace, string @struct, string valueTypeName)
    {
        try
        {
            var generator = new StructGeneratorFactory().Create();

            var config = new StructGenerator.Config
            {
                NamespaceName = @namespace,
                StructName = @struct,
                ValueTypeName = valueTypeName,
                AccessModifier = AccessModifier.Public,
                IndentationString = "    "
            };
            
            var source = generator.Generate(config);
            
            Log(source);
            
            return source;
        }
        catch (Exception e)
        {
            Log(e.Message);
            throw;
        }
    }

    private static void Log(string text)
    {
        File.AppendAllText(@"D:\test.txt", text);
    }
}