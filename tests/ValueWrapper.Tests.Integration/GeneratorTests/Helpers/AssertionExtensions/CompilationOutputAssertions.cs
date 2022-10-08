using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Primitives;
using FluentAssertions.Specialized;
using Microsoft.CodeAnalysis;
using VerifyTests;
using VerifyXunit;

namespace ValueWrapper.Tests.Integration.GeneratorTests.Helpers.AssertionExtensions;

internal class CompilationOutputAssertions : 
    ReferenceTypeAssertions<CompilationOutput, CompilationOutputAssertions>
{
    public CompilationOutputAssertions(CompilationOutput instance)
        : base(instance)
    {
    }

    protected override string Identifier => "CompilationOutput";

    public Task<AndWhichConstraint<GenericAsyncFunctionAssertions<VerifyResult>, VerifyResult>> BeCorrectFor(
        Source source,
        [CallerMemberName] string caller = "")
    {
        return Subject
            .Invoking(s => InvokeVerifier(s, source, caller))
            .Should()
            .NotThrowAsync();
    }

    private Task<VerifyResult> InvokeVerifier(CompilationOutput output, Source source, string caller)
    {
        // ReSharper disable once ExplicitCallerInfoArgument
        return Verifier
            .Verify(target: GetSyntaxTreeForComparison(output).ToString(), settings: null, sourceFile: caller)
            .UseParameters(GetParameterAsString(source))
            .ToTask();
    }

    private static SyntaxTree GetSyntaxTreeForComparison(CompilationOutput output)
    {
        return output.Compilation.SyntaxTrees.Last();
    }

    private static string GetParameterAsString(Source source)
    {
        var config = source.CurrentConfig;

        return $"{config.Namespace}-{config.AccessModifier}-{config.Partial}" +
               $"-{config.Type}-{config.Name}-{config.ValueType}";
    }
}