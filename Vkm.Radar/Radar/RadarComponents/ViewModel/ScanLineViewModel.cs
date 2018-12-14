using System;
using System.Collections.Generic;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    internal class ScanLineViewModel : RadarComponentBase, IPositionalComponent
    {
        public ScanLineViewModel(double azimuth, double pulseDuration, double pulseLength, double opacityMultiplier = 1) : base(azimuth, opacityMultiplier)
        {
            PulseDuration = pulseDuration;
            PulseLength = pulseLength;
        }

        public IEnumerable<TargetViewModel> RadarTargets { private get; set; }

        public double PulseDuration
        {
            get { return GetProperty(() => PulseDuration); }
            set { SetProperty(() => PulseDuration, value, OnPulseDurationChanged); }
        }

        private void OnPulseDurationChanged()
        {
            if (RadarTargets is null)
            {
                return;
            }

            foreach (var target in RadarTargets)
            {
                target.Thickness = PulseDuration;
            }
        }

        public double PulseLength
        {
            get { return GetProperty(() => PulseLength); }
            set { SetProperty(() => PulseLength, value, OnPulseLengthChanged); }
        }

        private void OnPulseLengthChanged()
        {
            if (RadarTargets is null)
            {
                return;
            }

            foreach (var target in RadarTargets)
            {
                target.Length = target.InitialLength * PulseLength;
            }
        }

        public double PosTop => Constants.RadarCenter.Y;
        public double PosLeft => Constants.RadarCenter.X;
    }
}