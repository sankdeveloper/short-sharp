using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShortSharp.Json.Converters;

/// <summary>
/// 
/// </summary>
/// <example>
/// <code>
/// [JsonPropertyName("id")]
/// [JsonConverter(typeof(IntToStringJsonStringConverter))]
/// public int IntegerId { get; set; }
/// </code>
/// </example>
public class IntConverter : JsonConverter<int>
{
    /// <inheritdoc />
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Invalid JsonProperty type '{reader.TokenType}'.");
        }

        var converted = int.TryParse(reader.GetString()!, out var value);
        if (!converted) throw new JsonException($"Conversion error for the value '{reader.GetString()}'");
        return value;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}