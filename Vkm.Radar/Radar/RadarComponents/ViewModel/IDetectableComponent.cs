using System.Windows.Input;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    public interface IDetectableComponent : IRadarComponent
    {
        ICommand TargetDetected { get; set; }
        void WhenDetected();
    }
}