using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CalculatorTest.ValidationRules
{
    class IntegerRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                Int32.Parse(value as string);
            }
            catch
            {
                return new ValidationResult(false, $"Введёное значение не является числом");
            }

            return new ValidationResult(true, null);
        }
    }
}
