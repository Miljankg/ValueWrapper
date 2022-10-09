namespace ValueWrapper.Tests.Integration.GeneratorTests.Helpers;

public sealed partial class Source
{
    public sealed class Config
    {
        public string? Namespace { get; private set; }
        
        public string? ValueType { get; private set; }
        
        public string? AccessModifier { get; private set;  }
        
        public bool Partial { get; private set; }
        
        public string? Type { get; private set; }
        
        public string? Name { get; private set; }

        public sealed class Builder
        {
            public static Builder From(Config config)
            {
                return new Builder
                {
                    Namespace = config.Namespace,
                    ValueType = config.ValueType,
                    AccessModifier = config.AccessModifier,
                    Partial = config.Partial,
                    Type = config.Type,
                    Name = config.Name
                };
            }
            
            public string? Namespace { get; set; }
        
            public string? ValueType { get; set; }
        
            public string? AccessModifier { get; set;  }
        
            public bool Partial { get; set; }
        
            public string? Type { get; set; }
        
            public string? Name { get; set; }

            public Config Build()
            {
                return new Config
                {
                    Namespace = Namespace,
                    ValueType = ValueType,
                    AccessModifier = AccessModifier,
                    Partial = Partial,
                    Type = Type,
                    Name = Name
                };
            }
        }
    }
}