using PRG_MAUI_Car_Register.Model;
using System.Text.Json;

namespace PRG_MAUI_Car_Register.Services
{
    public static class DataService
    {
        private static readonly string filePath =
            Path.Combine(FileSystem.AppDataDirectory, "Vehicles.json");

        public static async Task SaveAsync(List<Vehicle> vehicles)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new VehicleConverter() }
            };

            string json = JsonSerializer.Serialize(vehicles, options);
            await File.WriteAllTextAsync(filePath, json);
        }

        public static async Task<List<Vehicle>> LoadAsync()
        {
            if (!File.Exists(filePath))
                return new List<Vehicle>();

            string json = await File.ReadAllTextAsync(filePath);

            var options = new JsonSerializerOptions
            {
                Converters = { new VehicleConverter() }
            };

            return JsonSerializer.Deserialize<List<Vehicle>>(json, options)
                   ?? new List<Vehicle>();
        }
    }
}
