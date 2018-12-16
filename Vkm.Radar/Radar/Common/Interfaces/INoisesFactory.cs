using System.Collections.Generic;
using Vkm.Radar.Radar.RadarComponents.ViewModel;

namespace Vkm.Radar.Radar.Common.Interfaces
{
    internal interface INoisesFactory
    {
        IEnumerable<NoiseViewModel> GenerateNoisesCollection(double azimuth, int count, double opacity, double opacityMultiplier);
    }
}