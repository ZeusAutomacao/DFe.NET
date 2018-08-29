using System;
using System.Globalization;
using System.Windows.Data;

namespace NFe.AppTeste.Conversores
{
    public class EnumParaString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) throw new ArgumentNullException();

            try
            {
                var enumString = Enum.GetName((value.GetType()), value);
                return enumString;
            }
            catch
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) throw new ArgumentNullException();
            return Enum.Parse(targetType, value.ToString(), true);
        }
    }
}
