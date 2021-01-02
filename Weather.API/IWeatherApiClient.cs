using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Common.Model;

namespace Weather.API
{
    public interface IWeatherApiClient
    {
        Task<CurrentWeatherResponse> CurrentWeather();

        Task<List<ForecastResponse>> Forecast();
    }
}