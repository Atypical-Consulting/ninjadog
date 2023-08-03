namespace Ninjadog.Helpers;

public sealed record TypeContext
{
    public ITypeSymbol Type { get; }

    public StringTokens Tokens { get; }

    public string? Ns { get; }

    public string? RootNamespace { get; }

    public string HintName { get; }

    public PropertyContexts PropertyContexts { get; }

    public TypeContext(
        ITypeSymbol type,
        string className,
        string? subNamespace = null,
        StringTokens? stringTokens = null)
    {
        var rootNamespace = GetRootNamespace(type) ?? "Ninjadog";

        Type = type;
        Tokens = stringTokens ?? new StringTokens(type.Name);
        RootNamespace = rootNamespace;
        Ns = subNamespace is not null
            ? $"{rootNamespace}.{subNamespace}"
            : rootNamespace;
        HintName = $"{Ns}.{className}.g.cs";
        PropertyContexts = new PropertyContexts(type);
    }

    public void Deconstruct(
        out StringTokens tokens,
        out string? ns)
    {
        tokens = Tokens;
        ns = Ns;
    }
}
