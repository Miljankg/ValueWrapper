using Microsoft.CodeAnalysis;

namespace ValueWrapper.SourceGeneratorHelpers;

internal static class AttributeDataExtensions
{
    public static object? GetParameterValue(this AttributeData attribute, int position)
    {
        return attribute.ConstructorArguments.Length < position + 1 
            ? null 
            : attribute.ConstructorArguments[position].Value;
    }

    public static object? GetParameterValue(this AttributeData attribute, string parameterName)
    {
        foreach (var arg in attribute.NamedArguments)
        {
            if (string.Equals(arg.Key, parameterName, StringComparison.InvariantCultureIgnoreCase))
            {
                return arg.Value.Value;
            }
        }

        return null;
    }
}