// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

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
