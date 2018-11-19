using System;
using System.Windows.Input;
using DevExpress.Mvvm;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    public class TargetViewModel : RadarComponentBase, IPositionalComponent, IDetectableComponent
    {
        public ICommand TargetDetected { get; set; }

        public TargetViewModel(double azimuth, double range, double width) : base(azimuth)
        {
            Range = range;
            Width = width;
        }

        public double Range
        {
            get { return GetProperty(() => Range); }
            set { SetProperty(() => Range, value); }
        }

        public double Width
        {
            get { return GetProperty(() => Width); }
            set { SetProperty(() => Width, value / 2); }
        }

        public double PosTop => Range * Math.Sin(Azimuth / 180d * Math.PI) + Constants.RadarCenterY;
        public double PosLeft => Range * Math.Cos(Azimuth / 180d * Math.PI) + Constants.RadarCenterX;

        public void WhenDetected()
        {
            TargetDetected.Execute(null);
        }
    }
}