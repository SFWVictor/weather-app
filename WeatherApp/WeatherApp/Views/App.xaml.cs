namespace WeatherApp
{
    using WeatherApp.ViewModels;
    using Xamarin.Forms;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
            }

            NavigationPage navigationPage = new NavigationPage(new WeatherApp.MainPage())
            {
                BindingContext = new MainPageViewModel()
            };

            MainPage = navigationPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
