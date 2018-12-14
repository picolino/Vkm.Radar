using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using DevExpress.Mvvm;
using Vkm.Radar.Radar.RadarComponents.ViewModel;

namespace Vkm.Radar.Radar.RadarComponents
{
    public class DisappearingComponent : UserControl
    {
        protected FrameworkElement BaseElement { get; set; }

        public DisappearingComponent()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;
            Unloaded -= OnUnloaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && !DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                ((IDetectableComponent)DataContext).TargetDetected = new DelegateCommand<double>(OnTargetDetected);
            }
        }

        protected virtual void OnTargetDetected(double opacityMultiplier)
        {
            if (Application.Current != null)
            {
                Application.Current.Dispatcher.BeginInvoke(new ThreadStart(() =>
                                                                           {
                                                                               var detectedAnimation = new DoubleAnimation(1.0 * opacityMultiplier, 0.0, new Duration(TimeSpan.FromSeconds(7)));
                                                                               BaseElement.BeginAnimation(OpacityProperty, detectedAnimation);
                                                                           }));
            }
        }
    }
}