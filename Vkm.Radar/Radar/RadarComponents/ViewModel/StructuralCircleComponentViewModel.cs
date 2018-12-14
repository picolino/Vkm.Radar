using System.Windows.Input;
using DevExpress.Mvvm;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    internal class StructuralCircleComponentViewModel : ViewModelBase, IStructuralComponent, IDetectableComponent
    {
        public double Azimuth { get; set; }

        public double Radius
        {
            get { return GetProperty(() => Radius); }
            set { SetProperty(() => Radius, value); }
        }
    }
}