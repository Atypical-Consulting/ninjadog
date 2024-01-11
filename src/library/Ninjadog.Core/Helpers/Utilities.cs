namespace Ninjadog.Core.Helpers;

internal static class Utilities
{
    internal static string DefaultCodeLayout(string code)
    {
        return SourceGenerationHelper.Header +
               SourceGenerationHelper.NullableEnable +
               "\n" +
               code +
               "\n" +
               SourceGenerationHelper.NullableDisable;
    }

    internal static string? WriteFileScopedNamespace(string? ns)
    {
        return ns is not null
            ? $"namespace {ns};"
            : null;
    }
}
