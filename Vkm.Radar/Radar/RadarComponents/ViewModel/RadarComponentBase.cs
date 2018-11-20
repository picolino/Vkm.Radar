using System;
using DevExpress.Mvvm;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    public abstract class RadarComponentBase : ViewModelBase, IRadarComponent
    {
        protected RadarComponentBase(double azimuth)
        {
            Azimuth = azimuth;
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
    }
}