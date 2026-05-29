using System.Text.Json;
using System.Text.Json.Serialization;

namespace PRG_MAUI_Car_Register.Model
{
    public class VehicleConverter : JsonConverter<Vehicle>
    {
        public override Vehicle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            string type = root.GetProperty("Type").GetString()!;

            return type switch
            {
                "Car" => JsonSerializer.Deserialize<Car>(root.GetRawText(), options)!,
                "MC" => JsonSerializer.Deserialize<MC>(root.GetRawText(), options)!,
                "Truck" => JsonSerializer.Deserialize<Truck>(root.GetRawText(), options)!,
                _ => throw new JsonException($"Unknown vehicle type: {type}")
            };
        }

        public override void Write(Utf8JsonWriter writer, Vehicle value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
