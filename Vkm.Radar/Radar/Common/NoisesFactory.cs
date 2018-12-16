using System.Collections.Generic;
using Vkm.Radar.Radar.Common.Interfaces;
using Vkm.Radar.Radar.RadarComponents.ViewModel;

namespace Vkm.Radar.Radar.Common
{
    internal class NoisesFactory : INoisesFactory
    {
        public IEnumerable<NoiseViewModel> GenerateNoisesCollection(double azimuth, int count, double opacity, double opacityMultiplier)
        {
            var beginAzimuth = azimuth - count / 2.0;
            var endAzimuth = azimuth + count / 2.0;

            for (var a = beginAzimuth; a < endAzimuth; a += 0.2)
            {
                yield return new NoiseViewModel(a, opacity)
                             {
                                 Opacity = opacityMultiplier
                             };
            }
        }
    }
}