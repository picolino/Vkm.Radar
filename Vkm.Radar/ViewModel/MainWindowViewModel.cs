using System;
using System.Windows.Input;
using DevExpress.Mvvm;
using Vkm.Radar.Radar;
using Vkm.Radar.Radar.ViewModel;

namespace Vkm.Radar.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ICommand ResetCommand { get; }
        public ICommand PresetsCommand { get; }
        public ICommand CreateTargetCommand { get; }

        public MainWindowViewModel()
        {
            ResetCommand = new DelegateCommand(OnReset);
            PresetsCommand = new DelegateCommand(OnPresets);
            CreateTargetCommand = new DelegateCommand(OnCreateTarget);

            InitializeRadar();
        }

        private void OnReset()
        {
            RadarViewModel.ClearAllComponents();
        }

        private void OnPresets()
        {
            throw new NotImplementedException();
        }

        private void OnCreateTarget()
        {
            var random = new Random();
            RadarViewModel.AddTarget(random.Next(0,360), random.Next(10, (int) Constants.RadarRadius), random.Next(3, 6));
        }

        private void InitializeRadar()
        {
            var radar = new RadarViewModel(10);

            radar.AddTarget(120, 104, 4);
            radar.AddTarget(120, 100, 4);

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