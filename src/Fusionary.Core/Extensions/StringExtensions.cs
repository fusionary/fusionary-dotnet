using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Fusionary.Core.Extensions;

public static class StringExtensions {
    /// <summary>
    /// Converts the value to <typeparamref name="TValue" />
    /// </summary>
    /// <typeparam name="TValue">The type of the t value.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns>The converted value or default</returns>
    public static TValue? As<TValue>(this string? value) {
        return As<TValue?>(value, default);
    }

    /// <summary>
    /// Converts the value to <typeparamref name="TValue" />
    /// </summary>
    /// <typeparam name="TValue">The type of the t value.</typeparam>
    /// <param name="value">The value.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The converted value or default</returns>
    [SuppressMessage(
        "Microsoft.Design",
        "CA1031:DoNotCatchGeneralExceptionTypes",
        Justification = "We want to make this user friendly and return the default value on all failures")]
    public static TValue As<TValue>(this string? value, TValue defaultValue) {
        try {
            var converter = TypeDescriptor.GetConverter(typeof(TValue));
            if (converter.CanConvertFrom(typeof(string)) && value != null) {
                return (TValue?)converter.ConvertFrom(value) ?? defaultValue;
            }

            // try the other direction
            converter = TypeDescriptor.GetConverter(typeof(string));
            if (converter.CanConvertTo(typeof(TValue))) {
                return (TValue?)converter.ConvertTo(value ?? "", typeof(TValue)) ?? defaultValue;
            }
        } catch {
            // eat all exceptions and return the defaultValue, assumption is that its always a parse/format exception
        }

        return defaultValue;
    }

    /// <summary>
    /// An overload of string contains that supports Case Insensitive comparison.
    /// </summary>
    /// <param name="s">The current string.</param>
    /// <param name="value">The value.</param>
    /// <param name="comparisonType">Type of the comparison.</param>
    /// <returns><c>true</c> if [contains] [the specified value]; otherwise, <c>false</c>.</returns>
    public static bool Contains(this string? s, string? value, StringComparison comparisonType) {
        if (string.IsNullOrEmpty(s)) {
            return false;
        }

        if (string.IsNullOrEmpty(value)) {
            return true;
        }

        return s.IndexOf(value, comparisonType) >= 0;
    }

    public static string Left(this string s, int maxLength) {
        return string.IsNullOrEmpty(s) ? s : string.Concat(s.Take(maxLength));
    }

    /// <summary>
    /// Extends ToString formatting to Nullable types.
    /// </summary>
    /// <typeparam name="T">The current type must be a value type that implements IFormattable</typeparam>
    /// <param name="s">The s.</param>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">The format provider.</param>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public static string ToString<T>(this T? s, string format, IFormatProvider? formatProvider = default)
        where T : struct, IFormattable {
        return s?.ToString(format, formatProvider) ?? string.Empty;
    }
}
