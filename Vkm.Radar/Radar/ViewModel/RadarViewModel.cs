using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using Vkm.Radar.Radar.RadarComponents.ViewModel;

namespace Vkm.Radar.Radar.ViewModel
{
    public class RadarViewModel : ViewModelBase
    {
        public RadarViewModel()
        {
            Components = new ObservableCollection<IPositionalComponent>();
            InitializeBaseComponents();
            InitializeTargets();
        }

        public ObservableCollection<IPositionalComponent> Components { get; }

        private void InitializeBaseComponents()
        {
            Components.Add(new ScanLineViewModel());
        }

        private void InitializeTargets()
        {
            Components.Add(new TargetViewModel(120, 230, 10));
        }
    }
}