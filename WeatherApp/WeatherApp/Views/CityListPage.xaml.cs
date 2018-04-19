namespace WeatherApp.Views
{
    using WeatherApp.Helpers.AppSettings;
    using WeatherApp.ViewModels;
    using Xamarin.Forms;

    public partial class CityListPage : ContentPage
    {
        private double _width = 0;
        private double _height = 0;
        private CitiesMapView _citiesMapView = null;
        private CityListView _citiesListView = null;
        private StackOrientation _orientation;
        private bool _initialized = false;

        public CityListPage()
        {
            InitializeComponent();
        }

        private CityListViewModel ViewModel => BindingContext as CityListViewModel;

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (_width != width || _height != height)
            {
                _width = width;
                _height = height;

                var newOrientation = DetermineOrientation(width, height);
                if (_initialized && _orientation != newOrientation)
                {
                    _orientation = newOrientation;
                    SetupLayout(_orientation);
                }
            }
        }

        protected override void OnBindingContextChanged()
        {
            if (!_initialized)
            {
                InitialSetup();
            }
        }

        private void InitialSetup()
        {
            _initialized = true;
            _orientation = StackOrientation.Vertical;
            _citiesListView = new CityListView()
            {
                BindingContext = ViewModel,
                HorizontalOptions = LayoutOptions.Start
            };
            _citiesMapView = new CitiesMapView()
            {
                BindingContext = new MapViewModel(ViewModel.Cities),
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            MainStackLayout.Children.Add(_citiesListView);
            SetupLayout(_orientation, false);
        }

        private void SetupLayout(StackOrientation newOrientation)
        {
            SetupLayout(newOrientation, true);
        }

        private void SetupLayout(StackOrientation newOrientation, bool subsequentCall)
        {
            switch (newOrientation)
            {
                case StackOrientation.Vertical:
                    SetupVerticalLayout(subsequentCall);
                    break;
                case StackOrientation.Horizontal:
                    SetupHorizontalLayout(subsequentCall);
                    break;
                default:
                    SetupVerticalLayout(subsequentCall);
                    break;
            }
        }

        private void SetupVerticalLayout(bool subsequentCall)
        {
            if (MainStackLayout.Children.Contains(_citiesMapView))
            {
                MainStackLayout.Children.Remove(_citiesMapView);
            }

            _citiesListView.ItemTapped += CityList_ItemTappedVertical;
            if (subsequentCall)
            {
                _citiesListView.ItemTapped -= CityList_ItemTappedHorizontal;
            }
        }

        private void SetupHorizontalLayout(bool subsequentCall)
        {
            MainStackLayout.Children.Add(_citiesMapView);

            _citiesListView.ItemTapped += CityList_ItemTappedHorizontal;
            if (subsequentCall)
            {
                _citiesListView.ItemTapped -= CityList_ItemTappedVertical;
            }
        }

        private async void CityList_ItemTappedVertical(object sender, ItemTappedEventArgs e)
        {
            var selectedObject = ((ListView)sender).SelectedItem;
            ((ListView)sender).SelectedItem = null;

            var cityDetailsPage = new CityDetailsPage
            {
                BindingContext = selectedObject
            };

            await Navigation.PushAsync(cityDetailsPage);

            Settings.Instance.LocaleChanged += Settings_LocaleChanged;
        }

        private void CityList_ItemTappedHorizontal(object sender, ItemTappedEventArgs e)
        {
            var selectedCity = ((ListView)sender).SelectedItem as CityViewModel;

            _citiesMapView.CenterOnPosition(selectedCity.Coordinates);
        }

        private async void Settings_LocaleChanged(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
            Settings.Instance.LocaleChanged -= Settings_LocaleChanged;
        }

        private StackOrientation DetermineOrientation(double width, double height)
        {
            if (height > width)
            {
                return StackOrientation.Vertical;
            }
            else
            {
                return StackOrientation.Horizontal;
            }
        }
    }
}
