using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Weather.API;

namespace Weather
{
    public class WeatherViewModel : BaseViewModel
    {
        private readonly IWeatherApiClient _client;

        private CurrentWeatherResponse _currentWeather;
        private ObservableCollection<ForecastResponse> _forecast;

        public CurrentWeatherResponse CurrentWeather
        {
            get => _currentWeather;
            set
            {
                _currentWeather = value; 
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ForecastResponse> Forecast
        {
            get => _forecast;
            set { _forecast = value; OnPropertyChanged(); }
        }

        public WeatherViewModel(IWeatherApiClient client)
        {
            _client = client;
        }

        internal async Task GetForecast()
        {
            Forecast = new ObservableCollection<ForecastResponse>(await _client.Forecast());
            CurrentWeather = await _client.CurrentWeather();
        }


        public async Task Initialize()
        {
            await GetForecast();
        }
    }
}
