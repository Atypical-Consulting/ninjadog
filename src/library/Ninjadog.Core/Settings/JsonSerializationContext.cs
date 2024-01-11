using System.Text.Json.Serialization;

namespace Ninjadog.Core.Settings;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DictionaryKeyPolicy = JsonKnownNamingPolicy.Unspecified)]
[JsonSerializable(typeof(NinjadogSettings))]
internal partial class JsonSerializationContext
    : JsonSerializerContext;
