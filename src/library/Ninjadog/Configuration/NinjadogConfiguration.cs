// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Serialization;

namespace Ninjadog.Configuration;

[JsonSerializable(typeof(NinjadogConfiguration))]
public partial class JsonSerializationContext
    : JsonSerializerContext;

[method: JsonConstructor]
public record NinjadogConfiguration(
    NinjadogGeneralConfiguration General);

[method: JsonConstructor]
public record NinjadogGeneralConfiguration(
    string Name,
    Dictionary<string, NinjadogEntity>? Entities = null);

public record NinjadogEntity(
    Dictionary<string, NinjadogEntityProperty>? Properties = null);

[method: JsonConstructor]
public record NinjadogEntityProperty(
    string Type,
    bool IsKey);
