using System;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    internal class StructuralRadialComponentViewModel : NoiseViewModel, IStructuralComponent
    {
        public StructuralRadialComponentViewModel(double azimuth, double opacity) : base(azimuth, opacity)
        {
        }
    }
}