using System.Threading.Tasks;
using Weather.API;

namespace Weather.ViewModel
{
    public class WeatherViewModel : BaseViewModel
    {
        public OutsideWeatherViewModel OutsideWeather { get; private set; }

        public InsideViewModel InsideEnvironment { get; private set; }

        public WeatherViewModel()
        {
            OutsideWeather = new OutsideWeatherViewModel(new WeatherApiClient());
            InsideEnvironment = new InsideViewModel();
        }

        public async Task Initialize()
        {
            await OutsideWeather.Initialize();
            await InsideEnvironment.Initialize();
        }
    }
}
