using System;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    internal class TargetViewModel : RadarComponentBase, IPositionalComponent, IDetectableComponent
    {
        private readonly double internalLength;

        public TargetViewModel(double azimuth, double range, double length, double opacity) : base(azimuth, opacity)
        {
            Range = range;
            Length = internalLength = length;
        }

        public double Range
        {
            get { return GetProperty(() => Range); }
            set { SetProperty(() => Range, value); }
        }

        public double Length
        {
            get { return GetProperty(() => Length); }
            set { SetProperty(() => Length, value * internalLength * 0.02); }
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