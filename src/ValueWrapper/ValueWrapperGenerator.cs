using System.Diagnostics;
using Microsoft.CodeAnalysis;

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
        if (context.SyntaxContextReceiver is not ValueWrapperReceiver receiver) return;
        
        foreach (var targetType in receiver.TargetTypes)
        {
            var symbol = context
                .Compilation
                .GetSemanticModel(targetType.SyntaxTree)
                .GetDeclaredSymbol(targetType);
            
            var @namespace = symbol.ContainingNamespace?.ToDisplayString() ?? "NoNamespace";
            var structName = symbol.Name ?? "NoName";

            var structGenerator = TypeCodeGenerator.CreateStructGenerator(@namespace, structName);

            var source = structGenerator.Generate();
            
            context.AddSource($"{structName}.g.cs", source);   
        }
    }
}