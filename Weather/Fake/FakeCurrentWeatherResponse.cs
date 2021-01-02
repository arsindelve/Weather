using System;
using Weather.API;

namespace Weather.Fake
{
    internal class FakeCurrentWeatherResponse
        : CurrentWeatherResponse
    {
        private readonly Random _r = new Random();

        public FakeCurrentWeatherResponse()
        {
            Temperature = Convert.ToDouble(_r.Next(20, 120));
            WindSpeed = Convert.ToDouble(_r.Next(0, 40));

        }
    }
}