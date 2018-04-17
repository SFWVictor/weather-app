﻿namespace WeatherApp.ViewModels
{
    using System;
    using System.Linq;
    using WeatherApp.Models;
    using WeatherApp.Services;
    using Xamarin.Forms.Maps;

    public class CityViewModel : City
    {
        public CityViewModel(City city) :
            base(city.Id, city.Name, city.Coordinates, city.Description, city.SmallImageUrl)
        {
        }

        public CityViewModel(int id, string name, Position coordinates, string description, string smallImageUrl) :
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

        public char WindAngle
        {
            get
            {
                int angle = WeatherService.GetWeather(Id).Wind.Angle;
                return GetArrow(angle);
            }
        }

        public char GetArrow(int angle)
        {
            char leftArrow = (char)8592;//(char)8592; 
            char rightArrow = (char)8593;//(char)8593; 
            char upArrow = (char)8594;//(char)8594; 
            char downArrow = (char)8595;//(char)8595; 
            const int cicrle = 360;
            const int sector = 45;
            int typeArrow = (angle % cicrle) / sector;

            switch (typeArrow)
            {
                case int i when (i < 2):
                    return upArrow;
                case int i when (i < 4 && i >= 2):
                    return rightArrow;
                case int i when (i < 6 && i >= 4):
                    return downArrow;
                case int i when (i < 8 && i >= 6):
                    return leftArrow;
                default:
                    throw new ArgumentException(nameof(angle));
            }
        }
    }
}
