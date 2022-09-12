using System;
using System.Linq;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using ValueWrapper.SourceGeneration;

namespace ValueWrapper.Tests.Unit.TestTools;

internal static class SourceTestExtensions
{
    public static SourceTemplateAssertions Should(this SourceTemplate sourceTemplate)
    {
        return new SourceTemplateAssertions(sourceTemplate);
    }
}

internal sealed class SourceTemplateAssertions : ReferenceTypeAssertions<SourceTemplate, SourceTemplateAssertions>
{
    public SourceTemplateAssertions(SourceTemplate subject) : base(subject)
    {
    }

    protected override string Identifier => "Source Template";

    public void ContainLine(int index, string lineContent, int lineLevel)
    {
        var templateLines = Subject.Lines.ToList();

        var targetLine = templateLines.ElementAtOrDefault(index);
        
        Execute.Assertion
            .ForCondition(targetLine is not null)
            .FailWith($"There is not enough lines: {templateLines.Count} > {index}")
            .Then
            .ForCondition(targetLine?.Content.Equals(lineContent, StringComparison.Ordinal) ?? false)
            .FailWith($"Target line content is not as expected: '{lineContent}' => '{targetLine?.Content}'")
            .Then
            .ForCondition(targetLine?.Level == lineLevel)
            .FailWith($"Target line level is not as expected: '{lineLevel} => '{targetLine?.Level}'");
    }
}