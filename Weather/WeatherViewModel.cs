using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Windows.System.Threading;
using Windows.UI.Xaml;
using Weather.API;

namespace Weather
{
    public class WeatherViewModel : BaseViewModel
    {
        private readonly IWeatherApiClient _client;

        private CurrentWeatherResponse _currentWeather;
        private ObservableCollection<ForecastResponse> _forecast;
        private DateTime _updated;
        private Timer _timer;

        public CurrentWeatherResponse CurrentWeather
        {
            get => _currentWeather;
            private set
            {
                _currentWeather = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ForecastResponse> Forecast
        {
            get => _forecast;
            private set { _forecast = value; OnPropertyChanged(); }
        }

        public DateTime Updated
        {
            get => _updated;
            private set { _updated = value; OnPropertyChanged(); }
        }

        public WeatherViewModel(IWeatherApiClient client)
        {
            _client = client;
        }

     

        private async Task GetForecast()
        {
            Forecast = new ObservableCollection<ForecastResponse>(await _client.Forecast());
            CurrentWeather = await _client.CurrentWeather();
            Updated = DateTime.Now;
        }

        public async Task Initialize()
        {
            await GetForecast();
        }
    }
}
