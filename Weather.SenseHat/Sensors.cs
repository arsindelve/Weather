using System.Threading.Tasks;
using Weather.Common;
using Weather.Common.Model;

namespace Weather.SenseHat
{
    public static class Sensors
    {
        public static async Task<InsideEnvironment> GetEnvironment()
        {
            var senseHat = await SenseHatHelper.GetSenseHat();
            
            if (senseHat == null)
                return null;

            var s = senseHat.Sensors;
            s.HumiditySensor.Update();
            s.PressureSensor.Update();

            return new InsideEnvironment
            {
                Humidity = s.Humidity,
                Temperature = s.Temperature.ConvertToFarenheit(),
                Pressure = s.Pressure.GetValueOrDefault().ConvertToInchesMercury(),
            };
        }
    }
}