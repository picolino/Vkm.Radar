using System;
using System.Collections.Generic;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    internal class ScanLineViewModel : RadarComponentBase, IPositionalComponent
    {
        public ScanLineViewModel(double azimuth, double pulseDuration) : base(azimuth)
        {
            PulseDuration = pulseDuration;
        }

        public IEnumerable<TargetViewModel> RadarComponents { private get; set; }

        public double PulseDuration
        {
            get { return GetProperty(() => PulseDuration); }
            set { SetProperty(() => PulseDuration, value, OnPulseDurationChanged); }
        }

        private void OnPulseDurationChanged()
        {
            if (RadarComponents is null)
            {
                return;
            }

            foreach (var target in RadarComponents)
            {
                target.Thickness = PulseDuration;
            }
        }

        public double PosTop => Constants.RadarCenter.Y;
        public double PosLeft => Constants.RadarCenter.X;
    }
}