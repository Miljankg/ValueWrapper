namespace ValueWrapper.Tests.Integration.GeneratorTests.Helpers;

public sealed partial class Source
{
    public Source WithNamespace(string? @namespace)
    {
        CurrentConfig.Namespace = @namespace;
        
        return new Source(_template, CurrentConfig);
    }

    public Source WithValueType(string valueType)
    {
        CurrentConfig.ValueType = valueType;
        
        return new Source(_template, CurrentConfig);
    }

    public Source WithAccessModifier(string accessModifier)
    {
        CurrentConfig.AccessModifier = accessModifier;
        
        return new Source(_template, CurrentConfig);
    }

    public Source WithName(string name)
    {
        CurrentConfig.Name = name;
        
        return new Source(_template, CurrentConfig);
    }
}