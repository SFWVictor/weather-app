namespace WeatherApp.Models.ResponseObjects
{
    using Newtonsoft.Json;

    public class WeatherTempDetails
    {
        private string _temp;
        private string _humidity;

        [JsonProperty("temp")]
        public string Temp { get => _temp; set => _temp = value; }
        [JsonProperty("humidity")]
        public string Humidity { get => _humidity; set => _humidity = value; }
    }
}
