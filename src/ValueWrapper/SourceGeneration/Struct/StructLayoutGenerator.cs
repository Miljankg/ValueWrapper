using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct;

internal sealed class StructLayoutGenerator : IStructLayoutGenerator
{
    public Namespace GenerateLayout(StructLayoutConfig config)
    {
        var ns = new Namespace(config.NamespaceName);
        var str = new Structure(config.StructName, config.AccessModifier);
        var fromFactoryMethod = new StaticFactoryMethod(config.StructName, config.FactoryMethodName, config.ValueTypeName);
        var constructor = new Constructor(config.StructName, config.ValueTypeName);
        var valueProperty = new ValueProperty(config.ValueTypeName);

        str.Add(fromFactoryMethod);
        str.Add(constructor);
        str.Add(valueProperty);
        
        ns.Add(str);
        
        return ns;
    }
}