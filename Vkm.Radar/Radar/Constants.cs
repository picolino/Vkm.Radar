using System.Windows;

namespace Vkm.Radar.Radar
{
    internal class Constants
    {
        public const double RadarCenterX = 250;
        public const double RadarCenterY = 250;
        public const double RadarRadius = 250;

        public static double RadarDiameter => RadarRadius * 2;
        public static Point RadarCenter => new Point(RadarCenterX, RadarCenterY);
    }
}