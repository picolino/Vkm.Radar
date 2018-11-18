using System;
using System.Timers;
using DevExpress.Mvvm;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    public class ScanLineViewModel : ViewModelBase, IPositionalComponent
    {
        private Timer timer;

        public ScanLineViewModel()
        {
            Initialize();
            timer.Start();
        }

        private void Initialize()
        {
            LineAzimuth = 0;

            timer = new Timer(10);
            timer.Elapsed += TimerOnElapsed;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            LineStep();
            CheckTarget();
        }

        public double PosTop => Constants.RadarCenter.Y;
        public double PosLeft => Constants.RadarCenter.X;

        public double LineAzimuth
        {
            get { return GetProperty(() => LineAzimuth); }
            set { SetProperty(() => LineAzimuth, value); }
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