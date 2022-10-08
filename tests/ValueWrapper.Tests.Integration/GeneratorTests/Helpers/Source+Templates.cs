namespace ValueWrapper.Tests.Integration.GeneratorTests.Helpers;

public sealed partial class Source
{
    private const string Template = @"
            using ValueWrapper.Attributes;

            namespace {0};

            [ValueWrapperAttribute(typeof({1}))]
            {2} {3} {4} {5}
            {{
            }}
        ";
}