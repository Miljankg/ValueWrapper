using System.Collections.Generic;
using FluentAssertions;
using ValueWrapper.SourceGeneration.Struct;
using ValueWrapper.SourceLayout;
using ValueWrapper.Tests.Unit.TestTools;
using Xunit;

namespace ValueWrapper.Tests.Unit.SourceGeneration.Struct.SourceGenerators;

public sealed class StructLayoutGeneratorTests
{
    public class CreateLayout
    {
        [Theory]
        [MemberData(nameof(GetTestCases))]
        public void CreatesCorrectLayout(
            string namespaceName,
            string structName,
            TestType valueType,
            string factoryMethodName,
            string accessModifier)
        {
            // Arrange.
            var layoutGenerator = new StructLayoutGenerator();

            var config = new StructLayoutConfig
            {
                NamespaceName = namespaceName,
                StructName = structName,
                ValueTypeInfo = valueType.ToInfo(),
                FactoryMethodName = factoryMethodName,
                AccessModifier = AccessModifierTestFactory.FromString(accessModifier)
            };

            var expectedLayout = CreateExpectedLayout(factoryMethodName, config);

            // Act.
            var layout = layoutGenerator.GenerateLayout(config);

            // Assert.
            layout.Should().BeEquivalentTo(expectedLayout, opts => opts.RespectingRuntimeTypes());
        }

        
        public static IEnumerable<object[]> GetTestCases()
        {
            yield return new object[] { "Ns1", "Struct1", TestType.ValueType("Type1"), "Method1", AccessModifierTestFactory.PublicAsString };
            yield return new object[] { "Ns2", "Struct2", TestType.NullableValueType("Type2"), "Method2", AccessModifierTestFactory.InternalAsString };
            yield return new object[] { "Ns3", "Struct3", TestType.ReferenceType("Type3"), "Method3", AccessModifierTestFactory.PrivateAsString };
        }

        private static RootNode CreateExpectedLayout(string factoryMethodName, StructLayoutConfig config)
        {
            var root = new RootNode();
            var nullableEnable = Directive.NullableEnable;
            var factoryMethod = new StaticFactoryMethod(config.StructName, factoryMethodName, config.ValueTypeInfo.TypeName);
            var constructor = new Constructor(config.StructName, config.ValueTypeInfo.TypeName);
            var valueProperty = new ValueProperty(config.ValueTypeInfo.TypeName);
            var equals = new Equals(config.StructName, config.ValueTypeInfo.TypeName);
            var equalsObject = new EqualsObject(config.StructName);
            var equalsOperator = new EqualsOperator(config.StructName);
            var notEqualOperator = new NotEqualOperator(config.StructName);
            var getHashCode = new GetHashCode(!config.ValueTypeInfo.IsValueType || config.ValueTypeInfo.CanBeNull);
            var toString = new ToString(!config.ValueTypeInfo.IsValueType || config.ValueTypeInfo.CanBeNull);
            var structure = new Structure(config.StructName, config.AccessModifier);
            var @namespace = new Namespace(config.NamespaceName);

            structure.Add(factoryMethod);
            structure.Add(constructor);
            structure.Add(valueProperty);
            structure.Add(equals);
            structure.Add(equalsObject);
            structure.Add(equalsOperator);
            structure.Add(notEqualOperator);
            structure.Add(getHashCode);
            structure.Add(toString);

            @namespace.Add(structure);
            root.Add(nullableEnable);
            root.Add(@namespace);

            return root;
        }
    }
}