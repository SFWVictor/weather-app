namespace WeatherApp.Models.ResponseObjects
{
    using Newtonsoft.Json;

    public class WeatherWindDetails
    {
        private string _speed;

        [JsonProperty("speed")]
        public string Speed { get => _speed; set => _speed = value; }
    }
}
