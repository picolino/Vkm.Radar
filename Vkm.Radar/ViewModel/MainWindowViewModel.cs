using System;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using Vkm.Radar.Dialog;
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
            var dialog = new PresetSelectorDialog
                         {
                             Owner = Application.Current.MainWindow
                         };

            dialog.ShowDialog();

            OnReset();

            switch (dialog.Result)
            {
                case 0:
                    RadarViewModel.AddNoise(45, 20);
                    RadarViewModel.AddTarget(45, 120, 4, 0.3);
                    break;
                case 1:
                    RadarViewModel.AddNoise(45, 10);
                    RadarViewModel.AddTarget(45, 120, 4, 0.7);
                    break;
                case 2:
                    RadarViewModel.AddTarget(45, 104, 4);
                    RadarViewModel.AddTarget(45, 100, 4);
                    break;
                case 3:
                    RadarViewModel.AddTarget(45, 100, 4);
                    RadarViewModel.AddTarget(50, 100, 4);
                    break;
            }
        }

        private void OnCreateTarget()
        {
            var random = new Random();
            RadarViewModel.AddTarget(random.Next(0,360), random.Next(10, (int) Constants.RadarRadius), random.Next(3, 6));
        }

        private void InitializeRadar()
        {
            var radar = new RadarViewModel(10);
            RadarViewModel = radar;
        }

        public RadarViewModel RadarViewModel
        {
            get { return GetProperty(() => RadarViewModel); }
            set { SetProperty(() => RadarViewModel, value); }
        }
    }
}