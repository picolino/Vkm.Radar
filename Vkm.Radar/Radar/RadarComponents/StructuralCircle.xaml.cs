using System;

namespace Vkm.Radar.Radar.RadarComponents
{
    /// <summary>
    /// Interaction logic for StructuralCircle.xaml
    /// </summary>
    public partial class StructuralCircle : DisappearingComponent
    {
        public StructuralCircle()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            BaseElement = PART_Ellipse;
            base.OnInitialized(e);
        }

        protected override void OnTargetDetected(double opacityMultiplier)
        {
            throw new NotImplementedException();
        }
    }
}
