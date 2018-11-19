using System.Collections.Generic;
using System.Windows.Input;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    public class NoiseViewModel : ScanLineViewModel, IDetectableComponent
    {
        private NoiseViewModel(double azimuth) : base(azimuth)
        {
            
        }

        public NoiseViewModel(double azimuth, int count) : base(azimuth)
        {
            Count = count;
        }

        public int Count { get; }

        public ICommand TargetDetected { get; set; }

        public void WhenDetected()
        {
            TargetDetected.Execute(null);
        }

        public IEnumerable<NoiseViewModel> Initialize()
        {
            var beginAzimuth = Azimuth - Count / 2.0;
            var endAzimuth = Azimuth + Count / 2.0;

            for (var a = beginAzimuth; a < endAzimuth; a++)
            {
                yield return new NoiseViewModel(a);
            }
        }
    }
}