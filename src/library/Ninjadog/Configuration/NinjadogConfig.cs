// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Serialization;

namespace Ninjadog.Configuration;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DictionaryKeyPolicy = JsonKnownNamingPolicy.Unspecified)]
[JsonSerializable(typeof(NinjadogConfig))]
internal partial class JsonSerializationContext
    : JsonSerializerContext;

public record NinjadogConfig(
    NinjadogConfigGeneral General,
    Dictionary<string, NinjadogConfigEntity> Entities
);

public record NinjadogConfigGeneral(
    string Name);

public record NinjadogConfigEntity(
    Dictionary<string, NinjadogConfigEntityProperty> Properties);

public record NinjadogConfigEntityProperty(
    string Type,
    bool IsKey);
