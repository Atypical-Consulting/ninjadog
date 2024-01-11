namespace Ninjadog.Core.Settings;

public record NinjadogEntityPropertyWithKey(
    string Key,
    NinjadogEntityProperty Property)
    : NinjadogEntityProperty(Property);