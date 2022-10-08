using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using ValueWrapper.Attributes;

namespace ValueWrapper.Tests.Integration.GeneratorTests.Helpers;

internal static class Compile
{
    private const string AssemblyName = "generator";
    
    private static readonly Type[] TypesToInclude = 
    {
        typeof(ValueWrapperGenerator),
        typeof(ValueWrapperAttribute)
    };

    private static IIncrementalGenerator Generator => new ValueWrapperGenerator();
    
    public static CompilationOutput Source(Source source)
    {
        var syntaxTree = ParseSource(source.GetAsString());

        var references = GetReferences(TypesToInclude);
        
        var compilation = CreateCompilation(AssemblyName, references, syntaxTree);

        var (driver, updatedCompilation, diagnostic) = Run(compilation, Generator);
        
        return new CompilationOutput(driver, updatedCompilation, diagnostic);
    }

    private static SyntaxTree ParseSource(string source)
    {
        return CSharpSyntaxTree.ParseText(source);
    }

    private static IEnumerable<PortableExecutableReference> GetReferences(params Type[] typesToInclude)
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => !assembly.IsDynamic && !string.IsNullOrWhiteSpace(assembly.Location))
            .Select(assembly => MetadataReference.CreateFromFile(assembly.Location))
            .Concat(typesToInclude.Select(t => MetadataReference.CreateFromFile(t.Assembly.Location)));
    }

    private static CSharpCompilation CreateCompilation(
        string assemblyName,
        IEnumerable<PortableExecutableReference> references, 
        SyntaxTree syntaxTree)
    {
        return CSharpCompilation.Create(
            assemblyName,
            syntaxTrees: new[] {syntaxTree},
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
    }

    private static (GeneratorDriver Driver , Compilation Compilation, ImmutableArray<Diagnostic> Diagnostic) Run(
        Compilation compilation, 
        params IIncrementalGenerator[] generators)
    {
        var driver = CSharpGeneratorDriver
            .Create(generators)
            .RunGeneratorsAndUpdateCompilation(compilation, out var updatedCompilation, out var diagnostic);

        return (driver, updatedCompilation, diagnostic);
    }
}