namespace Ninjadog.Core.Settings;

public class NinjadogEntityProperties
    : Dictionary<string, NinjadogEntityProperty>
{
    public List<NinjadogEntityPropertyWithKey> FromKeys()
    {
        return this
            .Select(x => new NinjadogEntityPropertyWithKey(x.Key, x.Value))
            .ToList();
    }
}