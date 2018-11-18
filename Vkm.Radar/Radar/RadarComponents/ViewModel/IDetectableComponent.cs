namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    public interface IDetectableComponent
    {
        void WhenDetected();
        double Azimuth { get; set; }
    }
}