using System;
using System.Globalization;
using System.Windows.Data;

namespace NFe.AppTeste
{
    public class EnumParaInteger : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            {
                var enumValue = default(Enum);
                var type = parameter as Type;
                if (type != null)
                {
                    enumValue = (Enum)Enum.Parse(type, value.ToString());
                }
                return enumValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var returnValue = 0;
            var type = parameter as Type;
            if (type != null)
            {
                returnValue = (int)Enum.Parse(type, value.ToString());
            }
            return returnValue;
        }
    }
}
