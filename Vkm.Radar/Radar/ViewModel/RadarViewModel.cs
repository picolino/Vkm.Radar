using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace Vkm.Radar.Radar.ViewModel
{
    public class RadarViewModel : ViewModelBase
    {
        public RadarViewModel()
        {
            LineAzimuth = 0;
            Task.Run(LaunchLineScanning);
        }

        public double LineAzimuth
        {
            get { return GetProperty(() => LineAzimuth); }
            set { SetProperty(() => LineAzimuth, value); }
        }

        private async Task LaunchLineScanning()
        {
            while (true)
            {
                await Task.Delay(1);
                LineAzimuth += 1;
                if (LineAzimuth > 360)
                {
                    LineAzimuth = 0;
                }
            }
        }
    }
}