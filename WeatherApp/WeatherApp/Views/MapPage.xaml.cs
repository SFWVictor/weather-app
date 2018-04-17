namespace WeatherApp.Views
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using WeatherApp.Controls;
    using WeatherApp.Helpers;
    using WeatherApp.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Internals;
    using Xamarin.Forms.Maps;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            CitiesMap.Tapped += CitiesMap_Tapped;
            CitiesMap.CustomPins = new List<CustomPin>();
        }

        private MapViewModel ViewModel { get => BindingContext as MapViewModel; }

        protected override void OnBindingContextChanged()
        {
            if (!(ViewModel is null))
            {
                Load();
                ViewModel.Cities.CollectionChanged += (s, e) => Load();
            }
        }

        private void Load()
        {
            ObservableCollection<CityViewModel> cities = ViewModel.Cities;
            CitiesMap.Pins.Clear();
            CitiesMap.CustomPins.Clear();
            if (cities.Count != 0)
            {
                cities.ForEach(c =>
                {
                    CustomPin pin = new CustomPin()
                    {
                        Label = $"{c.Name}{Environment.NewLine}{Resx.AppResources.WindDirection}: {c.WindAngle}{Environment.NewLine}{c.WeatherMain}",
                        Type = PinType.Place,
                        Position = c.Coordinates
                    };
                    CitiesMap.Pins.Add(pin);
                    CitiesMap.CustomPins.Add(pin);
                });
                var city = cities.First();
                CenterOnPosition(city.Coordinates);
            };
        }

        private void CitiesMap_Tapped(object sender, Controls.MapTapEventArgs e)
        {
            Position resultPosition;
            ObservableCollection<CityViewModel> cities = ViewModel.Cities;

            if (cities.Count != 0)
            {
                var pos = e.Position;
                Position closestCity = cities.First().Coordinates;
                double minDistance = double.MaxValue;

                foreach (var city in cities)
                {
                    double distance = pos.GetDistance(city.Coordinates);
                    if (closestCity != city.Coordinates && distance < minDistance)
                    {
                        closestCity = city.Coordinates;
                        minDistance = distance;
                    }
                }

                resultPosition = closestCity;
            }
            else
            {
                resultPosition = e.Position;
            }

            CenterOnPosition(resultPosition);
        }

        private void CenterOnPosition(Position pos)
        {
            CitiesMap.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromKilometers(2)));
        }
    }
}