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
    public partial class CitiesMapView : ContentView
    {
        public CitiesMapView()
        {
            InitializeComponent();
            CitiesMap.Tapped += CitiesMap_Tapped;
        }

        private MapViewModel ViewModel { get => BindingContext as MapViewModel; }

        public void CenterOnPosition(Position pos)
        {
            CitiesMap.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromKilometers(2)));
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
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
                    CustomPin pin;
                    switch (Device.RuntimePlatform)
                    {
                        case Device.iOS:
                            pin = new CustomPin()
                            {
                                Label = $"{c.Name}",
                                Address = $"{c.WeatherMain}, {Resx.AppResources.WindDirection}: {c.WindAngle}",
                                Type = PinType.Place,
                                Position = c.Coordinates
                            };
                            break;
                        default:
                            pin = new CustomPin()
                            {
                                Label = $"{c.Name}{Environment.NewLine}{Resx.AppResources.WindDirection}: {c.WindAngle}{Environment.NewLine}{c.WeatherMain}",
                                Type = PinType.Place,
                                Position = c.Coordinates
                            };
                            break;
                    }

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
                CityViewModel closestCity = cities.First();
                double minDistance = pos.GetDistance(closestCity.Coordinates);

                foreach (var city in cities)
                {
                    double distance = pos.GetDistance(city.Coordinates);
                    if ((closestCity.Coordinates != city.Coordinates) && (distance < minDistance))
                    {
                        closestCity = city;
                        minDistance = distance;
                    }
                }

                resultPosition = closestCity.Coordinates;
            }
            else
            {
                resultPosition = e.Position;
            }

            CenterOnPosition(resultPosition);
        }
    }
}