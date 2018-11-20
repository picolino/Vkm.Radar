using System;

namespace Vkm.Radar.Radar.RadarComponents
{
    /// <summary>
    /// Interaction logic for Target.xaml
    /// </summary>
    internal partial class Target : DisappearingComponent
    {
        public Target()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            BaseElement = PART_Target;
            base.OnInitialized(e);
        }
    }
}
