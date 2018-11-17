﻿using System.Collections.ObjectModel;
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

            TargetsCollection = new ObservableCollection<TargetViewModel>
                                {
                                    new TargetViewModel(60, 100, 20)
                                };
            NoisesCollection = new ObservableCollection<NoiseViewModel>();

            Task.Run(LaunchLineScanning);
        }

        public ObservableCollection<TargetViewModel> TargetsCollection { get; }
        public ObservableCollection<NoiseViewModel> NoisesCollection { get; }

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