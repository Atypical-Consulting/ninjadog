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
