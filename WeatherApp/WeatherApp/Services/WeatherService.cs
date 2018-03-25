namespace WeatherApp.Services
{
    using WeatherApp.Helper;

    public static class WeatherService
    {
        private const string _apiKey = WeatherApiKeyProvider.Key;

        public static string GetWeather(int cityId)
        {
            //TODO implement weather fetch
            return "Sunny";
        }
    }
}
