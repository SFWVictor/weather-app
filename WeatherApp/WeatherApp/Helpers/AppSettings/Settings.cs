namespace WeatherApp.Helpers.AppSettings
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using WeatherApp.Helper;
    using Xamarin.Forms;

    public sealed class Settings : SingletonBase<Settings>
    {
        private readonly string LocaleFileName = "Resources.locales.json";

        private ObservableCollection<LocaleInfo> _locales;
        private ObservableCollection<Color> _colors;
        private Color _currentFontColor;
        private NamedSize _currentFontSize;
        private LocaleInfo _currentLocale;

        private Settings()
        {
            _currentFontSize = NamedSize.Medium;
            _locales = new ObservableCollection<LocaleInfo>(JsonDeserializer.LoadFromJson<LocaleInfo>(LocaleFileName));
            _colors = new ObservableCollection<Color>() { Color.Black, Color.Blue, Color.Brown, Color.Red, Color.Violet };
            _currentLocale = _locales.First();
            _currentFontColor = _colors.First();
        }

        public ObservableCollection<Color> Colors { get => _colors; }

        public List<string> FontSizeNames
        {
            get
            {
                return Enum.GetNames(typeof(NamedSize)).Select(b => b.SplitCamelCase()).ToList();
            }
        }

        public ObservableCollection<LocaleInfo> Locales { get => _locales; }

        public Color CurrentFontColor { get => _currentFontColor; set => _currentFontColor = value; }

        public NamedSize CurrentFontSize { get => _currentFontSize; set => _currentFontSize = value; }

        public LocaleInfo CurrentLocale { get => _currentLocale; set => _currentLocale = value; }
    }
}
