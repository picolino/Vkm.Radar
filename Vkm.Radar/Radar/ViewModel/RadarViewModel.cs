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

            ScanLine = new ScanLineViewModel(0);
            Components = new ObservableCollection<IPositionalComponent>();

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Components.Add(ScanLine);

            AddTarget(120, 230, 10);
            AddTarget(130, 100, 10);
            AddTarget(20, 90, 10);
            AddTarget(20, 120, 10);
            AddTarget(230, 248, 10);
            AddTarget(359, 110, 10);

            AddNoise(0, 8);

            DetectableComponents = new LinkedList<IDetectableComponent>(Components.OfType<IDetectableComponent>().OrderBy(dc => dc.Azimuth));
            detectableComponent = DetectableComponents.First;
        }

        public void AddTarget(double azimuth, double range, double width)
        {
            Components.Add(new TargetViewModel(azimuth, range, width));
        }

        public void AddNoise(double azimuth, int width)
        {
            var noises = new NoiseViewModel(azimuth, width).GenerateNoisesCollection();
            foreach (var noise in noises)
            {
                Components.Add(noise);
            }
        }

        private void OnLoaded()
        {
            ScanLineTimer.Start();
        }

        private void ScanLineMove(object sender, ElapsedEventArgs e)
        {
            CheckTargetByScanLine();
            ScanLineDoStep();
        }

        private void ScanLineDoStep()
        {
            if (ScanLine.Azimuth >= 360)
            {
                ScanLine.Azimuth = 0;
            }
            else
            {
                ScanLine.Azimuth += 1;
            }
        }

        private void CheckTargetByScanLine()
        {
            //Статья с видами индикаторов рлс при постановке различных видов помех: https://studfiles.net/preview/1430298/page:8/

            if (detectableComponent != null && Math.Abs(ScanLine.Azimuth - detectableComponent.Value.Azimuth) < 0.1)
            {
                detectableComponent.Value.WhenDetected();
                detectableComponent = detectableComponent.Next ?? DetectableComponents.First;
                if (detectableComponent?.Previous?.Value.Azimuth == detectableComponent?.Value?.Azimuth)
                {
                    CheckTargetByScanLine();
                }
            }
        }
    }
}