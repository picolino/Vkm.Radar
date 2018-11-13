using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace Vkm.Radar.Radar.ViewModel
{
    public class RadarViewModel : ViewModelBase
    {
        public RadarViewModel()
        {
            LineRotationDegrees = 0;
            Task.Run(LaunchLineRotation);
        }

        public double LineRotationDegrees
        {
            get { return GetProperty(() => LineRotationDegrees); }
            set { SetProperty(() => LineRotationDegrees, value); }
        }

        private async Task LaunchLineRotation()
        {
            while (true)
            {
                await Task.Delay(1);
                LineRotationDegrees += 1;
                if (LineRotationDegrees > 360)
                {
                    LineRotationDegrees = 0;
                }
            }
        }
    }
}