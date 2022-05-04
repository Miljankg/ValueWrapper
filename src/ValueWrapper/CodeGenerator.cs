using System.Diagnostics.Contracts;
using System.Text;

namespace ValueWrapper;

internal abstract class CodeGenerator
{
    private readonly List<CodeGenerator> _innerCodeGenerators = new();

    protected List<CodeGenerator> InnerCodeGenerators;

    protected CodeGenerator()
    {
        InnerCodeGenerators = _innerCodeGenerators;
    }

    public void AddInnerCodeGenerator(CodeGenerator innerCodeGenerator)
    {
        _innerCodeGenerators.Add(innerCodeGenerator);
    }

    public abstract string Generate();

    protected string GenerateFromInnerGenerators()
    {
        var sb = new StringBuilder();

        foreach (var codeGenerator in _innerCodeGenerators)
        {
            sb.Append(codeGenerator.Generate());
        }
        
        return sb.ToString();
    }
}

internal sealed class TypeCodeGenerator : CodeGenerator
{
    public static TypeCodeGenerator CreateStructGenerator(string @namespace, string structName)
    {
        var typeGenerator = new TypeCodeGenerator();

        var namespaceGenerator = new NamespaceCodeGenerator(@namespace);
        var structGenerator = new StructCodeGenerator(structName);
        var staticFactoryMethodGenerator = new StaticFactoryMethodCodeGenerator(structName);
            
        structGenerator.AddInnerCodeGenerator(staticFactoryMethodGenerator);
        namespaceGenerator.AddInnerCodeGenerator(structGenerator);
        typeGenerator.AddInnerCodeGenerator(namespaceGenerator);

        return typeGenerator;
    }
    
    public override string Generate()
    {
        return $@"
{GenerateFromInnerGenerators()}
";      
    }
}

internal sealed class UsingCodeGenerator : CodeGenerator
{
    public override string Generate()
    {
        return "using ValueWrapper;";
    }
}

internal sealed class NamespaceCodeGenerator : CodeGenerator
{
    private readonly string _namespace;

    public NamespaceCodeGenerator(string @namespace)
    {
        _namespace = @namespace;
    }
    public override string Generate()
    {
        return $@"
namespace {_namespace}
{{
    {GenerateFromInnerGenerators()}
}}
";
    }
}

internal sealed class StructCodeGenerator : CodeGenerator
{
    private readonly string _structName;

    public StructCodeGenerator(string structName)
    {
        _structName = structName;
    }
    
    public override string Generate()
    {
        return $@"
public partial struct {_structName}
{{
    {GenerateFromInnerGenerators()}
}}
";
    }
}

internal sealed class StaticFactoryMethodCodeGenerator : CodeGenerator
{
    private readonly string _structName;

    public StaticFactoryMethodCodeGenerator(string structName)
    {
        _structName = structName;
    }
    
    public override string Generate()
    {
        return $"public static {_structName} From() => new {_structName}();";
    }
}