using NasaWeatherApi.Models;
using System.Text;
using System.Text.Json;

namespace NasaWeatherApi.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly GeoService _geoService;

        public WeatherService(HttpClient httpClient, GeoService geoService, IConfiguration config)
        {
            _httpClient = httpClient;
            _geoService = geoService;
            _httpClient.BaseAddress = new Uri(config["PythonApi:BaseUrl"]);
        }

        // Existing single-day prediction
        public async Task<WeatherResponse?> PredictWeatherAsync(WeatherRequest req)
        {
            var (lat, lon) = await _geoService.GetCoordinatesAsync(req.city);

            var enrichedReq = new
            {
                city = req.city,
                datetime = req.datetime,
                lat,
                lon
            };

            var json = JsonSerializer.Serialize(enrichedReq);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/predict", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Python API error: {response.StatusCode}");

            var respJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<WeatherResponse>(respJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // NEW weekly forecast method
        public async Task<WeeklyForecastResponse?> PredictWeeklyWeatherAsync(WeeklyForecastRequest req)
        {
            var (lat, lon) = await _geoService.GetCoordinatesAsync(req.City);

            var enrichedReq = new
            {
                city = req.City,
                start_date = req.StartDate,
                lat,
                lon
            };

            var json = JsonSerializer.Serialize(enrichedReq);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/predict_week", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Python API error: {response.StatusCode}");

            var respJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<WeeklyForecastResponse>(respJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
