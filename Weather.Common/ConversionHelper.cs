using Windows.Graphics.Imaging;

namespace Weather.Common
{
    public static class ConversionHelper
    {
        public static double? ConvertToFarenheit(this double? celcius)
        {
            return celcius * 9 / 5 + 32;
        }

        public static double ConvertToInchesMercury(this double millibars)
        {
            return millibars * 0.0295301;
        }
    }
}
