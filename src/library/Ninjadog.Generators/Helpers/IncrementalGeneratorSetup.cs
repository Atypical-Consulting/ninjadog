// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Helpers;

public class IncrementalGeneratorSetup
{
    private readonly Func<StringTokens, string> _getClassName;
    private readonly Func<TypeContext, string>? _generateCode;
    private readonly Func<ImmutableArray<TypeContext>, string>? _generateCodeMultiple;

    public CodeGenerationMode CodeGenerationMode { get; }
    public string? SubNamespace { get; }

    public IncrementalGeneratorSetup(
        Func<StringTokens, string> getClassName,
        Func<TypeContext, string> generateCode,
        string? subNamespace = null)
    {
        CodeGenerationMode = CodeGenerationMode.ByModel;
        SubNamespace = subNamespace;

        _getClassName = getClassName;
        _generateCode = generateCode;
    }

    public IncrementalGeneratorSetup(
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
