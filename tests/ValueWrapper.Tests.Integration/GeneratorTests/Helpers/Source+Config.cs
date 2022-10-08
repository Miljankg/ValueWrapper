namespace ValueWrapper.Tests.Integration.GeneratorTests.Helpers;

public sealed partial class Source
{
    public sealed class Config
    {
        public string? Namespace { get; set; }
        
        public string? ValueType { get; set; }
        
        public string? AccessModifier { get; set;  }
        
        public bool Partial { get; set; }
        
        public string? Type { get; set; }
        
        public string? Name { get; set; }
    }
}