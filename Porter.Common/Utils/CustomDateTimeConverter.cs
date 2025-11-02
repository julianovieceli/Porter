using System.Text.Json;
using System.Text.Json.Serialization;

namespace Porter.Common.Utils
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // Use "s" format specifier for sortable format without fractional seconds (if UTC)
            // Or a custom format: "yyyy-MM-ddTHH:mm:ssZ" for UTC, "yyyy-MM-ddTHH:mm:ss" for local (no offset)
            writer.WriteStringValue(value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
        }
    }
}
