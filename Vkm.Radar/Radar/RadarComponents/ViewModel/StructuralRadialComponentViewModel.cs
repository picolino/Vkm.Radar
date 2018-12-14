using System;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    internal class StructuralRadialComponentViewModel : NoiseViewModel, IStructuralComponent
    {
        public bool IsAlwaysDetectable => false;

        public StructuralRadialComponentViewModel(double azimuth, double opacityMultiplier) : base(azimuth, opacityMultiplier)
        {
        }

        public StructuralRadialComponentViewModel(double azimuth, int count, double opacityMultiplier) : base(azimuth, count, opacityMultiplier)
        {
            throw new NotSupportedException();
        }
    }
}