using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using DevExpress.Mvvm;
using Vkm.Radar.Radar.RadarComponents.ViewModel;

namespace Vkm.Radar.Radar.ViewModel
{
    public class RadarViewModel : ViewModelBase
    {
        private ScanLineViewModel ScanLine { get; }
        private Timer Timer { get; }

        public ObservableCollection<IPositionalComponent> Components { get; }
        public ObservableCollection<IDetectableComponent> DetectableComponents { get; set; }

        private IDetectableComponent nextDetectableComponent;

        public RadarViewModel()
        {
            Timer = new Timer(10);
            Timer.Elapsed += ScanLineMove;

            ScanLine = new ScanLineViewModel();
            Components = new ObservableCollection<IPositionalComponent>();

            InitializeComponents();

            Timer.Start();
        }

        private void InitializeComponents()
        {
            Components.Add(ScanLine);
            Components.Add(new TargetViewModel(120, 230, 10));

            DetectableComponents = new ObservableCollection<IDetectableComponent>(Components.OfType<IDetectableComponent>());

            UpdateNextDetectableComponent();
        }

        private void ScanLineMove(object sender, ElapsedEventArgs e)
        {
            ScanLineDoStep();
            CheckTargetByScanLine();
        }

        private void CheckTargetByScanLine()
        {
            //Статья с видами индикаторов рлс при постановке различных видов помех: https://studfiles.net/preview/1430298/page:8/
            // TODO: Обнаружение цели
            // TODO: Обнаружение ложных целей
            // TODO: Обнаружение помехи

            if (nextDetectableComponent != null && Math.Abs(ScanLine.LineAzimuth - nextDetectableComponent.Azimuth) < 0.1)
            {
                nextDetectableComponent.WhenDetected();
                UpdateNextDetectableComponent();
            }
        }

        private void UpdateNextDetectableComponent()
        {
            nextDetectableComponent = DetectableComponents.OrderBy(dc => dc.Azimuth).FirstOrDefault(dc => dc.Azimuth > ScanLine.LineAzimuth);
        }

        private void ScanLineDoStep()
        {
            if (ScanLine.LineAzimuth >= 360)
            {
                ScanLine.LineAzimuth = 0;
                UpdateNextDetectableComponent();
            }
            else
            {
                ScanLine.LineAzimuth += 1;
            }
        }
    }
}