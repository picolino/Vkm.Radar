using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using Vkm.Radar.Radar.RadarComponents.ViewModel;

namespace Vkm.Radar.Radar.ViewModel
{
    public class RadarViewModel : ViewModelBase
    {
        public RadarViewModel()
        {
            LineAzimuth = 0;
            TargetsCollection = new ObservableCollection<TargetViewModel>();

            Task.Run(LaunchLineScanning);
        }

        public ObservableCollection<TargetViewModel> TargetsCollection { get; }

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
                LineStep();
                CheckTarget();
            }
        }

        private void CheckTarget()
        {
            // TODO: Обнаружение цели
            // TODO: Обнаружение ложных целей
            // TODO: Обнаружение помехи
        }

        private void LineStep()
        {
            LineAzimuth += 1;
            if (LineAzimuth > 360)
            {
                LineAzimuth = 0;
            }
        }
    }
}