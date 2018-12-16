namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    internal class NoiseViewModel : RadarComponentBase, IDetectableComponent, IPositionalComponent
    {
        public NoiseViewModel(double azimuth, double opacity) : base(azimuth, opacity)
        {
        }

        public double PosTop => Constants.RadarCenter.Y;
        public double PosLeft => Constants.RadarCenter.X;
    }
}