using System;

namespace LiteWare.DateAndTime.Extensions
{
    // Inspired from: https://stackoverflow.com/a/41805608/5240378

    /// <summary>
    /// Provides extension methods for manipulating and modifying <see cref="DateTime"/> objects.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Changes the year of the <paramref name="dateTime"/> to the specified <paramref name="year"/>.
        /// </summary>
        /// <param name="dateTime">The original <see cref="DateTime"/> object.</param>
        /// <param name="year">The new year value to set.</param>
        /// <returns>A new <see cref="DateTime"/> object with the modified year.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the <paramref name="year"/> is not within the valid range.
        /// </exception>
        public static DateTime ChangeYear(this DateTime dateTime, int year)
        {
            if (year < DateTime.MinValue.Year || year > DateTime.MaxValue.Year)
            {
                throw new ArgumentOutOfRangeException(nameof(year), year, "Year value is not within the valid range.");
            }

            return dateTime.AddYears(year - dateTime.Year);
        }

        /// <summary>
        /// Changes the month of the <paramref name="dateTime"/> to the specified <paramref name="month"/>.
        /// </summary>
        /// <param name="dateTime">The original <see cref="DateTime"/> object.</param>
        /// <param name="month">The new month value to set.</param>
        /// <returns>A new <see cref="DateTime"/> object with the modified month.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the <paramref name="month"/> is not within the valid range (1 - 12).
        /// </exception>
        public static DateTime ChangeMonth(this DateTime dateTime, int month)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month), month, "Month value is not within the valid range (1 - 12).");
            }

            return dateTime.AddMonths(month - dateTime.Month);
        }

        /// <summary>
        /// Changes the day of the <paramref name="dateTime"/> to the specified <paramref name="day"/>.
        /// </summary>
        /// <param name="dateTime">The original <see cref="DateTime"/> object.</param>
        /// <param name="day">The new day value to set.</param>
        /// <returns>A new <see cref="DateTime"/> object with the modified day.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the <paramref name="day"/> is not within the valid range (1 - 31 depending on the year and month).
        /// </exception>
        public static DateTime ChangeDay(this DateTime dateTime, int day)
        {
            if (day < 1 || day > DateTime.DaysInMonth(dateTime.Year, dateTime.Month))
            {
                throw new ArgumentOutOfRangeException(nameof(day), day, "Day value is not within the valid range (1 - 31 depending on the year and month).");
            }

            return dateTime.AddDays(day - dateTime.Day);
        }

        /// <summary>
        /// Changes the hour of the <paramref name="dateTime"/> to the specified <paramref name="hour"/>.
        /// </summary>
        /// <param name="dateTime">The original <see cref="DateTime"/> object.</param>
        /// <param name="hour">The new hour value to set.</param>
        /// <returns>A new <see cref="DateTime"/> object with the modified hour.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the <paramref name="hour"/> is not within the valid range (0 - 23).
        /// </exception>
        public static DateTime ChangeHour(this DateTime dateTime, int hour)
        {
            if (hour < 0 || hour > 23)
            {
                throw new ArgumentOutOfRangeException(nameof(hour), hour, "Hour value is not within the valid range (0 - 23).");
            }

            return dateTime.AddHours(hour - dateTime.Hour);
        }

        /// <summary>
        /// Changes the minute of the <paramref name="dateTime"/> to the specified <paramref name="minute"/>.
        /// </summary>
        /// <param name="dateTime">The original <see cref="DateTime"/> object.</param>
        /// <param name="minute">The new minute value to set.</param>
        /// <returns>A new <see cref="DateTime"/> object with the modified minute.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the <paramref name="minute"/> is not within the valid range (0 - 59).
        /// </exception>
        public static DateTime ChangeMinute(this DateTime dateTime, int minute)
        {
            if (minute < 0 || minute > 59)
            {
                throw new ArgumentOutOfRangeException(nameof(minute), minute, "Minute value is not within the valid range (0 - 59).");
            }

            return dateTime.AddMinutes(minute - dateTime.Minute);
        }

        /// <summary>
        /// Changes the second of the <paramref name="dateTime"/> to the specified <paramref name="second"/>.
        /// </summary>
        /// <param name="dateTime">The original <see cref="DateTime"/> object.</param>
        /// <param name="second">The new second value to set.</param>
        /// <returns>A new <see cref="DateTime"/> object with the modified second.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the <paramref name="second"/> is not within the valid range (0 - 59).
        /// </exception>
        public static DateTime ChangeSecond(this DateTime dateTime, int second)
        {
            if (second < 0 || second > 59)
            {
                throw new ArgumentOutOfRangeException(nameof(second), second, "Second value is not within the valid range (0 - 59).");
            }

            return dateTime.AddSeconds(second - dateTime.Second);
        }

        /// <summary>
        /// Changes the millisecond of the <paramref name="dateTime"/> to the specified <paramref name="millisecond"/>.
        /// </summary>
        /// <param name="dateTime">The original <see cref="DateTime"/> object.</param>
        /// <param name="millisecond">The new millisecond value to set.</param>
        /// <returns>A new <see cref="DateTime"/> object with the modified millisecond.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the <paramref name="millisecond"/> is not within the valid range (0 - 999).
        /// </exception>
        public static DateTime ChangeMillisecond(this DateTime dateTime, int millisecond)
        {
            if (millisecond < 0 || millisecond > 999)
            {
                throw new ArgumentOutOfRangeException(nameof(millisecond), millisecond, "Millisecond value is not within the valid range (0 - 999).");
            }

            return dateTime.AddMilliseconds(millisecond - dateTime.Millisecond);
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> with the specified time of day from the original <paramref name="dateTime"/>.
        /// </summary>
        /// <param name="dateTime">The original <see cref="DateTime"/> to adjust.</param>
        /// <param name="hour">The desired hour component of the time.</param>
        /// <param name="minute">The desired minute component of the time.</param>
        /// <param name="second">The desired second component of the time.</param>
        /// <param name="millisecond">The desired millisecond component of the time.</param>
        /// <returns>A new <see cref="DateTime"/> with the specified time of day.</returns>
        public static DateTime At(this DateTime dateTime, int hour, int minute, int second, int millisecond)
        {
            return dateTime
                .ChangeHour(hour)
                .ChangeMinute(minute)
                .ChangeSecond(second)
                .ChangeMillisecond(millisecond);
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> with the specified time of day from the original <paramref name="dateTime."/>
        /// </summary>
        /// <param name="dateTime">The original <see cref="DateTime"/> to adjust.</param>
        /// <param name="hour">The desired hour component of the time.</param>
        /// <param name="minute">The desired minute component of the time. Defaults to 0 if not provided.</param>
        /// <param name="second">The desired second component of the time. Defaults to 0 if not provided.</param>
        /// <returns>A new <see cref="DateTime"/> with the specified time of day.</returns>
        public static DateTime At(this DateTime dateTime, int hour, int minute = 0, int second = 0)
        {
            return dateTime.At(hour, minute, second, 0);
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> with the time of day adjusted to the specified <paramref name="time"/> from the original <paramref name="dateTime"/>.
        /// </summary>
        /// <param name="dateTime">The original <see cref="DateTime"/> to adjust.</param>
        /// <param name="time">A <see cref="TimeSpan"/> representing the desired time of day.</param>
        /// <returns>A new <see cref="DateTime"/> with the time of day adjusted to the specified <paramref name="time"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when an <paramref name="time"/> with a total duration greater than 24 hours is provided, as it cannot be used to determine a specific time of day.
        /// </exception>
        public static DateTime At(this DateTime dateTime, TimeSpan time)
        {
            if (time.TotalDays > 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dateTime), "Invalid time span provided. Time spans greater than 24 hours cannot be used to determine a specific time of day.");
            }

            return dateTime.At(time.Hours, time.Minutes, time.Seconds, time.Milliseconds);
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> with the time set to noon (12:00:00.000) from the original <paramref name="dateTime"/>.
        /// </summary>
        /// <param name="dateTime">The original <see cref="DateTime"/> to adjust.</param>
        /// <returns>A new <see cref="DateTime"/> set to noon.</returns>
        public static DateTime AtNoon(this DateTime dateTime)
        {
            return dateTime.At(12);
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> with the time set to midnight (00:00:00.000) from the original <paramref name="dateTime"/>.
        /// </summary>
        /// <param name="dateTime">The original <see cref="DateTime"/> to adjust.</param>
        /// <returns>A new <see cref="DateTime"/> set to midnight.</returns>
        public static DateTime AtMidnight(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> with the time set to the start of the day (00:00:00.000) from the original <paramref name="dateTime"/>.
        /// </summary>
        /// <param name="dateTime">The original <see cref="DateTime"/> to adjust.</param>
        /// <returns>A new <see cref="DateTime"/> set to the start of the day.</returns>
        public static DateTime AtStartOfDay(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> with the time set to the end of the day (23:59:59.999) from the original <paramref name="dateTime"/>.
        /// </summary>
        /// <param name="dateTime">The original <see cref="DateTime"/> to adjust.</param>
        /// <returns>A new <see cref="DateTime"/> set to the end of the day.</returns>
        public static DateTime AtEndOfDay(this DateTime dateTime)
        {
            return dateTime.Date.AddTicks(TimeSpan.TicksPerDay - 1);
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> representing the start of the month at 00:00:00.000 of the original <paramref name="dateTime"/>.
        /// </summary>
        /// <param name="dateTime">The original <see cref="DateTime"/> to adjust.</param>
        /// <returns>A new <see cref="DateTime"/> set to the start of the month.</returns>
        public static DateTime OnStartOfMonth(this DateTime dateTime)
        {
            return dateTime
                .ChangeDay(1)
                .AtStartOfDay();
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> representing the end of the month at 23:59:59.999 of the original <paramref name="dateTime"/>.
        /// </summary>
        /// <param name="dateTime">The original <see cref="DateTime"/> to adjust.</param>
        /// <returns>A new <see cref="DateTime"/> set to the end of the month.</returns>
        public static DateTime OnEndOfMonth(this DateTime dateTime)
        {
            int lastDayOfMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            return dateTime
                .ChangeDay(lastDayOfMonth)
                .AtEndOfDay();
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> representing the start of the year on January at 00:00:00.000 of the original <paramref name="dateTime"/>.
        /// </summary>
        /// <param name="dateTime">The original <see cref="DateTime"/> to adjust.</param>
        /// <returns>A new <see cref="DateTime"/> set to the start of the year.</returns>
        public static DateTime OnStartOfYear(this DateTime dateTime)
        {
            return dateTime
                .ChangeMonth(1)
                .OnStartOfMonth();
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> representing the end of the year on December at 23:59:59.999 of the original <paramref name="dateTime"/>.
        /// </summary>
        /// <param name="dateTime">The original <see cref="DateTime"/> to adjust.</param>
        /// <returns>A new <see cref="DateTime"/> set to the end of the year.</returns>
        public static DateTime OnEndOfYear(this DateTime dateTime)
        {
            return dateTime
                .ChangeMonth(12)
                .OnEndOfMonth();
        }

        /// <summary>
        /// Determines if the current <see cref="DateTime"/> is before the specified <paramref name="referenceDateTime"/>.
        /// </summary>
        /// <param name="dateTime">The current <see cref="DateTime"/> to compare.</param>
        /// <param name="referenceDateTime">The reference <see cref="DateTime"/> for comparison.</param>
        /// <returns><c>true</c> if the current <see cref="DateTime"/> is before the reference <see cref="DateTime"/>; otherwise, <c>false</c>.</returns>
        public static bool IsBefore(this DateTime dateTime, DateTime referenceDateTime)
        {
            return dateTime < referenceDateTime;
        }

        /// <summary>
        /// Determines if the current <see cref="DateTime"/> is on or before the specified <paramref name="referenceDateTime"/>.
        /// </summary>
        /// <param name="dateTime">The current <see cref="DateTime"/> to compare.</param>
        /// <param name="referenceDateTime">The reference <see cref="DateTime"/> for comparison.</param>
        /// <returns><c>true</c> if the current <see cref="DateTime"/> is on or before the reference <see cref="DateTime"/>; otherwise, <c>false</c>.</returns>
        public static bool IsOnOrBefore(this DateTime dateTime, DateTime referenceDateTime)
        {
            return dateTime <= referenceDateTime;
        }

        /// <summary>
        /// Determines if the current <see cref="DateTime"/> is after the specified <paramref name="referenceDateTime"/>.
        /// </summary>
        /// <param name="dateTime">The current <see cref="DateTime"/> to compare.</param>
        /// <param name="referenceDateTime">The reference <see cref="DateTime"/> for comparison.</param>
        /// <returns><c>true</c> if the current <see cref="DateTime"/> is after the reference <see cref="DateTime"/>; otherwise, <c>false</c>.</returns>
        public static bool IsAfter(this DateTime dateTime, DateTime referenceDateTime)
        {
            return dateTime > referenceDateTime;
        }

        /// <summary>
        /// Determines if the current <see cref="DateTime"/> is on or after the specified <paramref name="referenceDateTime"/>.
        /// </summary>
        /// <param name="dateTime">The current <see cref="DateTime"/> to compare.</param>
        /// <param name="referenceDateTime">The reference <see cref="DateTime"/> for comparison.</param>
        /// <returns><c>true</c> if the current <see cref="DateTime"/> is on or after the reference <see cref="DateTime"/>; otherwise, <c>false</c>.</returns>
        public static bool IsOnOrAfter(this DateTime dateTime, DateTime referenceDateTime)
        {
            return dateTime >= referenceDateTime;
        }

        /// <summary>
        /// Determines if the current <see cref="DateTime"/> is within the date range specified by <paramref name="startDateTime"/> and <paramref name="endDateTime"/>.
        /// </summary>
        /// <param name="dateTime">The current <see cref="DateTime"/> to compare.</param>
        /// <param name="startDateTime">The start of the date range for comparison.</param>
        /// <param name="endDateTime">The end of the date range for comparison.</param>
        /// <param name="inclusiveComparison">A flag indicating whether the comparison is inclusive (default) or exclusive.</param>
        /// <returns><c>true</c> if the current <see cref="DateTime"/> is within the specified date range; otherwise, <c>false</c>.</returns>
        public static bool IsBetween(this DateTime dateTime, DateTime startDateTime, DateTime endDateTime, bool inclusiveComparison = true)
        {
            if (inclusiveComparison)
            {
                return dateTime >= startDateTime && dateTime <= endDateTime;
            }

            return dateTime > startDateTime && dateTime < endDateTime;
        }

        /// <summary>
        /// Determines if the current <see cref="DateTime"/> falls on a weekday (Monday to Friday).
        /// </summary>
        /// <param name="dateTime">The current <see cref="DateTime"/> to evaluate.</param>
        /// <returns><c>true</c> if the current <see cref="DateTime"/> is a weekday; otherwise, <c>false</c>.</returns>
        public static bool IsWeekday(this DateTime dateTime)
        {
            return dateTime.DayOfWeek >= DayOfWeek.Monday && dateTime.DayOfWeek <= DayOfWeek.Friday;
        }

        /// <summary>
        /// Determines if the current <see cref="DateTime"/> falls on a weekend day (Saturday or Sunday).
        /// </summary>
        /// <param name="dateTime">The current <see cref="DateTime"/> to evaluate.</param>
        /// <returns><c>true</c> if the current <see cref="DateTime"/> is a weekend day; otherwise, <c>false</c>.</returns>
        public static bool IsWeekend(this DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Determines if the year of the current <see cref="DateTime"/> is a leap year.
        /// </summary>
        /// <param name="dateTime">The current <see cref="DateTime"/> to evaluate.</param>
        /// <returns><c>true</c> if the year of the current <see cref="DateTime"/> is a leap year; otherwise, <c>false</c>.</returns>
        public static bool IsLeapYear(this DateTime dateTime)
        {
            return DateTime.IsLeapYear(dateTime.Year);
        }
    }
}
