using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using DevExpress.Mvvm;
using Vkm.Radar.Radar.RadarComponents.ViewModel;

namespace Vkm.Radar.Radar.RadarComponents
{
    /// <summary>
    /// Interaction logic for Target.xaml
    /// </summary>
    public partial class Target : UserControl
    {
        public Target()
        {
            InitializeComponent();
        }

        private void OnTargetDetected()
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(() =>
                                                                       {
                                                                           var detectedAnimation = new DoubleAnimation(1.0, 0.0, new Duration(TimeSpan.FromSeconds(7)));
                                                                           PART_Target.BeginAnimation(OpacityProperty, detectedAnimation);
                                                                       }));
        }

        private void Target_OnLoaded(object sender, RoutedEventArgs e)
        {
            ((TargetViewModel)DataContext).TargetDetected = new DelegateCommand(OnTargetDetected);
        }
    }
}
