using System.Collections.Generic;
using System.Threading.Tasks;

namespace Weather.API
{
    public interface IWeatherApiClient
    {
        Task<CurrentWeatherResponse> CurrentWeather();

        Task<List<ForecastResponse>> Forecast();
    }
}