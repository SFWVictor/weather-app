namespace WeatherApp.Helpers.AppSettings
{
    using System.Collections.ObjectModel;
    using WeatherApp.Helper;
    using Xamarin.Forms;

    public sealed class Settings : SingletonBase<Settings>
    {
        private const string LocaleFileName = "Resources.locales.json";

        private Color _appColor;
        private NamedSize _fontSize;
        private ObservableCollection<LocaleInfo> _currentLocale;

        private Settings()
        {
            _appColor = Color.White;
            _fontSize = NamedSize.Medium;
            _currentLocale = new ObservableCollection<LocaleInfo>(JsonDeserializer.LoadFromJson<LocaleInfo>(LocaleFileName));
        }
    }
}
