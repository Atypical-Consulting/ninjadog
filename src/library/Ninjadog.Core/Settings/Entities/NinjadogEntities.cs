namespace Ninjadog.Core.Settings;

public class NinjadogEntities
    : Dictionary<string, NinjadogEntity>
{
    public List<NinjadogEntityWithKey> FromKeys()
    {
        return this
            .Select(x => new NinjadogEntityWithKey(x.Key, x.Value.Properties))
            .ToList();
    }
}