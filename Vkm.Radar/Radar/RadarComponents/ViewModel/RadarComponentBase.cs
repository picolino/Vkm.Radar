using DevExpress.Mvvm;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    internal abstract class RadarComponentBase : ViewModelBase, IRadarComponent
    {
        protected RadarComponentBase(double azimuth, double opacityMultiplier)
        {
            Azimuth = azimuth;
            OpacityMultiplier = opacityMultiplier;
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
        
        public double OpacityMultiplier { get; }
    }
}