using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows.Input;
using DevExpress.Mvvm;
using Vkm.Radar.Radar.RadarComponents.ViewModel;

namespace Vkm.Radar.Radar.ViewModel
{
    public class RadarViewModel : ViewModelBase
    {
        private ScanLineViewModel ScanLine { get; }
        private Timer ScanLineTimer { get; }

        public ObservableCollection<IPositionalComponent> Components { get; }
        private LinkedList<IDetectableComponent> DetectableComponents { get; set; }

        public ICommand LoadedCommand { get; set; }

        private LinkedListNode<IDetectableComponent> detectableComponent;

        public RadarViewModel()
        {
            LoadedCommand = new DelegateCommand(OnLoaded);

            ScanLineTimer = new Timer(10);
            ScanLineTimer.Elapsed += ScanLineMove;

            ScanLine = new ScanLineViewModel();
            Components = new ObservableCollection<IPositionalComponent>();

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Components.Add(ScanLine);

            Components.Add(new TargetViewModel(120, 230, 10));
            Components.Add(new TargetViewModel(130, 100, 10));
            Components.Add(new TargetViewModel(20, 90, 10));
            Components.Add(new TargetViewModel(60, 230, 10));
            Components.Add(new TargetViewModel(30, 110, 10));
            Components.Add(new TargetViewModel(230, 240, 10));
            Components.Add(new TargetViewModel(0, 90, 10));

            DetectableComponents = new LinkedList<IDetectableComponent>(Components.OfType<IDetectableComponent>().OrderBy(dc => dc.Azimuth));

            detectableComponent = DetectableComponents.First;

            UpdateNextDetectableComponent();
        }

        private void OnLoaded()
        {
            ScanLineTimer.Start();
        }

        private void ScanLineMove(object sender, ElapsedEventArgs e)
        {
            ScanLineDoStep();
            CheckTargetByScanLine();
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

        private void CheckTargetByScanLine()
        {
            //Статья с видами индикаторов рлс при постановке различных видов помех: https://studfiles.net/preview/1430298/page:8/
            // TODO: Обнаружение цели
            // TODO: Обнаружение ложных целей
            // TODO: Обнаружение помехи

            if (detectableComponent != null && Math.Abs(ScanLine.LineAzimuth - detectableComponent.Value.Azimuth) < 0.1)
            {
                detectableComponent.Value.WhenDetected();
                UpdateNextDetectableComponent();
            }
        }

        private void UpdateNextDetectableComponent()
        {
            detectableComponent = detectableComponent.Next;
        }
    }
}