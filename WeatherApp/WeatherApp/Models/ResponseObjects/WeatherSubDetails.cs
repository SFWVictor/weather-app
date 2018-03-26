namespace WeatherApp.Models.ResponseObjects
{
    using Newtonsoft.Json;

    public class WeatherSubDetails
    {
        private string _main;
        private string _description;
        private string _icon;

        [JsonProperty("main")]
        public string Main { get => _main; set => _main = value; }
        [JsonProperty("description")]
        public string Description { get => _description; set => _description = value; }
        [JsonProperty("icon")]
        public string Icon { get => _icon; set => _icon = value; }
    }
}
