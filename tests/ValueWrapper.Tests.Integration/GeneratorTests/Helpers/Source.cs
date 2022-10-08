namespace ValueWrapper.Tests.Integration.GeneratorTests.Helpers;

public sealed partial class Source
{
    private readonly string _template;

    public static Source Struct(Config config) 
        => new(Template, config);
    
    private Source(
        string template,
        Config config)
    {
        _template = template;
        CurrentConfig = config;
    }

    public Config CurrentConfig { get; }

    public string GetAsString() 
        => string.Format(
            _template, 
            CurrentConfig.Namespace, 
            CurrentConfig.ValueType, 
            CurrentConfig.AccessModifier,
            CurrentConfig.Partial ? "partial" : string.Empty,
            CurrentConfig.Type, 
            CurrentConfig.Name);

    public override string ToString()
        => $"{CurrentConfig.AccessModifier} {CurrentConfig.Type} {CurrentConfig.Name}<{CurrentConfig.ValueType}>";
}