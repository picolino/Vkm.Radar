using System;
using System.Collections.Generic;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    internal class ScanLineViewModel : RadarComponentBase, IPositionalComponent
    {
        public ScanLineViewModel(double azimuth, double targetsThickness, double targetsLength, double stepDegrees, double opacity = 1) : base(azimuth, opacity)
        {
            TargetsThickness = targetsThickness;
            TargetsLength = targetsLength;
            StepDegrees = stepDegrees;
        }

        public IEnumerable<TargetViewModel> RadarTargets { private get; set; }

        public double StepDegrees
        {
            get { return GetProperty(() => StepDegrees); }
            set { SetProperty(() => StepDegrees, value); }
        }

        public double TargetsThickness
        {
            get { return GetProperty(() => TargetsThickness); }
            set { SetProperty(() => TargetsThickness, value, OnTargetsThicknessChanged); }
        }

        private void OnTargetsThicknessChanged()
        {
            if (RadarTargets is null)
            {
                return;
            }

            foreach (var target in RadarTargets)
            {
                target.Thickness = TargetsThickness;
            }
        }

        public double TargetsLength
        {
            get { return GetProperty(() => TargetsLength); }
            set { SetProperty(() => TargetsLength, value, OnTargetsLengthChanged); }
        }

        private void OnTargetsLengthChanged()
        {
            if (RadarTargets is null)
            {
                return;
            }

            foreach (var target in RadarTargets)
            {
                target.Length = TargetsLength;
            }
        }

        public double PosTop => Constants.RadarCenter.Y;
        public double PosLeft => Constants.RadarCenter.X;
    }
}