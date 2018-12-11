﻿using System;
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

        public RadarViewModel(double scanLineMoveInterval)
        {
            LoadedCommand = new DelegateCommand(OnLoaded);

            ScanLineTimer = new Timer(scanLineMoveInterval);
            ScanLineTimer.Elapsed += ScanLineMove;

            ScanLine = new ScanLineViewModel(0, 2, 1);
            Components = new ObservableCollection<IPositionalComponent>();

            InitializeComponents();

            ScanLine.RadarTargets = Components.OfType<TargetViewModel>();

            OpacityMultiplier = 1;
        }

        private void InitializeComponents()
        {
            Components.Add(ScanLine);
            DetectableComponents = new LinkedList<IDetectableComponent>();
        }

        public void AddTarget(double azimuth, double range, double width, double opacityMultiplier = 1)
        {
            var newTarget = new TargetViewModel(azimuth, range, width, ScanLine.PulseDuration, opacityMultiplier);
            Components.Add(newTarget);
            if (DetectableComponents?.Count == 0)
            {
                DetectableComponents.AddFirst(newTarget);
                detectableComponent = DetectableComponents.First;
            }
            else
            {
                var beforeNode = DetectableComponents.FindLast(DetectableComponents.LastOrDefault(c => c.Azimuth <= azimuth));
                if (beforeNode != null)
                {
                    DetectableComponents.AddAfter(beforeNode, newTarget);
                }
                var afterNode = DetectableComponents.Find(DetectableComponents.FirstOrDefault(c => c.Azimuth > azimuth));
                if (afterNode != null)
                {
                    DetectableComponents.AddBefore(afterNode, newTarget);
                    if (detectableComponent == afterNode)
                    {
                        detectableComponent = DetectableComponents.Find(newTarget);
                    }
                }
            }
        }

        public void AddNoise(double azimuth, int width)
        {
            var noises = new NoiseViewModel(azimuth, width).GenerateNoisesCollection();
            foreach (var noise in noises)
            {
                Components.Add(noise);
                if (DetectableComponents?.Count == 0)
                {
                    DetectableComponents.AddFirst(noise);
                    detectableComponent = DetectableComponents.First;
                }
                else
                {
                    var beforeNode = DetectableComponents.FindLast(DetectableComponents.LastOrDefault(c => c.Azimuth <= noise.Azimuth));
                    if (beforeNode != null)
                    {
                        DetectableComponents.AddAfter(beforeNode, noise);
                    }
                    var afterNode = DetectableComponents.Find(DetectableComponents.FirstOrDefault(c => c.Azimuth > noise.Azimuth));
                    if (afterNode != null)
                    {
                        DetectableComponents.AddBefore(afterNode, noise);
                        if (detectableComponent == afterNode)
                        {
                            detectableComponent = DetectableComponents.Find(noise);
                        }
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
    }
}