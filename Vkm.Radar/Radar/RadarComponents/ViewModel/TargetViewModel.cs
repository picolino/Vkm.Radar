using System;
using DevExpress.Mvvm;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    public class TargetViewModel : ViewModelBase, IPositionalComponent
    {
        [Obsolete("Needs for designer only")]
        public TargetViewModel()
        {

        }

        public TargetViewModel(double azimuth, double range, double width)
        {
            Azimuth = azimuth;
            Range = range;
            Width = width;
        }

        public double Azimuth
        {
            get { return GetProperty(() => Azimuth); }
            set { SetProperty(() => Azimuth, value); }
        }

        public double Range
        {
            get { return GetProperty(() => Range); }
            set { SetProperty(() => Range, value); }
        }

        public double Width
        {
            get { return GetProperty(() => Width); }
            set { SetProperty(() => Width, value); }
        }

        public int PosTop => Convert.ToInt32(Range * Math.Sin(Azimuth / 180d *Math.PI) + Constants.RadarCenterY);
        public int PosLeft => Convert.ToInt32(Range * Math.Cos(Azimuth / 180d * Math.PI) + Constants.RadarCenterX);
    }
}