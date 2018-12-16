using System;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    internal class TargetViewModel : RadarComponentBase, IPositionalComponent, IDetectableComponent
    {
        public TargetViewModel(double azimuth, double range, double length, double thickness, double opacity) : base(azimuth, opacity)
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
            set { SetProperty(() => Length, value * 0.02); }
        }

        public double Thickness
        {
            get { return GetProperty(() => Thickness); }
            set { SetProperty(() => Thickness, value); }
        }

        public double PosTop => Constants.RadarCenterY;
        public double PosLeft => Constants.RadarCenterX;
    }
}