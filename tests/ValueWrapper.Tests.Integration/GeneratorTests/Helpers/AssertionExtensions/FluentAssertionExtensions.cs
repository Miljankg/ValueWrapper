namespace ValueWrapper.Tests.Integration.GeneratorTests.Helpers.AssertionExtensions;

internal static class FluentAssertionExtensions 
{
    public static CompilationOutputAssertions Should(this CompilationOutput instance)
    {
        return new CompilationOutputAssertions(instance); 
    }
}