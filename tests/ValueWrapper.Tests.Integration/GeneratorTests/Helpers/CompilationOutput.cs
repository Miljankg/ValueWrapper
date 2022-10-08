using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace ValueWrapper.Tests.Integration.GeneratorTests.Helpers;

internal sealed class CompilationOutput
{
    public CompilationOutput(
        GeneratorDriver driver,
        Compilation compilation,
        ImmutableArray<Diagnostic> diagnostics)
    {
        Driver = driver;
        Compilation = compilation;
        Diagnostics = diagnostics;
    }

    public GeneratorDriver Driver { get; }
    
    public Compilation Compilation { get; }
    
    public ImmutableArray<Diagnostic> Diagnostics { get; }
}