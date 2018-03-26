namespace WeatherApp.Models.ResponseObjects
{
    using Newtonsoft.Json;

    public class WeatherSysDetails
    {
        private string _country;

        [JsonProperty("country")]
        public string Country { get => _country; set => _country = value; }
    }
}
