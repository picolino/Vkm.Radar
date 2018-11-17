using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using Vkm.Radar.Radar.RadarComponents.ViewModel;

namespace Vkm.Radar.Radar.ViewModel
{
    public class RadarViewModel : ViewModelBase
    {
        public RadarViewModel()
        {
            TargetsCollection = new ObservableCollection<IPositionalComponent>
                                {
                                    new TargetViewModel(120, 230, 10)
                                };
            NoisesCollection = new ObservableCollection<NoiseViewModel>();
        }

        public ObservableCollection<IPositionalComponent> TargetsCollection { get; }
        public ObservableCollection<NoiseViewModel> NoisesCollection { get; }
    }
}