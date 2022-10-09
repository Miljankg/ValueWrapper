using System;

namespace ValueWrapper.Tests.Integration.GeneratorTests.Helpers;

public sealed partial class Source
{
    public Source WithNamespace(string? @namespace) 
        => UpdateSource(b => b.Namespace = @namespace);

    public Source WithValueType(string valueType)
        => UpdateSource(b => b.ValueType = valueType);

    public Source WithAccessModifier(string accessModifier)
        => UpdateSource(b => b.AccessModifier = accessModifier);

    public Source WithName(string name)
        => UpdateSource(b => b.Name = name);

    public Source MarkAsNonPartial()
        => UpdateSource(b => b.Partial = false);

    private Source UpdateSource(Action<Config.Builder> builderAction)
    {
        var builder = Config.Builder.From(CurrentConfig);

        builderAction(builder);

        return new Source(_template, builder.Build());
    }
}