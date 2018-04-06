namespace WeatherApp.Helpers.AppSettings
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using WeatherApp.Helper;
    using Xamarin.Forms;

    public sealed class Settings : INotifyPropertyChanged
    {
        private static Settings _instance;
        private readonly string LocaleFileName = "Resources.locales.json";
        private double _currentFontSize;
        private LocaleInfo _currentLocale;
        private Tuple<Color, string> _currentNamedFontColor;

        private Settings()
        {
            Locales = new ObservableCollection<LocaleInfo>(JsonDeserializer.LoadFromJson<LocaleInfo>(LocaleFileName));
            NamedColors = new ObservableCollection<Tuple<Color, string>>() { new Tuple<Color, string>(Color.Black, "Black"), new Tuple<Color, string>(Color.Blue, "Blue"), new Tuple<Color, string>(Color.Brown, "Brown"),
                new Tuple<Color, string>( Color.Red, "Red"), new Tuple<Color, string>(Color.Violet, "Violet"), new Tuple<Color, string>(Color.DeepPink, "Pink") };
            FontSizes = new ObservableCollection<double>() { 14, 15, 16, 17 };
            CurrentLocale = Locales.First();
            CurrentNamedFontColor = NamedColors.First();
            CurrentFontSize = FontSizes.First();
        }

        public static Settings Instance
        {
            get
            {
                return _instance ?? (_instance = new Settings());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<LocaleInfo> Locales { get; private set; }

        public ObservableCollection<double> FontSizes { get; private set; }

        public ObservableCollection<Tuple<Color, string>> NamedColors { get; private set; }

        public LocaleInfo CurrentLocale
        {
            get => _currentLocale;
            set
            {
                _currentLocale = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentLocale)));
                //TODO uncomment
                //Resx.AppResources.Culture = new CultureInfo(value.Locale);
            }
        }

        public List<string> FontSizeNames
        {
            get
            {
                return Enum.GetNames(typeof(NamedSize)).Select(b => b.SplitCamelCase()).ToList();
            }
        }

        public double CurrentFontSize
        {
            get => _currentFontSize;
            set
            {
                _currentFontSize = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentFontSize)));
            }
        }

        public Tuple<Color, string> CurrentNamedFontColor
        {
            get => _currentNamedFontColor;
            set
            {
                _currentNamedFontColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentNamedFontColor)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentFontColor)));
            }
        }

        public Color CurrentFontColor
        {
            get => CurrentNamedFontColor.Item1;
            set
            {
                CurrentNamedFontColor = NamedColors.First((t) => t.Item1 == value);
            }
        }
    }
}
