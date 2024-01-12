// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

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
