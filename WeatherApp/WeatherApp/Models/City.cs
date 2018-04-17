namespace WeatherApp.Models
{
    using System;
    using Newtonsoft.Json;
    using Xamarin.Forms.Maps;

    public class City
    {
        private int _id;
        private string _name;
        private Position _coordinates;
        private string _description;
        private string _smallImageUrl;

        public City(int id, string name, Position coordinates, string description, string smallImageUrl)
        {
            _id = id;
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _coordinates = coordinates;
            _description = description ?? throw new ArgumentNullException(nameof(description));
            _smallImageUrl = smallImageUrl ?? throw new ArgumentNullException(nameof(smallImageUrl));
        }

        [JsonProperty("id")]
        public int Id { get => _id; set => _id = value; }
        [JsonProperty("name")]
        public string Name { get => _name; set => _name = value; }
        [JsonProperty("coordinates")]
        public Position Coordinates { get => _coordinates; set => _coordinates = value; }
        [JsonProperty("description")]
        public string Description { get => _description; set => _description = value; }
        [JsonProperty("smallImageUrl")]
        public string SmallImageUrl { get => _smallImageUrl; set => _smallImageUrl = value; }
    }
}
