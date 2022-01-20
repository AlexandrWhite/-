using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CalculatorTest.ValidationRules
{
    class PrimeRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int? number;
            try
            {
                number = Int32.Parse((string)value);
            }
            catch
            {
                return null;
            }


            if (!Algorithms.isPrime((int)number))
            {
                return new ValidationResult(false, $"Число {number} не является простым");
            }
            return new ValidationResult(true, null);
        }
    }
}
