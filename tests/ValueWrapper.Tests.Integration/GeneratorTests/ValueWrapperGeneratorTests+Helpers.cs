using ValueWrapper.Tests.Integration.GeneratorTests.Helpers;

namespace ValueWrapper.Tests.Integration.GeneratorTests;

public partial class ValueWrapperGeneratorTests
{
    private static Source DefaultStruct => Source.Struct(GetDefaultConfig());

    private static Source.Config GetDefaultConfig()
    {
        return new Source.Config.Builder
        {
            Type = "struct",
            Namespace = "TestNamespace",
            AccessModifier = "public",
            Partial = true,
            ValueType = "int",
            Name = "TestId"
        }.Build();
    }
}