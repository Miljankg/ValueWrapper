using Microsoft.CodeAnalysis;

namespace ValueWrapper;

internal static class Diagnostics
{
    private static class Categories
    {
        public const string Usage = "Usage";
    }
    
    public static class MustBePartial
    {
        private const string Id = "VALWRP-001";
        private const string Message = "Any types annotated with the ValueWrapperAttribute must be marked as partial";
        private const string Title = "Missing partial keyword";

        public static Diagnostic For(SyntaxNode node)
        {
            var descriptor = new DiagnosticDescriptor(
                Id, 
                Title, 
                Message, 
                category: Categories.Usage, 
                DiagnosticSeverity.Error,
                isEnabledByDefault: true);

            return Diagnostic.Create(descriptor, node.GetLocation());
        }
    }
}