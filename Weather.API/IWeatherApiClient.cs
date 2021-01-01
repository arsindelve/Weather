using System.Collections.Generic;

namespace Weather.API
{
    public interface IWeatherApiClient
    {
        CurrentWeatherResponse CurrentWeather();

        List<ForecastResponse> Forecast();
    }
}