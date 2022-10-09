using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ValueWrapper.Tests.Integration.GeneratorTests.Helpers;

internal static class CompilationOutPutExtensions
{
    public static SyntaxNode GetInputSyntaxNode(this CompilationOutput output)
    {
        return output.Compilation.SyntaxTrees
            .First()
            .GetRoot()
            .DescendantNodes()
            .First(n => n is StructDeclarationSyntax);
    }
}