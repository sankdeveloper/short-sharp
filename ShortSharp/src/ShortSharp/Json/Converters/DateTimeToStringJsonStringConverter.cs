using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShortSharp.Json.Converters;

/// <summary>
/// String format is : 2022-10-21 (YYYY-mm-dd)
/// </summary>
/// <example>
/// <code>
/// [JsonPropertyName("date")]
/// [JsonConverter(typeof(DateTimeToStringJsonStringConverter))]
/// public DateTime Date { get; set; }
/// </code>
/// </example>
public class DateTimeToStringJsonStringConverter: JsonConverter<DateTime>
{
    private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

    /// <inheritdoc />
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Invalid JsonProperty type '{reader.TokenType}'.");
        }
        var converted = DateTime.TryParseExact(
            reader.GetString()!,
            DateTimeFormat,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var value);

        if (!converted) throw new JsonException($"Conversion error for the value '{reader.GetString()}'");
        return value;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, DateTime dateTime, JsonSerializerOptions options)
    {
        var value = dateTime.ToString(DateTimeFormat, CultureInfo.InvariantCulture);
        writer.WriteStringValue(value);
    }
}