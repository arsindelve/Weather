using System.Drawing;

namespace Weather.Common
{
    public static class ColorHelper
    {
        public static Color ColorFromTemperature(double temperature)
        {
            if (temperature < 30)
                return Color.Blue;

            if (temperature < 40)
                return Color.CornflowerBlue;

            if (temperature < 50)
                return Color.Aqua;

            if (temperature < 60)
                return Color.Yellow;

            if (temperature < 70)
                return Color.YellowGreen;

            if (temperature < 80)
                return Color.GreenYellow;

            if (temperature < 90)
                return Color.Orange;

            if (temperature < 100)
                return Color.DarkOrange;

            return Color.Red;
        }
    }
}
