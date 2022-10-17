using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct;

internal sealed class StructLayoutGenerator : IStructLayoutGenerator
{
    public Namespace GenerateLayout(StructLayoutConfig config)
    {
        var ns = new Namespace(config.NamespaceName);
        var str = new Structure(config.StructName, config.AccessModifier);
        var fromFactoryMethod = new StaticFactoryMethod(config.StructName, config.FactoryMethodName, config.ValueTypeInfo.TypeName);
        var constructor = new Constructor(config.StructName, config.ValueTypeInfo.TypeName);
        var valueProperty = new ValueProperty(config.ValueTypeInfo.TypeName);
        var equals = new Equals(config.StructName, config.ValueTypeInfo.TypeName);
        var equalsObject = new EqualsObject(config.StructName);
        var equalsOperator = new EqualsOperator(config.StructName);
        var notEqualOperator = new NotEqualOperator(config.StructName);
        var getHashCode = new GetHashCode(!config.ValueTypeInfo.IsValueType || config.ValueTypeInfo.CanBeNull);
        var toString = new ToString(!config.ValueTypeInfo.IsValueType || config.ValueTypeInfo.CanBeNull);

        str.Add(fromFactoryMethod);
        str.Add(constructor);
        str.Add(valueProperty);
        str.Add(equals);
        str.Add(equalsObject);
        str.Add(equalsOperator);
        str.Add(notEqualOperator);
        str.Add(getHashCode);
        str.Add(toString);
        
        ns.Add(str);
        
        return ns;
    }
}