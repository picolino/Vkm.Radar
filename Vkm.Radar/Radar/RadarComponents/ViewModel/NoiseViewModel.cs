using System.Collections.Generic;
using System.Windows.Input;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    internal class NoiseViewModel : RadarComponentBase, IDetectableComponent, IPositionalComponent
    {
        public NoiseViewModel(double azimuth, double opacity) : base(azimuth, opacity)
        {
        }

        public NoiseViewModel(double azimuth, int count, double opacity) : base(azimuth, opacity)
        {
            Count = count;
        }

        public int Count { get; }

        public IEnumerable<NoiseViewModel> GenerateNoisesCollection()
        {
            var beginAzimuth = Azimuth - Count / 2.0;
            var endAzimuth = Azimuth + Count / 2.0;

            for (var a = beginAzimuth; a < endAzimuth; a += 0.5)
            {
                yield return new NoiseViewModel(a, Opacity);
            }
        }

        public double PosTop => Constants.RadarCenter.Y;
        public double PosLeft => Constants.RadarCenter.X;
    }
}