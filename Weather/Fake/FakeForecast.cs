using System;
using System.Collections.Generic;
using Weather.API;

namespace Weather.Fake
{
    internal class FakeForecast
    {
        public static List<ForecastResponse> GetForecast()
        {
            var forecasts = new List<ForecastResponse>();
            
            for (int counter = 0; counter < 30; counter++)
            {
                var f = new FakeForecastResponse { TimeUniversal = DateTime.UtcNow.AddHours(counter * 3) };
                forecasts.Add(f);
            }
            return forecasts;
        }
    }
}