namespace WeatherApp.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using WeatherApp.Helper;
    using WeatherApp.Models;
    using Xamarin.Forms;
    using Xamarin.Forms.Internals;

    public class MainPageViewModel : INotifyPropertyChanged
    {
        private const string FileName = "Resources.cities.json";

        private ObservableCollection<CityViewModel> _cities;
        private Command _loadCitiesCommand;
        private bool _isBusy;

        public MainPageViewModel()
        {
            _cities = new ObservableCollection<CityViewModel>()
            {
                new CityViewModel(1, "name", new Coordinates(0, 0), "asd", "http://bipbap.ru/wp-content/uploads/2017/04/72fqw2qq3kxh.jpg")
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<CityViewModel> Cities
        {
            get => _cities;
            set
            {
                if (_cities == value)
                    return;

                _cities = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cities)));
            }
        }
        public Command LoadCitiesCommand
        {
            get
            {
                return _loadCitiesCommand ??
                  (_loadCitiesCommand = new Command(async () =>
                  {
                      await ExecuteLoadCitiesCommand();
                  }, () =>
                  {
                      return !IsBusy;
                  }));
            }
        }
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy == value)
                    return;

                _isBusy = !_isBusy;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsBusy)));
            }
        }

        private async Task ExecuteLoadCitiesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            LoadCitiesCommand.ChangeCanExecute();

            _cities.Clear();
            var loadedObjects = LoadCities();
            loadedObjects.ForEach((obj) => { _cities.Add(obj); });

            IsBusy = false;
            LoadCitiesCommand.ChangeCanExecute();
        }

        private IEnumerable<CityViewModel> LoadCities()
        {
            var readObjects = JsonDeserializer.LoadFromJson<City>(FileName);
            return readObjects.Select((city) => { return new CityViewModel(city); });
        }
    }
}
