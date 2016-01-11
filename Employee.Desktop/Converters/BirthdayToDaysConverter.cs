using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Employee.Client.WPF.Converters
{
    public class BirthdayToDaysConverter : IValueConverter
    {
        private const string Phrase = "Days to next birthday: ";

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                DateTime today = DateTime.Today;
                DateTime next = ((DateTime)value).AddYears(today.Year - ((DateTime)value).Year);

                if (next < today)
                    next = next.AddYears(1);

                return Phrase + (next - today).Days;
            }
            else
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
