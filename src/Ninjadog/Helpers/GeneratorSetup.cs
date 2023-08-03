namespace Ninjadog.Helpers;

public class GeneratorSetup
{
    private readonly Func<StringTokens, string> _getClassName;
    private readonly Func<TypeContext, string>? _generateCode;
    private readonly Func<ImmutableArray<TypeContext>, string>? _generateCodeMultiple;

    public CodeGenerationMode CodeGenerationMode { get; }
    public string? SubNamespace { get; }

    public GeneratorSetup(
        Func<StringTokens, string> getClassName,
        Func<TypeContext, string> generateCode,
        string? subNamespace = null)
    {
        CodeGenerationMode = CodeGenerationMode.ByModel;
        SubNamespace = subNamespace;

        _getClassName = getClassName;
        _generateCode = generateCode;
    }

    public GeneratorSetup(
        string className,
        Func<ImmutableArray<TypeContext>, string> generateCode,
        string? subNamespace)
    {
        CodeGenerationMode = CodeGenerationMode.SingleFile;
        SubNamespace = subNamespace;

        _getClassName = _ => className;
        _generateCodeMultiple = generateCode;
    }

    public string GetClassName()
    {
        return _getClassName.Invoke(null!);
    }

    public string GetClassName(StringTokens st)
    {
        return _getClassName.Invoke(st);
    }

    public string GenerateCode(TypeContext typeContext)
    {
        return _generateCode is not null
            ? _generateCode.Invoke(typeContext)
            : "Error in GeneratorSetup";
    }

    public string GenerateCode(ImmutableArray<TypeContext> typeContexts)
    {
        return _generateCodeMultiple is not null
            ? _generateCodeMultiple.Invoke(typeContexts)
            : "Error in GeneratorSetup";
    }
}
