namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    internal class StructuralRadialComponent : NoiseViewModel, IStructuralComponent
    {
        public bool IsStructuralComponent => true;

        public StructuralRadialComponent(double azimuth, double opacityMultiplier) : base(azimuth, opacityMultiplier)
        {
        }

        public StructuralRadialComponent(double azimuth, int count, double opacityMultiplier) : base(azimuth, count, opacityMultiplier)
        {
        }
    }
}