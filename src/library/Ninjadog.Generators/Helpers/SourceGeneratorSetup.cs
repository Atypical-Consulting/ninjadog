// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Ninjadog.Configuration;

namespace Ninjadog.Helpers;

public class SourceGeneratorSetup
{
    private readonly Func<StringTokens, string> _getClassName;
    private readonly Func<NinjadogSettings, string>? _generateCode;
    private readonly Func<NinjadogSettings, string>? _generateMultiple;

    public CodeGenerationMode CodeGenerationMode { get; }
    public string? SubNamespace { get; }

    public SourceGeneratorSetup(
        Func<StringTokens, string> getClassName,
        Func<NinjadogSettings, string> generateCode,
        string? subNamespace = null)
    {
        CodeGenerationMode = CodeGenerationMode.ByModel;
        SubNamespace = subNamespace;

        _getClassName = getClassName;
        _generateCode = generateCode;
    }

    public string GetClassName()
    {
        return _getClassName.Invoke(null!);
    }

    public string GetClassName(StringTokens st)
    {
        return _getClassName.Invoke(st);
    }

    public string GenerateCode(NinjadogSettings ninjadogSettings)
    {
        return _generateCode is not null
            ? _generateCode.Invoke(ninjadogSettings)
            : "Error in GeneratorSetup";
    }
}
