using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI;
using Emmellsoft.IoT.Rpi.SenseHat;
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

            return new InsideEnvironment
            {
                Humidity = s.Humidity,
                Temperature = s.Temperature,
                Pressure = s.Pressure,
            };
        }
    }

    public static class Lights
    {
        private static readonly Random Random = new Random();

        public static async Task Temperature(double temperature)
        {
            var senseHat = await SenseHatHelper.GetSenseHat();
            
            if (senseHat == null)
                return;

            var c = Common.ColorHelper.ColorFromTemperature(temperature);
            var c2 = Color.FromArgb(c.A, c.R, c.G, c.B);

            FillDisplayWithSolidColor(senseHat, c2);
        }

        public static async Task Disco()
        {
            var senseHat = await SenseHatHelper.GetSenseHat();

            if (senseHat == null)
                return;

            Action a = () => FillDisplayWithRandomColor(senseHat);
            await ExecutionHelper.RunTaskForTimespan(TimeSpan.FromSeconds(5), a);
        }

        private static void FillDisplayWithSolidColor(ISenseHat senseHat, Color c2)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Color pixel = c2;
                    senseHat.Display.Screen[x, y] = pixel;
                }
            }

            senseHat.Display.Update();
            Thread.Sleep(TimeSpan.FromMilliseconds(50));
        }

        private static void FillDisplayWithRandomColor(ISenseHat senseHat)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Color pixel = Color.FromArgb(
                        255,
                        (byte) Random.Next(256),
                        (byte) Random.Next(256),
                        (byte) Random.Next(256));

                    senseHat.Display.Screen[x, y] = pixel;
                }
            }

            senseHat.Display.Update();
            Thread.Sleep(TimeSpan.FromMilliseconds(50));
        }
    }
}
