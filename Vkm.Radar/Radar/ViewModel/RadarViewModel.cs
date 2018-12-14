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

        public RadarViewModel(double scanLineMoveInterval, bool useStructuralComponents)
        {
            LoadedCommand = new DelegateCommand(OnLoaded);

            ScanLineTimer = new Timer(scanLineMoveInterval);
            ScanLineTimer.Elapsed += ScanLineMove;

            ScanLine = new ScanLineViewModel(0, 2, 1);
            Components = new ObservableCollection<IPositionalComponent>();

            InitializeComponents(useStructuralComponents);

            ScanLine.RadarTargets = Components.OfType<TargetViewModel>();

            OpacityMultiplier = 1;
        }

        private void InitializeComponents(bool useStructuralComponents)
        {
            UseDefaultStructuralComponents = useStructuralComponents;
            Components.Add(ScanLine);
            DetectableComponents = new LinkedList<IDetectableComponent>();
        }

        public void AddTarget(double azimuth, double range, double width, double opacityMultiplier = 1)
        {
            var newTarget = new TargetViewModel(azimuth, range, width, ScanLine.PulseDuration, opacityMultiplier);
            AddComponent(newTarget);
        }

        public void AddNoise(double azimuth, int width, double opacityMultiplier = 1)
        {
            var noises = new NoiseViewModel(azimuth, width, opacityMultiplier).GenerateNoisesCollection();
            AddComponents(noises);
        }

        private void AddDefaultStructuralComponents()
        {
            throw new NotImplementedException();
        }

        private void AddComponents<T>(IEnumerable<T> components) where T : IPositionalComponent, IDetectableComponent
        {
            foreach (var component in components)
            {
                AddComponent(component);
            }
        }

        private void AddComponent<T>(T component) where T : IPositionalComponent, IDetectableComponent
        {
            Components.Add(component);
            if (DetectableComponents?.Count == 0)
            {
                DetectableComponents.AddFirst(component);
                detectableComponent = DetectableComponents.First;
            }
            else
            {
                var beforeNode = DetectableComponents.FindLast(DetectableComponents.LastOrDefault(c => c.Azimuth <= component.Azimuth));
                if (beforeNode != null)
                {
                    DetectableComponents.AddAfter(beforeNode, component);
                }
                var afterNode = DetectableComponents.Find(DetectableComponents.FirstOrDefault(c => c.Azimuth > component.Azimuth));
                if (afterNode != null)
                {
                    DetectableComponents.AddBefore(afterNode, component);
                    if (detectableComponent == afterNode)
                    {
                        detectableComponent = DetectableComponents.Find(component);
                    }
                }
            }
        }

        public void ClearAllComponents()
        {
            for (var i = Components.Count - 1; i > 0; i--)
            {
                Components.RemoveAt(i);
            }
            DetectableComponents.Clear();
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

            if (detectableComponent != null && Math.Abs(ScanLine.Azimuth - detectableComponent.Value.Azimuth) < 1)
            {
                
                detectableComponent.Value.WhenDetected(OpacityMultiplier);
                detectableComponent = detectableComponent.Next ?? DetectableComponents.First;
                if (detectableComponent?.Previous?.Value.Azimuth <= detectableComponent?.Value?.Azimuth)
                {
                    CheckTargetByScanLine();
                }
            }
        }

        public double ScanLinePulseDuration
        {
            get => ScanLine.PulseDuration;
            set => ScanLine.PulseDuration = value;
        }

        public double ScanLinePulseLength
        {
            get => ScanLine.PulseLength;
            set => ScanLine.PulseLength = value;
        }

        public double ScanLineTimerInterval
        {
            get => ScanLineTimer.Interval;
            set => ScanLineTimer.Interval = value;
        }

        public double OpacityMultiplier { get; set; }

        public bool UseDefaultStructuralComponents
        {
            get { return GetProperty(() => UseDefaultStructuralComponents); }
            set { SetProperty(() => UseDefaultStructuralComponents, value); }
        }
    }
}