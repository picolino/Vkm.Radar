using System;
using System.Windows.Input;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    internal class TargetViewModel : RadarComponentBase, IPositionalComponent, IDetectableComponent
    {
        public ICommand TargetDetected { get; set; }

        public TargetViewModel(double azimuth, double range, double length, double thickness) : base(azimuth)
        {
            Range = range;
            Length = InitialLength = length;
            Thickness = thickness;
        }

        public double Range
        {
            get { return GetProperty(() => Range); }
            set { SetProperty(() => Range, value); }
        }

        public double InitialLength { get; }
        public double Length
        {
            get { return GetProperty(() => Length); }
            set { SetProperty(() => Length, Range * value / 100); }
        }

        public double Thickness
        {
            get { return GetProperty(() => Thickness); }
            set { SetProperty(() => Thickness, value); }
        }

        public double PosTop => Range * Math.Sin(Azimuth / 180d * Math.PI) + Constants.RadarCenterY;
        public double PosLeft => Range * Math.Cos(Azimuth / 180d * Math.PI) + Constants.RadarCenterX;

        public void WhenDetected(double opacityMultiplier)
        {
            TargetDetected.Execute(opacityMultiplier);
        }
    }
}