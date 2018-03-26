﻿namespace WeatherApp.ViewModels
{
    using System.Linq;
    using WeatherApp.Models;
    using WeatherApp.Services;

    public class CityViewModel : City
    {
        public CityViewModel(City city) :
            base(city.Id, city.Name, city.Coordinates, city.Description, city.SmallImageUrl)
        {
        }

        public CityViewModel(int id, string name, Coordinates coordinates, string description, string smallImageUrl) :
            base(id, name, coordinates, description, smallImageUrl)
        {
        }

        public string WeatherMain
        {
            get
            {
                return WeatherService.GetWeather(Id).Weather.FirstOrDefault().Main;
            }
        }
    }
}
