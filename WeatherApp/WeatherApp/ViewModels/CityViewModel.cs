namespace WeatherApp.ViewModels
{
    using System;
    using System.Linq;
    using WeatherApp.Models;
    using WeatherApp.Helpers;
    using Xamarin.Forms.Maps;

    public class CityViewModel : City
    {
        private const string WeatherLoadErrorMessage = "Error loading weather";
        private const char WindAngleDefault = 'X';
        private string _weatherMain = null;
        private char _windAngle = WindAngleDefault;

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
                if (_weatherMain is null || _weatherMain == WeatherLoadErrorMessage)
                {
                    _weatherMain = WeatherLoadErrorMessage;
                    try
                    {
                        _weatherMain = WeatherService.GetWeather(Id).Weather.FirstOrDefault().Main;
                    }
                    catch
                    {
                    }
                }

                return _weatherMain;
            }
        }

        public char WindAngle
        {
            get
            {
                if (_windAngle == WindAngleDefault)
                {
                    int angle = 0;
                    try
                    {
                        angle = WeatherService.GetWeather(Id).Wind.Angle;
                    }
                    catch
                    {
                    }

                    _windAngle = GetArrow(angle);
                }

                return _windAngle;
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
