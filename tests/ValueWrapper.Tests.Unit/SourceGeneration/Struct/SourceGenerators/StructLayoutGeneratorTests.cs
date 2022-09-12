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
        [InlineData("Ns1", "Struct1", "Type1", "Method1", AccessModifierTestFactory.PublicAsString)]
        [InlineData("Ns2", "Struct2", "Type2", "Method2", AccessModifierTestFactory.InternalAsString)]
        [InlineData("Ns3", "Struct3", "Type3", "Method3", AccessModifierTestFactory.PrivateAsString)]
        public void CreatesCorrectLayout(
            string namespaceName,
            string structName,
            string valueType,
            string factoryMethodName,
            string accessModifier)
        {
            // Arrange.
            var layoutGenerator = new StructLayoutGenerator();

            var config = new StructLayoutConfig
            {
                NamespaceName = namespaceName,
                StructName = structName,
                ValueTypeName = valueType,
                FactoryMethodName = factoryMethodName,
                AccessModifier = AccessModifierTestFactory.FromString(accessModifier)
            };

            var expectedLayout = CreateExpectedLayout(factoryMethodName, config);

            // Act.
            var layout = layoutGenerator.GenerateLayout(config);

            // Assert.
            layout.Should().BeEquivalentTo(expectedLayout, opts => opts.RespectingRuntimeTypes());
        }

        private static Namespace CreateExpectedLayout(string factoryMethodName, StructLayoutConfig config)
        {
            var factoryMethod = new StaticFactoryMethod(config.StructName, factoryMethodName, config.ValueTypeName);
            var constructor = new Constructor(config.StructName, config.ValueTypeName);
            var valueProperty = new ValueProperty(config.ValueTypeName);
            var structure = new Structure(config.StructName, config.AccessModifier);
            var @namespace = new Namespace(config.NamespaceName);

            structure.Add(factoryMethod);
            structure.Add(constructor);
            structure.Add(valueProperty);

            @namespace.Add(structure);
            
            return @namespace;
        }
    }
}