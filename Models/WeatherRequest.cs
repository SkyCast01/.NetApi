namespace NasaWeatherApi.Models
{
    public class WeatherRequest
    {
        public string city { get; set; }
        public string datetime { get; set; } // Keep as string to match Python API
   
    }
    public class WeeklyForecastRequest
    {
        public string City { get; set; }
        public string StartDate { get; set; } // "YYYY-MM-DD"
    }

}
