using System.Collections.Generic;
using System.Windows.Input;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    internal class NoiseViewModel : RadarComponentBase, IDetectableComponent, IPositionalComponent
    {
        private NoiseViewModel(double azimuth, double opacityMultiplier) : base(azimuth, opacityMultiplier)
        {
        }

        public NoiseViewModel(double azimuth, int count, double opacityMultiplier) : base(azimuth, opacityMultiplier)
        {
            Count = count;
        }

        public int Count { get; }

        public ICommand TargetDetected { get; set; }

        public void WhenDetected(double opacityMultiplier)
        {
            TargetDetected.Execute(opacityMultiplier);
        }

        public IEnumerable<NoiseViewModel> GenerateNoisesCollection()
        {
            var beginAzimuth = Azimuth - Count / 2.0;
            var endAzimuth = Azimuth + Count / 2.0;

            for (var a = beginAzimuth; a < endAzimuth; a += 0.5)
            {
                yield return new NoiseViewModel(a, OpacityMultiplier);
            }
        }

        public double PosTop => Constants.RadarCenter.Y;
        public double PosLeft => Constants.RadarCenter.X;
    }
}