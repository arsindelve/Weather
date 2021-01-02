using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.System.Threading;
using Windows.UI.Core;
using Weather.API;
using Weather.SenseHat;

namespace Weather
{
    public class WeatherViewModel : BaseViewModel
    {
        private readonly IWeatherApiClient _client;

        private CurrentWeatherResponse _currentWeather;
        private ObservableCollection<ForecastResponse> _forecast;
        private DateTime _updated;
        private string _dallasBackground;

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

        public string DallasBackground
        {
            get => _dallasBackground;
            set { _dallasBackground = value; OnPropertyChanged(); }
        }

        public DateTime Updated
        {
            get => _updated;
            private set { _updated = value; OnPropertyChanged(); }
        }

        public WeatherViewModel(IWeatherApiClient client)
        {
            _client = client;
            DallasBackground = "Assets/night.jpg";
            ThreadPoolTimer timer = ThreadPoolTimer.CreatePeriodicTimer(async (t) =>
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    async () =>
                    {
                        await GetForecast();
                    });

            }, TimeSpan.FromMinutes(1));
        }

        private async Task GetForecast()
        {
            //Forecast = new ObservableCollection<ForecastResponse>(await _client.Forecast());
            //CurrentWeather = await _client.CurrentWeather();

            Forecast = new ObservableCollection<ForecastResponse>();
            CurrentWeather = new CurrentWeatherResponse();
            
            Updated = DateTime.Now;

            DallasBackground = Updated.Hour >= 19 ? "Assets/night.jpg" : "Assets/day.jpg";
            await Lights.Disco();
        }

        public async Task Initialize()
        {
            await GetForecast();
        }
    }
}
