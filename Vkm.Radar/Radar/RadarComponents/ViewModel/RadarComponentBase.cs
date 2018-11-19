using System;
using DevExpress.Mvvm;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    public abstract class RadarComponentBase : ViewModelBase, IRadarComponent
    {
        public RadarComponentBase(double azimuth)
        {
            Azimuth = azimuth;
        }

        public double Azimuth
        {
            get { return GetProperty(() => Azimuth); }
            set
            {
                if (value < 0 || value > 360)
                {
                    throw new ArgumentOutOfRangeException($"Value '{value}' is not acceptable for {nameof(Azimuth)} property. Please use double value from 0 to 360 instead.");
                }

                if (Math.Abs(value - 360.0) < 0)
                {
                    value = 0;
                }

                SetProperty(() => Azimuth, value);
            }
        }
    }
}