using System;

namespace Vkm.Radar.Radar.RadarComponents
{
    /// <summary>
    /// Interaction logic for ScanLine.xaml
    /// </summary>
    public partial class Noise : DisappearingComponent
    {
        public Noise()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            BaseElement = PART_Line;
            base.OnInitialized(e);
        }
    }
}
