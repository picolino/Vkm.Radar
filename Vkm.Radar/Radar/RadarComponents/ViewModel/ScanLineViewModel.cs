using System;
using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    public class ScanLineViewModel : ViewModelBase, IPositionalComponent
    {
        public ScanLineViewModel()
        {
            LineAzimuth = 0;
            
            Task.Run(LaunchLineScanning);
        }

        public int PosTop => Convert.ToInt32(Constants.RadarCenter.Y);
        public int PosLeft => Convert.ToInt32(Constants.RadarCenter.X);

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
            //Статья с видами индикаторов рлс при постановке различных видов помех: https://studfiles.net/preview/1430298/page:8/
            // TODO: Обнаружение цели
            // TODO: Обнаружение ложных целей
            // TODO: Обнаружение помехи
        }

        private void LineStep()
        {
            if (LineAzimuth > 360)
            {
                LineAzimuth = 0;
            }
            else
            {
                LineAzimuth += 1;
            }

        }
    }
}