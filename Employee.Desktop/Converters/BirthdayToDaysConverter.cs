using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Employee.Client.WPF.Converters
{
    public class BirthdayToDaysConverter : IValueConverter
    {
        private const string Phrase = "Days to next birthday: ";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                DateTime today = DateTime.Today;
                DateTime next = ((DateTime)value).AddYears(today.Year - ((DateTime)value).Year);

                if (next < today)
                {
                    next = next.AddYears(1);
                }

                return Phrase + (next - today).Days;
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
