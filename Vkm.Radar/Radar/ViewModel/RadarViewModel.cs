using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using Vkm.Radar.Radar.RadarComponents.ViewModel;

namespace Vkm.Radar.Radar.ViewModel
{
    public class RadarViewModel : ViewModelBase
    {
        public RadarViewModel()
        {
            Components = new ObservableCollection<IPositionalComponent>
                                {
                                    new ScanLineViewModel(),
                                    new TargetViewModel(120, 230, 10),
                                };
        }

        public ObservableCollection<IPositionalComponent> Components { get; }
    }
}