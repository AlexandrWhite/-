using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CalculatorTest.Converters
{
    class ExpressionFontConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int length = (int)value;
            int font_size = 36;
            if (length <= 12) { font_size = 36; }
            if (length > 12) { font_size = 30; } 
            if (length > 16) { font_size = 20; }
            if (length > 20) { font_size = 15; }
            return font_size;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
