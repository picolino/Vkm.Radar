using System.Windows.Input;
using DevExpress.Mvvm;

namespace Vkm.Radar.Radar.RadarComponents.ViewModel
{
    internal class StructuralCircleComponentViewModel : ViewModelBase, IStructuralComponent, IDetectableComponent
    {
        public bool IsAlwaysDetectable => true;
        public double Azimuth { get; set; }
        public ICommand TargetDetected { get; set; }
        
        public void WhenDetected(double opacityMultiplier)
        {
            throw new System.NotImplementedException();
        }

        public double Radius
        {
            get { return GetProperty(() => Radius); }
            set { SetProperty(() => Radius, value); }
        }
    }
}