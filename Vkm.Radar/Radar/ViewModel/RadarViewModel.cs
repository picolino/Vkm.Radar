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
        internal ScanLineViewModel ScanLine { get; }
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
            Components.Add(ScanLine);
            DetectableComponents = new LinkedList<IDetectableComponent>();
            UseDefaultStructuralComponents = useStructuralComponents;
            if (useStructuralComponents)
            {
                AddDefaultStructuralComponents();
            }
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
            for (var i = 0; i < 360; i += 10)
            {
                if (i % 30 == 0)
                {
                    var bigRadialStructuralComponentFirst = new StructuralRadialComponentViewModel(i - 0.2, 0.4);
                    var bigRadialStructuralComponentSecond = new StructuralRadialComponentViewModel(i + 0.2, 0.4);
                    AddComponent(bigRadialStructuralComponentFirst);
                    AddComponent(bigRadialStructuralComponentSecond);
                }
                else
                {
                    var radialStructuralComponent = new StructuralRadialComponentViewModel(i, 0.1);
                    AddComponent(radialStructuralComponent);
                }
            }

            var firstCircle = new StructuralCircleComponentViewModel(0, 0.4, 100);
            var secondCircle = new StructuralCircleComponentViewModel(0, 0.4, 200);
            AddComponent(firstCircle);
            AddComponent(secondCircle);
        }

        private void AddComponents<T>(IEnumerable<T> components) where T : IPositionalComponent, IDetectableComponent
        {
            foreach (var component in components)
            {
                AddComponent(component);
            }
        }

        private void AddComponent<T>(T component) where T : IDetectableComponent, IPositionalComponent
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
                var component = Components[i];
                if (component is IStructuralComponent)
                {
                    continue;
                }
                Components.RemoveAt(i);
            }

            for (var it = DetectableComponents.First; it != null;)
            {
                var next = it.Next;
                if (it.Value is IStructuralComponent)
                {
                    it = next;
                    continue;
                }

                DetectableComponents.Remove(it);
                it = next;
            }
        }

        private void OnLoaded()
        {
            ScanLineTimer.Start();
        }

        private void ScanLineMove(object sender, ElapsedEventArgs e)
        {
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

            ScanLineAzimuth = ScanLine.Azimuth;
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

        public double ScanLineAzimuth
        {
            get { return GetProperty(() => ScanLineAzimuth); }
            set { SetProperty(() => ScanLineAzimuth, value); }
        }

        public double OpacityMultiplier { get; set; }

        public bool UseDefaultStructuralComponents
        {
            get { return GetProperty(() => UseDefaultStructuralComponents); }
            set { SetProperty(() => UseDefaultStructuralComponents, value); }
        }
    }
}