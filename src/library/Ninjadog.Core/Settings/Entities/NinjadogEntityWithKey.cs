using Ninjadog.Core.Helpers;

namespace Ninjadog.Core.Settings;

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