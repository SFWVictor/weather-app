namespace WeatherApp.Models
{
    using Newtonsoft.Json;

    public struct Coordinates
    {
        private double _longitude;
        private double _latitude;

        [JsonConstructor]
        public Coordinates(double longitude, double latitude)
        {
            _longitude = longitude;
            _latitude = latitude;
        }

        [JsonProperty("longitude")]
        public double Longitude { get => _longitude; set => _longitude = value; }
        [JsonProperty("latitude")]
        public double Latitude { get => _latitude; set => _latitude = value; }
    }
}
