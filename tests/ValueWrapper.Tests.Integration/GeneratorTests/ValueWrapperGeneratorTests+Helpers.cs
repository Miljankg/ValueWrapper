using System.Collections.Generic;
using System.Threading.Tasks;
using ValueWrapper.Tests.Integration.GeneratorTests.Helpers;
using ValueWrapper.Tests.Integration.GeneratorTests.Helpers.AssertionExtensions;
using VerifyXunit;
using Xunit;

namespace ValueWrapper.Tests.Integration.GeneratorTests;

public partial class ValueWrapperGeneratorTests
{
    private static Source DefaultStruct => Source.Struct(GetDefaultConfig());

    private static Source.Config GetDefaultConfig()
    {
        return new Source.Config
        {
            Type = "struct",
            Namespace = "TestNamespace",
            AccessModifier = "public",
            Partial = true,
            ValueType = "int",
            Name = "TestId"
        };
    }
}