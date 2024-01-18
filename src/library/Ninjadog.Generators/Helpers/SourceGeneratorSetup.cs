// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Configuration;

namespace Ninjadog.Helpers;

public class SourceGeneratorSetup(
    Func<StringTokens, string> getClassName,
    Func<NinjadogSettings, string> generateCode,
    string? subNamespace = null)
{
    private readonly Func<NinjadogSettings, string>? _generateCode = generateCode;
    private readonly Func<NinjadogSettings, string>? _generateMultiple;

    public CodeGenerationMode CodeGenerationMode { get; } = CodeGenerationMode.ByModel;
    public string? SubNamespace { get; } = subNamespace;

    public string GetClassName()
    {
        return getClassName.Invoke(null!);
    }

    public string GetClassName(StringTokens st)
    {
        return getClassName.Invoke(st);
    }

    public string GenerateCode(NinjadogSettings ninjadogSettings)
    {
        return _generateCode is not null
            ? _generateCode.Invoke(ninjadogSettings)
            : "Error in GeneratorSetup";
    }
}
