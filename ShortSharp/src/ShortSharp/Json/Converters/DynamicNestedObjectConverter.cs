using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShortSharp.Json.Converters;

/// <summary>
/// 
/// </summary>
public class DynamicNestedObjectConverter : JsonConverter<IReadOnlyDictionary<string, dynamic>>
{
    /// <inheritdoc />
    public override Dictionary<string, dynamic>? Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Invalid JsonProperty type '{reader.TokenType}'.");
        }

        Dictionary<string, dynamic>? keyValue = new();
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return keyValue;

            if (reader.TokenType != JsonTokenType.PropertyName) continue;
            
            var propertyName = reader.GetString() ?? null;
            if (!JsonDocument.TryParseValue(ref reader, out JsonDocument? jsonDoc)) continue;

            if (propertyName != null && !keyValue.ContainsKey(propertyName))
            {
                var jsonValue = jsonDoc.RootElement.GetRawText();
                keyValue.Add(propertyName, JsonSerializer.Deserialize<dynamic>(jsonValue));
            }
        }

        return keyValue!;
    }

    /// <inheritdoc />
    public override void Write(
        Utf8JsonWriter writer,
        IReadOnlyDictionary<string, dynamic> value,
        JsonSerializerOptions options)
    {
        var serialize = JsonSerializer.Serialize(value);
        writer.WriteStringValue(serialize);
    }
}