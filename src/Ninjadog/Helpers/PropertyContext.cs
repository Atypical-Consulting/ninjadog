namespace Ninjadog.Helpers;

public sealed record PropertyContext(bool IsId, bool IsLast, string Name)
{
    public bool IsId { get; } = IsId;
    public bool IsLast { get; } = IsLast;
    public string Name { get; } = Name;
}
