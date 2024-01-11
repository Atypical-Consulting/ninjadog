namespace Ninjadog.Settings.Extensions;

public record NinjadogEntityPropertyWithKey(
    string Key,
    NinjadogEntityProperty Property)
    : NinjadogEntityProperty(Property);
