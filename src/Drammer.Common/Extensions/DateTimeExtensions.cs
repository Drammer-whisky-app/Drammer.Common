namespace Drammer.Common.Extensions;

/// <summary>
/// The date time extensions.
/// </summary>
public static class DateTimeExtensions
{
    /// <param name="currentValue">
    /// The current value.
    /// </param>
    extension(DateTime? currentValue)
    {
        /// <summary>
        /// Returns the earliest date of two values.
        /// </summary>
        /// <param name="newValue">
        /// The new value.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public DateTime? GetMinDate(DateTime? newValue)
        {
            if (newValue == null)
            {
                return currentValue;
            }

            if (currentValue == null)
            {
                return newValue;
            }

            return newValue < currentValue ? newValue : currentValue;
        }

        /// <summary>
        /// Returns the latest date of two values.
        /// </summary>
        /// <param name="newValue">
        /// The new value.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public DateTime? GetMaxDate(DateTime? newValue)
        {
            if (newValue == null)
            {
                return currentValue;
            }

            if (currentValue == null)
            {
                return newValue;
            }

            return newValue > currentValue ? newValue : currentValue;
        }
    }

    /// <summary>
    /// Change the year of a datetime.
    /// </summary>
    /// <param name="datetime">The current value.</param>
    /// <param name="year">The new year.</param>
    /// <returns>The <see cref="DateTime"/>.</returns>
    public static DateTime ChangeYear(this DateTime datetime, int year)
    {
        try
        {
            return new DateTime(
                year,
                datetime.Month,
                datetime.Day,
                datetime.Hour,
                datetime.Minute,
                datetime.Second,
                datetime.Millisecond,
                datetime.Kind);
        }
        catch (ArgumentOutOfRangeException)
        {
            return new DateTime(
                year,
                datetime.Month,
                DateTime.DaysInMonth(year, datetime.Month),
                datetime.Hour,
                datetime.Minute,
                datetime.Second,
                datetime.Millisecond,
                datetime.Kind);
        }
    }
}