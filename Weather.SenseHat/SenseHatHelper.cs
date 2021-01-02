using System.Threading.Tasks;
using Emmellsoft.IoT.Rpi.SenseHat;

namespace Weather.SenseHat
{
    internal static class SenseHatHelper
    {
        public static async Task<ISenseHat> GetSenseHat()
        {
            ISenseHat senseHat = null;

            try
            {
                senseHat = await SenseHatFactory.GetSenseHat();
            }
            catch
            {
                return null;
            }

            return senseHat;
        }
    }
}
