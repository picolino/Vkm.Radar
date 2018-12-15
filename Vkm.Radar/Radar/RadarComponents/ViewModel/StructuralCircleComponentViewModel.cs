
namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    internal class StructuralCircleComponentViewModel : RadarComponentBase, IStructuralComponent, IDetectableComponent, IPositionalComponent
    {
        public double Radius
        {
            get { return GetProperty(() => Radius); }
            set { SetProperty(() => Radius, value * 2); }
        }

        public StructuralCircleComponentViewModel(double azimuth, double opacity, double radius) : base(azimuth, opacity)
        {
            Radius = radius;
        }

        public double PosTop => Constants.RadarCenterY - Radius / 2;
        public double PosLeft => Constants.RadarCenterX - Radius / 2;
    }
}