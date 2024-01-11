using Ninjadog.Helpers;

namespace Ninjadog.Settings.Extensions;

public record NinjadogEntityWithKey(
    string Key,
    NinjadogEntityProperties Properties)
    : NinjadogEntity(Properties)
{
    public StringTokens GetStringTokens()
    {
        return new StringTokens(Key);
    }
}
