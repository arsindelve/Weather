using System;
using Weather.Common.Model;

namespace Weather.Fake
{
    internal class FakeEnvironment : InsideEnvironment
    {
        private Random r = new Random();
        
        public FakeEnvironment()
        {
            Temperature = r.Next(65, 80);
            Humidity = r.Next(5, 5000);
            Pressure = r.Next(5, 5000);
        }
    }
}