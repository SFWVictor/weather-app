namespace WeatherApp.Models.ResponseObjects
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class WeatherMainModel
    {
        private string _name;
        private WeatherTempDetails _main;
        private List<WeatherSubDetails> _weather;
        private WeatherWindDetails _wind;
        private WeatherSysDetails _sys;

        [JsonProperty("name")]
        public string Name { get => _name; set => _name = value; }
        [JsonProperty("main")]
        public WeatherTempDetails Main { get => _main; set => _main = value; }
        [JsonProperty("weather")]
        public List<WeatherSubDetails> Weather { get => _weather; set => _weather = value; }
        [JsonProperty("wind")]
        public WeatherWindDetails Wind { get => _wind; set => _wind = value; }
        [JsonProperty("sys")]
        public WeatherSysDetails Sys { get => _sys; set => _sys = value; }
    }
}
