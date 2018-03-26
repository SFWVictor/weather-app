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
            _cities = new ObservableCollection<CityViewModel>();
            LoadCities();
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

            LoadCities();

            IsBusy = false;
            LoadCitiesCommand.ChangeCanExecute();
        }

        private void LoadCities()
        {
            _cities.Clear();
            var loadedObjects = GetCities();
            loadedObjects.ForEach((obj) => { _cities.Add(obj); });
        }

        private IEnumerable<CityViewModel> GetCities()
        {
            var readObjects = JsonDeserializer.LoadFromJson<City>(FileName);
            return readObjects.Select((city) => { return new CityViewModel(city); });
        }
    }
}
