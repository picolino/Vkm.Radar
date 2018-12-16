using System.Windows;

namespace Vkm.Radar.Example.Dialog
{
    /// <summary>
    /// Interaction logic for PresetSelectorDialog.xaml
    /// </summary>
    public partial class PresetSelectorDialog : Window
    {
        public PresetSelectorDialog()
        {
            InitializeComponent();
        }

        public int Result { get; private set; } = -1;

        private void OnItemSelected(object sender, RoutedEventArgs e)
        {
            Result = PART_ListView.SelectedIndex;
            Close();
        }
    }
}
