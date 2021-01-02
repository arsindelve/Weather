using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media.Animation;
using Emmellsoft.IoT.Rpi.SenseHat;

namespace Weather.SenseHat
{
    public class Lights
    {
        private static readonly Random Random = new Random();

        public static async Task Temperature(double temperature)
        {
            ISenseHat senseHat = await SenseHatFactory.GetSenseHat();
            Fill(senseHat, temperature);
        }
        
        public static async Task Disco()
        {
            ISenseHat senseHat = await SenseHatFactory.GetSenseHat();
         
            TaskFactory tf = new TaskFactory();
            await tf.StartNew(() =>
            {
                Stopwatch s = new Stopwatch();
                s.Start();
                while (s.Elapsed < TimeSpan.FromSeconds(5))
                {
                    Flash(senseHat);
                }

                s.Stop();

            });
        }

        private static void Fill(ISenseHat senseHat, double temperature)
        {
            var c = Common.ColorHelper.ColorFromTemperature(temperature);
            var c2 = Color.FromArgb(c.A, c.R, c.G, c.B);
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

        private static void Flash(ISenseHat senseHat)
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
