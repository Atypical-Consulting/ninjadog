using Ninjadog.Settings;

namespace Ninjadog.Core.SettingsExtensions;

public record NinjadogEntityPropertyWithKey(
    string Key,
    NinjadogEntityProperty Property)
    : NinjadogEntityProperty(Property);
