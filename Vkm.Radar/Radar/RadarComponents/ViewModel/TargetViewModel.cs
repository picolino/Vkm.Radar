using DevExpress.Mvvm;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    public class TargetViewModel : ViewModelBase
    {
        public TargetViewModel(double azimuth, double range, double width)
        {
            Azimuth = azimuth;
            Range = range;
            Width = width;
        }

        public double Azimuth
        {
            get { return GetProperty(() => Azimuth); }
            set { SetProperty(() => Azimuth, value); }
        }

        public double Range
        {
            get { return GetProperty(() => Range); }
            set { SetProperty(() => Range, value); }
        }

        public double Width
        {
            get { return GetProperty(() => Width); }
            set { SetProperty(() => Width, value); }
        }
    }
}