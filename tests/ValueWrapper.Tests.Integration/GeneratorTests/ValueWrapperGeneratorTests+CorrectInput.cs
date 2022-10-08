using System.Collections.Generic;
using System.Threading.Tasks;
using ValueWrapper.Tests.Integration.GeneratorTests.Helpers;
using ValueWrapper.Tests.Integration.GeneratorTests.Helpers.AssertionExtensions;
using VerifyXunit;
using Xunit;

namespace ValueWrapper.Tests.Integration.GeneratorTests;

public class ValueWrapperGeneratorTests
{
    [UsesVerify]
    public class CorrectInput
    {
        [Theory]
        [MemberData(nameof(GetAccessModifierTestCases))]
        public Task DifferentAccessModifiersHandledCorrectly(Source source)
        {
            // Arrange / Act.
            var output = Compile.Source(source);

            // Assert.
            return output.Should().BeCorrectFor(source);
        }
        
        [Theory]
        [MemberData(nameof(GetValueTypeTestCases))]
        public Task DifferentValueTypesHandledCorrectly(Source source)
        {
            // Arrange / Act.
            var output = Compile.Source(source);

            // Assert.
            return output.Should().BeCorrectFor(source);
        }
        
        [Theory]
        [MemberData(nameof(GetNameTestCases))]
        public Task DifferentNamesHandledCorrectly(Source source)
        {
            // Arrange / Act.
            var output = Compile.Source(source);

            // Assert.
            return output.Should().BeCorrectFor(source);
        }
        
        [Theory]
        [MemberData(nameof(GetNamespaceTestCases))]
        public Task DifferentNamespacesHandledCorrectly(Source source)
        {
            // Arrange / Act.
            var output = Compile.Source(source);

            // Assert.
            return output.Should().BeCorrectFor(source);
        }

        public static IEnumerable<object[]> GetAccessModifierTestCases()
        {
            yield return new object[] { DefaultStruct.WithAccessModifier("public") };
            yield return new object[] { DefaultStruct.WithAccessModifier("internal") };
            yield return new object[] { DefaultStruct.WithAccessModifier("private")};
        }
        
        public static IEnumerable<object[]> GetValueTypeTestCases()
        {
            yield return new object[] { DefaultStruct.WithValueType("int") };
            yield return new object[] { DefaultStruct.WithValueType("string") };
        }

        public static IEnumerable<object[]> GetNameTestCases()
        {
            yield return new object[] { DefaultStruct.WithName("SomeId") };
            yield return new object[] { DefaultStruct.WithName("SomeOtherId") };
        }

        public static IEnumerable<object[]> GetNamespaceTestCases()
        {
            yield return new object[] { DefaultStruct.WithNamespace("SomeNamespace") };
            yield return new object[] { DefaultStruct.WithNamespace("SomeOtherNamespace") };
        }


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
}