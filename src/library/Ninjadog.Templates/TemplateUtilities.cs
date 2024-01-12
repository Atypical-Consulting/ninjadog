// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Core.Helpers;

namespace Ninjadog.Templates;

public static class TemplateUtilities
{
    public static string DefaultCodeLayout(string code)
    {
        return SourceGenerationHelper.Header +
               SourceGenerationHelper.NullableEnable +
               "\n" +
               code +
               "\n" +
               SourceGenerationHelper.NullableDisable;
    }

    public static string? WriteFileScopedNamespace(string? ns)
    {
        return ns is not null
            ? $"namespace {ns};"
            : null;
    }
}
