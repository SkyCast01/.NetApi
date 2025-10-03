using System.Text.Json;

namespace NasaWeatherApi.Services
{
    public class GeoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _geoApiKey;

        public GeoService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _geoApiKey = config["Geoapify:ApiKey"]; // store in appsettings.json
        }

        public async Task<(double lat, double lon)> GetCoordinatesAsync(string city)
        {
            var url = $"https://api.geoapify.com/v1/geocode/search?text={Uri.EscapeDataString(city)}&type=city&apiKey={_geoApiKey}";
            var resp = await _httpClient.GetAsync(url);

            if (!resp.IsSuccessStatusCode)
                throw new Exception($"Geoapify error: {resp.StatusCode}");

            var json = await resp.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var root = doc.RootElement;
            if (!root.TryGetProperty("features", out var features) || features.GetArrayLength() == 0)
                throw new Exception($"No coordinates found for city: {city}");

            var props = features[0].GetProperty("properties");

            double lat = props.GetProperty("lat").GetDouble();
            double lon = props.GetProperty("lon").GetDouble();

            return (lat, lon);
        }
    }
}
