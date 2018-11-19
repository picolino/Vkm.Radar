using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Vkm.Radar.Converters
{
    public abstract class BaseConverter<T> : MarkupExtension, IValueConverter where T : class, new()
    {
        private static T converter;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return converter ?? (converter = new T());
        }

        #region IValueConverter

        public virtual object Convert(object value, Type targetType, object parameter,
                                      CultureInfo culture)
        {
            return value;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter,
                                          CultureInfo culture)
        {
            return value;
        }

        #endregion
    }
}