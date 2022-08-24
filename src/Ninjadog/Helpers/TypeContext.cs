namespace Ninjadog.Helpers;

public sealed record TypeContext
{
    public ITypeSymbol Type { get; }

    public StringTokens Tokens { get; }

    public string? Ns { get; }

    public string? RootNamespace { get; }

    public string HintName { get; }

    public TypeContext(
        ITypeSymbol type,
        string className,
        string? subNamespace = null,
        StringTokens? stringTokens = null)
    {
        Type = type;
        Tokens = stringTokens ?? new StringTokens(type.Name);
        RootNamespace = GetRootNamespace(type) ?? "Ninjadog";
        Ns = RootNamespace is not null ? $"{RootNamespace}.{subNamespace}" : null;
        HintName = $"{Ns}.{className}.g.cs";
    }

    public void Deconstruct(
        out StringTokens tokens,
        out string? ns)
    {
        tokens = Tokens;
        ns = Ns;
    }
}
