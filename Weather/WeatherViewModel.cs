using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage.Search;
using Windows.System.Threading;
using Windows.UI.Core;
using Weather.API;
using Weather.Fake;
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
        private TimeSpan _nextRefresh;
        private const int ForecastRefreshMinutes = 45;

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
            private set
            {
                _forecast = value;
                OnPropertyChanged();
            }
        }

        public string DallasBackground
        {
            get => _dallasBackground;
            private set
            {
                _dallasBackground = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan NextRefresh
        {
            get => _nextRefresh;
            set
            {
                _nextRefresh = value; OnPropertyChanged();
            }
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
            NextRefresh = TimeSpan.FromMinutes(ForecastRefreshMinutes);

            InitializeForecastTimer();
            InitializeCountdownTimer();
        }

        private void InitializeCountdownTimer()
        {
            ThreadPoolTimer timer = ThreadPoolTimer.CreatePeriodicTimer(async (t) =>
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    UpdateCountdownTimer);
            }, TimeSpan.FromMilliseconds(500));
        }

        private void UpdateCountdownTimer()
        {
            NextRefresh = Updated.AddMinutes(ForecastRefreshMinutes) - DateTime.Now;
        }

        private void InitializeForecastTimer()
        {
            ThreadPoolTimer timer = ThreadPoolTimer.CreatePeriodicTimer(async (t) =>
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    async () => { await GetForecast(); });
            }, TimeSpan.FromMinutes(ForecastRefreshMinutes));
        }

        private async Task GetForecast()
        {
            Forecast = new ObservableCollection<ForecastResponse>(await _client.Forecast());
            CurrentWeather = await _client.CurrentWeather();

            //Forecast = new ObservableCollection<ForecastResponse>(FakeForecast.GetForecast());
            //CurrentWeather = new FakeCurrentWeatherResponse();

            Updated = DateTime.Now;
            DallasBackground = Updated.Hour >= 19 ? "Assets/night.jpg" : "Assets/day.jpg";

            await Lights.Disco();
            await Lights.Temperature(CurrentWeather.Temperature);
        }

        public async Task Initialize()
        {
            await GetForecast();
        }
    }
}
