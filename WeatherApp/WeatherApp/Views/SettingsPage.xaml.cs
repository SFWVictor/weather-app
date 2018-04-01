namespace WeatherApp.Views
{
    using WeatherApp.Helpers.Converters;
    using WeatherApp.Helpers.AppSettings;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            BindingContext = Settings.Instance;
            PickerFontSize.SetBinding(Picker.SelectedIndexProperty, new Binding("SelectedFontSize", BindingMode.Default, new IntEnumConverter()));
        }
    }
}