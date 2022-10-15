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
        var toString = new ToString(!config.ValueTypeInfo.IsValueType || config.ValueTypeInfo.CanBeNull);

        str.Add(fromFactoryMethod);
        str.Add(constructor);
        str.Add(valueProperty);
        str.Add(toString);
        
        ns.Add(str);
        
        return ns;
    }
}