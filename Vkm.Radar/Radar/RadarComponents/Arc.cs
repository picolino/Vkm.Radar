#region Usings

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

#endregion

namespace Vkm.Radar.Radar.RadarComponents
{
    public sealed class Arc : Shape
    {
        // Using a DependencyProperty as the backing store for Center.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CenterProperty =
            DependencyProperty.Register(nameof(Center),
                                        typeof(Point),
                                        typeof(Arc),
                                        new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsRender));

        // Using a DependencyProperty as the backing store for StartAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register(nameof(StartAngle),
                                        typeof(double),
                                        typeof(Arc),
                                        new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        // Using a DependencyProperty as the backing store for EndAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndAngleProperty =
            DependencyProperty.Register(nameof(EndAngle),
                                        typeof(double),
                                        typeof(Arc),
                                        new FrameworkPropertyMetadata(Math.PI / 2.0, FrameworkPropertyMetadataOptions.AffectsRender));

        // Using a DependencyProperty as the backing store for Radius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register(nameof(Radius),
                                        typeof(double),
                                        typeof(Arc),
                                        new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));

        // Using a DependencyProperty as the backing store for SmallAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SmallAngleProperty =
            DependencyProperty.Register(nameof(SmallAngle),
                                        typeof(bool),
                                        typeof(Arc),
                                        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        static Arc() => DefaultStyleKeyProperty.OverrideMetadata(typeof(Arc), new FrameworkPropertyMetadata(typeof(Arc)));

        public Point Center
        {
            get => (Point) GetValue(CenterProperty);
            set => SetValue(CenterProperty, value);
        }

        public double EndAngle
        {
            get => (double) GetValue(EndAngleProperty);
            set => SetValue(EndAngleProperty, value);
        }

        public double Radius
        {
            get => (double) GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        public bool SmallAngle
        {
            get => (bool) GetValue(SmallAngleProperty);
            set => SetValue(SmallAngleProperty, value);
        }

        public double StartAngle
        {
            get => (double) GetValue(StartAngleProperty);
            set => SetValue(StartAngleProperty, value);
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                var a0 = StartAngle < 0
                             ? StartAngle + 2 * Math.PI
                             : StartAngle;
                var a1 = EndAngle < 0
                             ? EndAngle + 2 * Math.PI
                             : EndAngle;

                if (a1 < a0)
                {
                    a1 += Math.PI * 2;
                }

                var sweepDirection = SweepDirection.Counterclockwise;
                bool isLargeArc;

                if (SmallAngle)
                {
                    isLargeArc = false;
                    sweepDirection = (a1 - a0) > Math.PI
                                         ? SweepDirection.Counterclockwise
                                         : SweepDirection.Clockwise;
                }
                else
                {
                    isLargeArc = (Math.Abs(a1 - a0) < Math.PI);
                }

                var p0 = Center + new Vector(Math.Cos(a0), Math.Sin(a0)) * Radius;
                var p1 = Center + new Vector(Math.Cos(a1), Math.Sin(a1)) * Radius;

                var segments = new List<PathSegment>
                               {
                                   new ArcSegment(p1, new Size(Radius, Radius), 0.0, isLargeArc, sweepDirection, true)
                               };

                var figures = new List<PathFigure>
                              {
                                  new PathFigure(p0, segments, true)
                                  {
                                      IsClosed = false
                                  }
                              };

                return new PathGeometry(figures, FillRule.EvenOdd, null);
            }
        }
    }
}