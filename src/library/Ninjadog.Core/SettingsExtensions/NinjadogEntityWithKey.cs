using Ninjadog.Core.Helpers;
using Ninjadog.Settings;

namespace Ninjadog.Core.SettingsExtensions;

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
