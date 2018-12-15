using DevExpress.Mvvm;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    internal abstract class RadarComponentBase : ViewModelBase, IRadarComponent
    {
        private readonly double internalOpacityMultiplier;

        protected RadarComponentBase(double azimuth, double opacity)
        {
            Azimuth = azimuth;
            Opacity = internalOpacityMultiplier = opacity;
        }

        public double Azimuth
        {
            get { return GetProperty(() => Azimuth); }
            set
            {
                value = value % 360;

                if (value < 0)
                {
                    value = 360 + value;
                }

                SetProperty(() => Azimuth, value);
            }
        }

        public double Opacity
        {
            get { return GetProperty(() => Opacity); }
            set { SetProperty(() => Opacity, value * internalOpacityMultiplier); }
        }
    }
}