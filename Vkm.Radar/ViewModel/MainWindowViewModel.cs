﻿using System.Windows.Input;
using DevExpress.Mvvm;
using Vkm.Radar.Radar.ViewModel;

namespace Vkm.Radar.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ICommand ResetCommand { get; }
        public ICommand PresetsCommand { get; }

        public MainWindowViewModel()
        {
            ResetCommand = new DelegateCommand(OnReset);
            PresetsCommand = new DelegateCommand(OnPresets);

            InitializeRadar();
        }

        private void OnPresets()
        {
            throw new System.NotImplementedException();
        }

        private void OnReset()
        {
            RadarViewModel.ClearAllComponents();
        }

        private void InitializeRadar()
        {
            var radar = new RadarViewModel(10);

            radar.AddTarget(120, 230, 4);
            radar.AddTarget(130, 100, 4);
            radar.AddTarget(20, 20, 4);
            radar.AddTarget(20, 90, 4);
            radar.AddTarget(20, 120, 4);
            radar.AddTarget(20, 200, 4);
            radar.AddTarget(20, 240, 4);
            radar.AddTarget(230, 248, 4);
            radar.AddTarget(359, 110, 4);

            radar.AddNoise(20, 40);

            RadarViewModel = radar;
        }

        public RadarViewModel RadarViewModel
        {
            get { return GetProperty(() => RadarViewModel); }
            set { SetProperty(() => RadarViewModel, value); }
        }
    }
}