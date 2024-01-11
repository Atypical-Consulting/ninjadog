// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Serialization;

namespace Ninjadog.Configuration;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DictionaryKeyPolicy = JsonKnownNamingPolicy.Unspecified)]
[JsonSerializable(typeof(NinjadogSettings))]
internal partial class JsonSerializationContext
    : JsonSerializerContext;

public record NinjadogSettings(
    NinjadogConfiguration Config,
    NinjadogEntities Entities
);

public record NinjadogConfiguration(
    string Name,
    string Version,
    string Description,
    string RootNamespace);

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

public record NinjadogEntity(
    NinjadogEntityProperties Properties);

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

public record NinjadogEntityProperty(
    string Type,
    bool IsKey);

public record NinjadogEntityPropertyWithKey(
    string Key,
    NinjadogEntityProperty Property)
    : NinjadogEntityProperty(Property);
