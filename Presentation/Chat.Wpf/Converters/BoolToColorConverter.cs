// ------------------------------------------------------------
// <copyright file="BoolToColorConverter.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Chat.Wpf.Converters
{
    /// <summary>
    /// Convert bool to fill color.
    /// </summary>
    public class BoolToColorConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (bool)value ? Colors.Green : Colors.Red;
            return new SolidColorBrush(color);
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
