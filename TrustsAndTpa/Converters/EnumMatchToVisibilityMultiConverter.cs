using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace TrustsAndTpa.Converters {
    public class EnumMatchToVisibilityMultiConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            if (values == null || values.Length < 2 || values[0] == null || values[0] == DependencyProperty.UnsetValue)
                return Visibility.Collapsed;

            // Bound enum value
            var current = values[0];

            // ConverterParameter = "Visible|Collapsed" or "Visible!Hidden" or just "Collapsed"
            if (parameter is not string paramStr || string.IsNullOrWhiteSpace(paramStr))
                return Visibility.Collapsed;

            // Split on | (we also accept ! as separator for backward compatibility)
            var visibilityParts = paramStr.Split(new[] { '|', '!' }, StringSplitOptions.RemoveEmptyEntries)
                                          .Select(p => p.Trim())
                                          .Where(p => !string.IsNullOrEmpty(p))
                                          .ToArray();

            Visibility matchVisibility;
            Visibility noMatchVisibility;

            if (visibilityParts.Length == 0)
                return Visibility.Collapsed;

            if (!Enum.TryParse<Visibility>(visibilityParts[0], ignoreCase: true, out matchVisibility))
                return Visibility.Collapsed;

            noMatchVisibility = visibilityParts.Length > 1
                ? Enum.TryParse<Visibility>(visibilityParts[1], ignoreCase: true, out var nmv)
                    ? nmv
                    : Visibility.Collapsed
                : matchVisibility;  // fallback = same as match

            // Collect enum string names from remaining bindings (skip first = current)
            var candidates = values.Skip(1)
                                   .Where(v => v != null && v != DependencyProperty.UnsetValue)
                                   .Select(v => v.ToString()!)
                                   .ToList();

            if (!candidates.Any())
                return noMatchVisibility;

            // Check if negation mode is active (any starts with !)
            bool isNegationMode = candidates.Any(c => c.StartsWith("!", StringComparison.Ordinal));

            bool conditionMet;

            if (isNegationMode) {
                var negatedNames = candidates
                    .Where(c => c.StartsWith("!", StringComparison.Ordinal))
                    .Select(c => c.Substring(1).Trim())
                    .Where(n => !string.IsNullOrEmpty(n))
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                conditionMet = !negatedNames.Contains(current.ToString()!);
            }
            else {
                var allowedNames = candidates
                    .Select(c => c.Trim())
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                conditionMet = allowedNames.Contains(current.ToString()!);
            }

            return conditionMet ? matchVisibility : noMatchVisibility;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
