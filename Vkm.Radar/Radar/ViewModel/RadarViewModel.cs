using DevExpress.Mvvm;

namespace Vkm.Radar.Radar.ViewModel
{
    public class RadarViewModel : ViewModelBase
    {
        public RadarViewModel()
        {
            RadarDiameter = 400;
        }

        public int RadarDiameter
        {
            get { return GetProperty(() => RadarDiameter); }
            set { SetProperty(() => RadarDiameter, value); }
        }
    }
}