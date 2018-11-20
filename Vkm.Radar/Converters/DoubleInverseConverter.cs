using System;
using System.Globalization;

namespace Vkm.Radar.Converters
{
    internal class DoubleInverseConverter : BaseConverter<DoubleInverseConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double) value * -1;
        }
    }
}