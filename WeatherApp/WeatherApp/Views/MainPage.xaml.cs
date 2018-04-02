namespace WeatherApp
{
    using System;
    using WeatherApp.ViewModels;
    using WeatherApp.Views;
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        private double _width = 0;
        private double _height = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (_width != width || _height != height)
            {
                _width = width;
                _height = height;
                //TODO reconfigure layout
            }
        }

        private async void ButtonSettings_Tapped(object sender, EventArgs e)
        {
            var settingsPage = new SettingsPage();

            await Navigation.PushAsync(settingsPage);
        }
    }
}
