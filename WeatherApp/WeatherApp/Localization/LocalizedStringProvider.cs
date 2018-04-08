namespace WeatherApp.Localization
{
    using System.ComponentModel;
    using WeatherApp.Helpers.AppSettings;

    public class LocalizedStringProvider : INotifyPropertyChanged
    {
        private static LocalizedStringProvider _instance;

        private LocalizedStringProvider()
        {
            SetHandlers();
        }

        public static LocalizedStringProvider Instance
        {
            get
            {
                return _instance ?? (_instance = new LocalizedStringProvider());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string CitiesText
        {
            get
            {
                return Resx.AppResources.CitiesText;
            }
        }

        public string SettingsText
        {
            get
            {
                return Resx.AppResources.SettingsText;
            }
        }

        public string FontColorText
        {
            get
            {
                return Resx.AppResources.FontColorText;
            }
        }

        public string AppLanguageText
        {
            get
            {
                return Resx.AppResources.AppLanguageText;
            }
        }

        public string FontSizeText
        {
            get
            {
                return Resx.AppResources.FontSizeText;
            }
        }

        public string LatitudeText
        {
            get
            {
                return Resx.AppResources.LatitudeText;
            }
        }

        public string LongitudeText
        {
            get
            {
                return Resx.AppResources.LongitudeText;
            }
        }

        private void SetHandlers()
        {
            Settings.Instance.LocaleChanged += (o, e) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CitiesText)));
            Settings.Instance.LocaleChanged += (o, e) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SettingsText)));
            Settings.Instance.LocaleChanged += (o, e) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontColorText)));
            Settings.Instance.LocaleChanged += (o, e) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AppLanguageText)));
            Settings.Instance.LocaleChanged += (o, e) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontSizeText)));
            Settings.Instance.LocaleChanged += (o, e) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LatitudeText)));
            Settings.Instance.LocaleChanged += (o, e) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LongitudeText)));
        }
    }
}
