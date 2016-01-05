using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Employee.Client.WPF.Converters
{
    public class DateTimeConverter : DependencyObject, IValueConverter
    {
        private const string DefaultFormat = "g";
        private const string DefaultMinValueString = "";

        /// <summary>
        /// The datetime format property.
        /// Check MSDN for information about predefined datetime formats: http://msdn.microsoft.com/en-us/library/362btx8f(v=vs.90).aspx.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Dependency properties are static by design.")]
        public static readonly DependencyProperty FormatProperty = DependencyProperty.Register(
            "Format",
            typeof(string),
            typeof(DateTimeConverter),
            new PropertyMetadata(DefaultFormat));

        /// <summary>
        /// The DateTime.MinValue string property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Dependency properties are static by design.")]
        public static readonly DependencyProperty MinValueStringProperty = DependencyProperty.Register(
            "MinValueString",
            typeof(string),
            typeof(DateTimeConverter),
            new PropertyMetadata(DefaultMinValueString));

        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        /// <value>The format.</value>
        public string Format
        {
            get { return (string)this.GetValue(FormatProperty); }
            set { this.SetValue(FormatProperty, value); }
        }

        /// <summary>
        /// Gets or sets the minimum value string.
        /// </summary>
        /// <value>The minimum value string.</value>
        public string MinValueString
        {
            get { return (string)this.GetValue(MinValueStringProperty); }
            set { this.SetValue(MinValueStringProperty, value); }
        }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (DateTime)value == DateTime.MinValue)
            {
                return this.MinValueString;
            }
            
            if (targetType == typeof(string))
            {
                return ((DateTime)value).ToLocalTime().ToString(this.Format, culture);
            }

            return DependencyProperty.UnsetValue;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Conversion of date/time string back to DateTime type is not supported.");
        }
    }
}
