using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct;

internal sealed class StructLayoutGenerator : IStructLayoutGenerator
{
    public RootNode GenerateLayout(StructLayoutConfig config)
    {
        var root = new RootNode();
        var nullableEnable = Directive.NullableEnable;
        var @namespace = new Namespace(config.NamespaceName);
        var @struct = new Structure(config.StructName, config.AccessModifier);
        var fromFactoryMethod = new StaticFactoryMethod(config.StructName, config.FactoryMethodName, config.ValueTypeInfo.TypeName);
        var constructor = new Constructor(config.StructName, config.ValueTypeInfo.TypeName);
        var valueProperty = new ValueProperty(config.ValueTypeInfo.TypeName);
        var equals = new Equals(config.StructName, config.ValueTypeInfo.TypeName);
        var equalsObject = new EqualsObject(config.StructName);
        var equalsOperator = new EqualsOperator(config.StructName);
        var notEqualOperator = new NotEqualOperator(config.StructName);
        var getHashCode = new GetHashCode(!config.ValueTypeInfo.IsValueType || config.ValueTypeInfo.CanBeNull);
        var toString = new ToString(!config.ValueTypeInfo.IsValueType || config.ValueTypeInfo.CanBeNull);

        @struct.Add(fromFactoryMethod);
        @struct.Add(constructor);
        @struct.Add(valueProperty);
        @struct.Add(equals);
        @struct.Add(equalsObject);
        @struct.Add(equalsOperator);
        @struct.Add(notEqualOperator);
        @struct.Add(getHashCode);
        @struct.Add(toString);
        
        @namespace.Add(@struct);
        
        root.Add(nullableEnable);
        root.Add(@namespace);
        
        return root;
    }
}