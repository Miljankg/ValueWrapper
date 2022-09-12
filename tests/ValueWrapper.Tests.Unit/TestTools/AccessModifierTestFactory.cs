using System;
using ValueWrapper.SourceLayout;

namespace ValueWrapper.Tests.Unit.TestTools;

internal sealed class AccessModifierTestFactory
{
    public const string PublicAsString = "public";
    
    public const string InternalAsString = "internal";
    
    public const string PrivateAsString = "private";
    
    public static AccessModifier FromString(string accessModifierStr)
    {
        return accessModifierStr switch
        {
            PublicAsString => AccessModifier.Public,
            InternalAsString => AccessModifier.Internal,
            PrivateAsString => AccessModifier.Private,
            _ => throw new InvalidOperationException($"Cannot use access modifier '{accessModifierStr}' as test parameter.")
        };
    }
}