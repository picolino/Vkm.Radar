#region Usings

using System;
using System.Windows;
using System.Windows.Controls;
using Vkm.Radar.Radar.ViewModel;

#endregion

namespace Vkm.Radar.Radar
{
    /// <summary>
    /// Interaction logic for Radar.xaml
    /// </summary>
    public partial class Radar : UserControl
    {
        #region Dependency Properties

        /// <summary>
        /// Gets or sets the 
        /// </summary>
        public bool UseStructureNetwork
        {
            get { return (bool)GetValue(UseStructureNetworkProperty); }
            set { SetValue(UseStructureNetworkProperty, value); }
        }

        /// <summary>
        /// Identified the UseStructureNetwork dependency property
        /// </summary>
        public static readonly DependencyProperty UseStructureNetworkProperty =
            DependencyProperty.Register("UseStructureNetwork", typeof(bool),
                                        typeof(Radar), new PropertyMetadata(true));

        #endregion

        public Radar()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is RadarViewModel)
            {
                return;
            }

            var vm = new RadarViewModel(UseStructureNetwork);
            DataContext = vm;
        }
    }
}