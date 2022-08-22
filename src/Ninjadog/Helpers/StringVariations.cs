namespace Ninjadog.Helpers;

internal sealed record StringVariations(string Pascal)
{
    public string Camel { get; } = Pascal.Camelize();
    public string CamelPlural { get; } = Pascal.Camelize().Pluralize();
    public string Pascal { get; } = Pascal;
    public string PascalPlural { get; } = Pascal.Pluralize();
    public string Dashed { get; } = Pascal.Underscore().Dasherize();
    public string DashedPlural { get; } = Pascal.Pluralize().Underscore().Dasherize();
    public string Humanized { get; } = Pascal.Underscore().Humanize().ToLower();
    public string HumanizedPlural { get; } = Pascal.Pluralize().Underscore().Humanize().ToLower();
}
