namespace WeatherApp.Views
{
    using WeatherApp.Helpers.AppSettings;
    using Xamarin.Forms;

    public partial class CityListPage : ContentPage
    {
        private double _width = 0;
        private double _height = 0;

        public CityListPage()
        {
            InitializeComponent();
            CityList.ItemTapped += CityList_ItemTapped; ;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (_width != width || _height != height)
            {
                _width = width;
                _height = height;
                //TODO reconfigure layout
            }
        }

        private async void CityList_ItemTapped(object sender, ItemTappedEventArgs e)
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

        private async void Settings_LocaleChanged(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
            Settings.Instance.LocaleChanged -= Settings_LocaleChanged;
        }
    }
}
