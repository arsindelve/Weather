using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI;
using Emmellsoft.IoT.Rpi.SenseHat;

namespace Weather.SenseHat
{
    public class Lights
    {
        private static readonly Random Random = new Random();

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
