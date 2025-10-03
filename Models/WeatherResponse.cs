using System.Text.Json.Serialization;

namespace NasaWeatherApi.Models
{
    public class WeatherResponse
    {
        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [JsonPropertyName("humidity")]
        public double Humidity { get; set; }

        [JsonPropertyName("wind_speed")]
        public double WindSpeed { get; set; }

        [JsonPropertyName("weather")]
        public string Weather { get; set; }
    }
    public class DailyForecast
    {
        public string Date { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public string Weather { get; set; }
    }

    public class WeeklyForecastResponse
    {
        public string City { get; set; }
        public List<DailyForecast> Predictions { get; set; }
    }
}
