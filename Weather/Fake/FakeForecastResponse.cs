using System;
using Weather.API;
using Weather.Common.Model;

namespace Weather.Fake
{
    internal class FakeForecastResponse
        : ForecastResponse
    {
        private readonly Random _r = new Random();

        public FakeForecastResponse()
        {
            Temperature = Convert.ToDouble(_r.Next(20, 120));
            WindSpeed = Convert.ToDouble(_r.Next(0, 40));

        }
    }
}