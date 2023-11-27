using System;

namespace LiteWare.DateAndTime.Extensions
{
    /// <summary>
    /// Provides extension methods for manipulating and modifying <see cref="DateTimeOffset"/> objects.
    /// </summary>
    public static class DateTimeOffsetExtensions
    {
        /// <summary>
        /// Adds a specified number of weeks to the current <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to which weeks will be added.</param>
        /// <param name="value">The number of weeks to add. It can be a fractional value.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> representing the result of adding the specified weeks to the original <paramref name="dateTimeOffset"/>.</returns>
        public static DateTimeOffset AddWeeks(this DateTimeOffset dateTimeOffset, double value)
        {
            return dateTimeOffset.AddDays(value * 7);
        }

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> with the time set according to the provided <paramref name="time"/> and adjusted for the specified <paramref name="timeZoneOffset"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        /// <param name="time">The time span representing the desired time of day.</param>
        /// <param name="timeZoneOffset">The time zone offset to adjust for.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> set to the specified time and adjusted for the time zone offset.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="time"/> represents a time span greater than 24 hours.</exception>
        public static DateTimeOffset At(this DateTimeOffset dateTimeOffset, TimeSpan time, TimeSpan timeZoneOffset)
        {
            if (time.TotalDays > 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dateTimeOffset), "Invalid time span provided. Time spans greater than 24 hours cannot be used to determine a specific time of day.");
            }

            TimeSpan adjustedTimeZone = dateTimeOffset.Offset - timeZoneOffset;
            TimeSpan adjustedTime = time + adjustedTimeZone - dateTimeOffset.TimeOfDay;

            return dateTimeOffset.Add(adjustedTime);
        }

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> with the time set according to the provided <paramref name="time"/> and adjusted for the specified <paramref name="timeZoneInfo"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        /// <param name="time">The time span representing the desired time of day.</param>
        /// <param name="timeZoneInfo">The <see cref="TimeZoneInfo"/> to use for adjusting the time.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> set to the specified time and adjusted based on the specified <paramref name="timeZoneInfo"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="time"/> represents a time span greater than 24 hours.</exception>
        public static DateTimeOffset At(this DateTimeOffset dateTimeOffset, TimeSpan time, TimeZoneInfo timeZoneInfo)
        {
            return dateTimeOffset.At(time, timeZoneInfo.BaseUtcOffset);
        }

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> with the time set according to the provided <paramref name="time"/> and adjusted for the specified <paramref name="kind"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        /// <param name="time">The time span representing the desired time of day.</param>
        /// <param name="kind">The desired <see cref="DateTimeKind"/> for the resulting <see cref="DateTimeOffset"/>.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> set to the specified time and adjusted based on the specified <paramref name="kind"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="time"/> represents a time span greater than 24 hours.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="kind"/> has an unsupported value.</exception>
        public static DateTimeOffset At(this DateTimeOffset dateTimeOffset, TimeSpan time, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            TimeSpan timeZoneOffset;
            switch (kind)
            {
                case DateTimeKind.Unspecified:
                    timeZoneOffset = dateTimeOffset.Offset;
                    break;

                case DateTimeKind.Utc:
                    timeZoneOffset = TimeZoneInfo.Utc.BaseUtcOffset;
                    break;

                case DateTimeKind.Local:
                    timeZoneOffset = TimeZoneInfo.Local.BaseUtcOffset;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, $"Unsupported {nameof(DateTimeKind)} value.");
            }

            return dateTimeOffset.At(time, timeZoneOffset);
        }

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> with the specified time of day from the original <paramref name="dateTimeOffset"/> and adjusted for the specified <paramref name="timeZoneOffset"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        /// <param name="timeZoneOffset">The time zone offset to adjust for.</param>
        /// <param name="hour">The hour of the day.</param>
        /// <param name="minute">The minute of the hour (default is 0).</param>
        /// <param name="second">The second of the minute (default is 0).</param>
        /// <param name="millisecond">The millisecond of the second (default is 0).</param>
        /// <returns>A new <see cref="DateTimeOffset"/> set to the specified time and adjusted for the time zone offset.</returns>
        public static DateTimeOffset At(this DateTimeOffset dateTimeOffset, TimeSpan timeZoneOffset, int hour, int minute = 0, int second = 0, int millisecond = 0)
        {
            TimeSpan time = new TimeSpan(0, hour, minute, second, millisecond);
            return dateTimeOffset.At(time, timeZoneOffset);
        }

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> with the specified time of day from the original <paramref name="dateTimeOffset"/> and adjusted for the specified <paramref name="timeZoneInfo"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        /// <param name="timeZoneInfo">The <see cref="TimeZoneInfo"/> to use for adjusting the time.</param>
        /// <param name="hour">The hour of the day.</param>
        /// <param name="minute">The minute of the hour (default is 0).</param>
        /// <param name="second">The second of the minute (default is 0).</param>
        /// <param name="millisecond">The millisecond of the second (default is 0).</param>
        /// <returns>A new <see cref="DateTimeOffset"/> set to the specified time and adjusted based on the specified <paramref name="timeZoneInfo"/>.</returns>
        public static DateTimeOffset At(this DateTimeOffset dateTimeOffset, TimeZoneInfo timeZoneInfo, int hour, int minute = 0, int second = 0, int millisecond = 0)
        {
            TimeSpan time = new TimeSpan(0, hour, minute, second, millisecond);
            return dateTimeOffset.At(time, timeZoneInfo);
        }

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> with the specified time of day from the original <paramref name="dateTimeOffset"/> and adjusted for the specified <paramref name="kind"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        /// <param name="kind">The desired <see cref="DateTimeKind"/> for the resulting <see cref="DateTimeOffset"/>.</param>
        /// <param name="hour">The hour of the day.</param>
        /// <param name="minute">The minute of the hour (default is 0).</param>
        /// <param name="second">The second of the minute (default is 0).</param>
        /// <param name="millisecond">The millisecond of the second (default is 0).</param>
        /// <returns>A new <see cref="DateTimeOffset"/> set to the specified time and adjusted based on the specified <paramref name="kind"/>.</returns>
        public static DateTimeOffset At(this DateTimeOffset dateTimeOffset, DateTimeKind kind, int hour, int minute = 0, int second = 0, int millisecond = 0)
        {
            TimeSpan time = new TimeSpan(0, hour, minute, second, millisecond);
            return dateTimeOffset.At(time, kind);
        }

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> with the specified time of day from the original <paramref name="dateTimeOffset"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        /// <param name="hour">The hour of the day.</param>
        /// <param name="minute">The minute of the hour (default is 0).</param>
        /// <param name="second">The second of the minute (default is 0).</param>
        /// <param name="millisecond">The millisecond of the second (default is 0).</param>
        /// <returns>A new <see cref="DateTimeOffset"/> set to the specified time.</returns>
        public static DateTimeOffset At(this DateTimeOffset dateTimeOffset, int hour, int minute = 0, int second = 0, int millisecond = 0)
        {
            TimeSpan time = new TimeSpan(0, hour, minute, second, millisecond);
            return dateTimeOffset.At(time);
        }

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> with the time set to the end of the day (23:59:59.999) from the original <paramref name="dateTimeOffset"/> and adjusted for the specified <paramref name="timeZoneOffset"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        /// <param name="timeZoneOffset">The time zone offset to adjust for.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> set to the end of the day.</returns>
        public static DateTimeOffset AtEndOfDay(this DateTimeOffset dateTimeOffset, TimeSpan timeZoneOffset)
        {
            TimeSpan time = TimeSpan.FromTicks(TimeSpan.TicksPerDay - 1);
            return dateTimeOffset.At(time, timeZoneOffset);
        }

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> with the time set to the end of the day (23:59:59.999) from the original <paramref name="dateTimeOffset"/> and adjusted for the specified <paramref name="timeZoneInfo"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        /// <param name="timeZoneInfo">The <see cref="TimeZoneInfo"/> to use for adjusting the time.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> set to the end of the day.</returns>
        public static DateTimeOffset AtEndOfDay(this DateTimeOffset dateTimeOffset, TimeZoneInfo timeZoneInfo)
        {
            TimeSpan time = TimeSpan.FromTicks(TimeSpan.TicksPerDay - 1);
            return dateTimeOffset.At(time, timeZoneInfo);
        }

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> with the time set to the end of the day (23:59:59.999) from the original <paramref name="dateTimeOffset"/> and adjusted for the specified <paramref name="kind"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        /// <param name="kind">The desired <see cref="DateTimeKind"/> for the resulting <see cref="DateTimeOffset"/>.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> set to the end of the day.</returns>
        public static DateTimeOffset AtEndOfDay(this DateTimeOffset dateTimeOffset, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            TimeSpan time = TimeSpan.FromTicks(TimeSpan.TicksPerDay - 1);
            return dateTimeOffset.At(time, kind);
        }

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> with the time set to midnight (00:00:00.000) from the original <paramref name="dateTimeOffset"/> and adjusted for the specified <paramref name="timeZoneOffset"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        /// <param name="timeZoneOffset">The time zone offset to adjust for.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> set to midnight.</returns>
        public static DateTimeOffset AtMidnight(this DateTimeOffset dateTimeOffset, TimeSpan timeZoneOffset)
        {
            return dateTimeOffset.At(TimeSpan.Zero, timeZoneOffset);
        }

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> with the time set to midnight (00:00:00.000) from the original <paramref name="dateTimeOffset"/> and adjusted for the specified <paramref name="timeZoneInfo"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        /// <param name="timeZoneInfo">The <see cref="TimeZoneInfo"/> to use for adjusting the time.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> set to midnight.</returns>
        public static DateTimeOffset AtMidnight(this DateTimeOffset dateTimeOffset, TimeZoneInfo timeZoneInfo)
        {
            return dateTimeOffset.At(TimeSpan.Zero, timeZoneInfo);
        }

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> with the time set to midnight (00:00:00.000) from the original <paramref name="dateTimeOffset"/> and adjusted for the specified <paramref name="kind"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        /// <param name="kind">The desired <see cref="DateTimeKind"/> for the resulting <see cref="DateTimeOffset"/>.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> set to midnight.</returns>
        public static DateTimeOffset AtMidnight(this DateTimeOffset dateTimeOffset, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            return dateTimeOffset.At(TimeSpan.Zero, kind);
        }

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> with the time set to noon (12:00:00.000) from the original <paramref name="dateTimeOffset"/> and adjusted for the specified <paramref name="timeZoneOffset"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        /// <param name="timeZoneOffset">The time zone offset to adjust for.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> set to noon.</returns>
        public static DateTimeOffset AtNoon(this DateTimeOffset dateTimeOffset, TimeSpan timeZoneOffset)
        {
            return dateTimeOffset.At(timeZoneOffset, 12);
        }

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> with the time set to noon (12:00:00.000) from the original <paramref name="dateTimeOffset"/> and adjusted for the specified <paramref name="timeZoneInfo"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        /// <param name="timeZoneInfo">The <see cref="TimeZoneInfo"/> to use for adjusting the time.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> set to noon.</returns>
        public static DateTimeOffset AtNoon(this DateTimeOffset dateTimeOffset, TimeZoneInfo timeZoneInfo)
        {
            return dateTimeOffset.At(timeZoneInfo, 12);
        }

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> with the time set to noon (12:00:00.000) from the original <paramref name="dateTimeOffset"/> and adjusted for the specified <paramref name="kind"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        /// <param name="kind">The desired <see cref="DateTimeKind"/> for the resulting <see cref="DateTimeOffset"/>.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> set to noon.</returns>
        public static DateTimeOffset AtNoon(this DateTimeOffset dateTimeOffset, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            return dateTimeOffset.At(kind, 12);
        }

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> with the time set to the start of the day (00:00:00.000) from the original <paramref name="dateTimeOffset"/> and adjusted for the specified <paramref name="timeZoneOffset"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        /// <param name="timeZoneOffset">The time zone offset to adjust for.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> set to the start of the day.</returns>
        public static DateTimeOffset AtStartOfDay(this DateTimeOffset dateTimeOffset, TimeSpan timeZoneOffset)
        {
            return dateTimeOffset.At(TimeSpan.Zero, timeZoneOffset);
        }

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> with the time set to the start of the day (00:00:00.000) from the original <paramref name="dateTimeOffset"/> and adjusted for the specified <paramref name="timeZoneInfo"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        /// <param name="timeZoneInfo">The <see cref="TimeZoneInfo"/> to use for adjusting the time.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> set to the start of the day.</returns>
        public static DateTimeOffset AtStartOfDay(this DateTimeOffset dateTimeOffset, TimeZoneInfo timeZoneInfo)
        {
            return dateTimeOffset.At(TimeSpan.Zero, timeZoneInfo);
        }

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> with the time set to the start of the day (00:00:00.000) from the original <paramref name="dateTimeOffset"/> and adjusted for the specified <paramref name="kind"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        /// <param name="kind">The desired <see cref="DateTimeKind"/> for the resulting <see cref="DateTimeOffset"/>.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> set to the start of the day.</returns>
        public static DateTimeOffset AtStartOfDay(this DateTimeOffset dateTimeOffset, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            return dateTimeOffset.At(TimeSpan.Zero, kind);
        }

        /// <summary>
        /// Changes the day of the <paramref name="dateTimeOffset"/> to the specified <paramref name="day"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> object.</param>
        /// <param name="day">The new day value to set.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> object with the modified day.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the <paramref name="day"/> is not within the valid range (1 - 31 depending on the year and month).
        /// </exception>
        public static DateTimeOffset ChangeDay(this DateTimeOffset dateTimeOffset, int day)
        {
            if (day < 1 || day > DateTime.DaysInMonth(dateTimeOffset.Year, dateTimeOffset.Month))
            {
                throw new ArgumentOutOfRangeException(nameof(day), day, "Day value is not within the valid range (1 - 31 depending on the year and month).");
            }

            return dateTimeOffset.AddDays(day - dateTimeOffset.Day);
        }

        /// <summary>
        /// Changes the hour of the <paramref name="dateTimeOffset"/> to the specified <paramref name="hour"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> object.</param>
        /// <param name="hour">The new hour value to set.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> object with the modified hour.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the <paramref name="hour"/> is not within the valid range (0 - 23).
        /// </exception>
        public static DateTimeOffset ChangeHour(this DateTimeOffset dateTimeOffset, int hour)
        {
            if (hour < 0 || hour > 23)
            {
                throw new ArgumentOutOfRangeException(nameof(hour), hour, "Hour value is not within the valid range (0 - 23).");
            }

            return dateTimeOffset.AddHours(hour - dateTimeOffset.Hour);
        }

        /// <summary>
        /// Changes the millisecond of the <paramref name="dateTimeOffset"/> to the specified <paramref name="millisecond"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> object.</param>
        /// <param name="millisecond">The new millisecond value to set.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> object with the modified millisecond.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the <paramref name="millisecond"/> is not within the valid range (0 - 999).
        /// </exception>
        public static DateTimeOffset ChangeMillisecond(this DateTimeOffset dateTimeOffset, int millisecond)
        {
            if (millisecond < 0 || millisecond > 999)
            {
                throw new ArgumentOutOfRangeException(nameof(millisecond), millisecond, "Millisecond value is not within the valid range (0 - 999).");
            }

            return dateTimeOffset.AddMilliseconds(millisecond - dateTimeOffset.Millisecond);
        }

        /// <summary>
        /// Changes the minute of the <paramref name="dateTimeOffset"/> to the specified <paramref name="minute"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> object.</param>
        /// <param name="minute">The new minute value to set.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> object with the modified minute.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the <paramref name="minute"/> is not within the valid range (0 - 59).
        /// </exception>
        public static DateTimeOffset ChangeMinute(this DateTimeOffset dateTimeOffset, int minute)
        {
            if (minute < 0 || minute > 59)
            {
                throw new ArgumentOutOfRangeException(nameof(minute), minute, "Minute value is not within the valid range (0 - 59).");
            }

            return dateTimeOffset.AddMinutes(minute - dateTimeOffset.Minute);
        }

        /// <summary>
        /// Changes the month of the <paramref name="dateTimeOffset"/> to the specified <paramref name="month"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> object.</param>
        /// <param name="month">The new month value to set.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> object with the modified month.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the <paramref name="month"/> is not within the valid range (1 - 12).
        /// </exception>
        public static DateTimeOffset ChangeMonth(this DateTimeOffset dateTimeOffset, int month)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month), month, "Month value is not within the valid range (1 - 12).");
            }

            return dateTimeOffset.AddMonths(month - dateTimeOffset.Month);
        }

        /// <summary>
        /// Changes the second of the <paramref name="dateTimeOffset"/> to the specified <paramref name="second"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> object.</param>
        /// <param name="second">The new second value to set.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> object with the modified second.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the <paramref name="second"/> is not within the valid range (0 - 59).
        /// </exception>
        public static DateTimeOffset ChangeSecond(this DateTimeOffset dateTimeOffset, int second)
        {
            if (second < 0 || second > 59)
            {
                throw new ArgumentOutOfRangeException(nameof(second), second, "Second value is not within the valid range (0 - 59).");
            }

            return dateTimeOffset.AddSeconds(second - dateTimeOffset.Second);
        }

        /// <summary>
        /// Changes the year of the <paramref name="dateTimeOffset"/> to the specified <paramref name="year"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> object.</param>
        /// <param name="year">The new year value to set.</param>
        /// <returns>A new <see cref="DateTimeOffset"/> object with the modified year.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the <paramref name="year"/> is not within the valid range.
        /// </exception>
        public static DateTimeOffset ChangeYear(this DateTimeOffset dateTimeOffset, int year)
        {
            if (year < DateTimeOffset.MinValue.Year || year > DateTimeOffset.MaxValue.Year)
            {
                throw new ArgumentOutOfRangeException(nameof(year), year, "Year value is not within the valid range.");
            }

            return dateTimeOffset.AddYears(year - dateTimeOffset.Year);
        }

        /// <summary>
        /// Gets the current quarter of the year for the specified <paramref name="dateTimeOffset"/>.
        /// Quarters are numbered from 1 to 4.
        /// </summary>
        /// <param name="dateTimeOffset">The <see cref="DateTimeOffset"/> for which to determine the quarter.</param>
        /// <returns>An integer representing the current quarter of the year.</returns>
        public static int GetCurrentQuarter(this DateTimeOffset dateTimeOffset)
        {
            return (dateTimeOffset.Month - 1) / 3 + 1;
        }

        ///// <summary>
        ///// Determines if the current <see cref="DateTimeOffset"/> is after the specified <paramref name="referenceDateTimeOffset"/>.
        ///// </summary>
        ///// <param name="dateTimeOffset">The current <see cref="DateTimeOffset"/> to compare.</param>
        ///// <param name="referenceDateTimeOffset">The reference <see cref="DateTimeOffset"/> for comparison.</param>
        ///// <returns><c>true</c> if the current <see cref="DateTimeOffset"/> is after the reference <see cref="DateTimeOffset"/>; otherwise, <c>false</c>.</returns>
        //public static bool IsAfter(this DateTimeOffset dateTimeOffset, DateTimeOffset referenceDateTimeOffset)
        //{
        //    return dateTimeOffset > referenceDateTimeOffset;
        //}

        ///// <summary>
        ///// Determines if the current <see cref="DateTimeOffset"/> is before the specified <paramref name="referenceDateTimeOffset"/>.
        ///// </summary>
        ///// <param name="dateTimeOffset">The current <see cref="DateTimeOffset"/> to compare.</param>
        ///// <param name="referenceDateTimeOffset">The reference <see cref="DateTimeOffset"/> for comparison.</param>
        ///// <returns><c>true</c> if the current <see cref="DateTimeOffset"/> is before the reference <see cref="DateTimeOffset"/>; otherwise, <c>false</c>.</returns>
        //public static bool IsBefore(this DateTimeOffset dateTimeOffset, DateTimeOffset referenceDateTimeOffset)
        //{
        //    return dateTimeOffset < referenceDateTimeOffset;
        //}

        ///// <summary>
        ///// Determines if the current <see cref="DateTimeOffset"/> is within the date range specified by <paramref name="startDateTimeOffset"/> and <paramref name="endDateTimeOffset"/>.
        ///// </summary>
        ///// <param name="dateTimeOffset">The current <see cref="DateTimeOffset"/> to compare.</param>
        ///// <param name="startDateTimeOffset">The start of the date range for comparison.</param>
        ///// <param name="endDateTimeOffset">The end of the date range for comparison.</param>
        ///// <param name="inclusiveComparison">A flag indicating whether the comparison is inclusive (default) or exclusive.</param>
        ///// <returns><c>true</c> if the current <see cref="DateTimeOffset"/> is within the specified date range; otherwise, <c>false</c>.</returns>
        //public static bool IsBetween(this DateTimeOffset dateTimeOffset, DateTimeOffset startDateTimeOffset, DateTimeOffset endDateTimeOffset, bool inclusiveComparison = true)
        //{
        //    if (inclusiveComparison)
        //    {
        //        return dateTimeOffset >= startDateTimeOffset && dateTimeOffset <= endDateTimeOffset;
        //    }

        //    return dateTimeOffset > startDateTimeOffset && dateTimeOffset < endDateTimeOffset;
        //}

        ///// <summary>
        ///// Determines if the year of the current <see cref="DateTimeOffset"/> is a leap year.
        ///// </summary>
        ///// <param name="dateTimeOffset">The current <see cref="DateTimeOffset"/> to evaluate.</param>
        ///// <returns><c>true</c> if the year of the current <see cref="DateTimeOffset"/> is a leap year; otherwise, <c>false</c>.</returns>
        //public static bool IsLeapYear(this DateTimeOffset dateTimeOffset)
        //{
        //    return DateTimeOffset.IsLeapYear(dateTimeOffset.Year);
        //}

        ///// <summary>
        ///// Determines if the current <see cref="DateTimeOffset"/> is on or after the specified <paramref name="referenceDateTimeOffset"/>.
        ///// </summary>
        ///// <param name="dateTimeOffset">The current <see cref="DateTimeOffset"/> to compare.</param>
        ///// <param name="referenceDateTimeOffset">The reference <see cref="DateTimeOffset"/> for comparison.</param>
        ///// <returns><c>true</c> if the current <see cref="DateTimeOffset"/> is on or after the reference <see cref="DateTimeOffset"/>; otherwise, <c>false</c>.</returns>
        //public static bool IsOnOrAfter(this DateTimeOffset dateTimeOffset, DateTimeOffset referenceDateTimeOffset)
        //{
        //    return dateTimeOffset >= referenceDateTimeOffset;
        //}

        ///// <summary>
        ///// Determines if the current <see cref="DateTimeOffset"/> is on or before the specified <paramref name="referenceDateTimeOffset"/>.
        ///// </summary>
        ///// <param name="dateTimeOffset">The current <see cref="DateTimeOffset"/> to compare.</param>
        ///// <param name="referenceDateTimeOffset">The reference <see cref="DateTimeOffset"/> for comparison.</param>
        ///// <returns><c>true</c> if the current <see cref="DateTimeOffset"/> is on or before the reference <see cref="DateTimeOffset"/>; otherwise, <c>false</c>.</returns>
        //public static bool IsOnOrBefore(this DateTimeOffset dateTimeOffset, DateTimeOffset referenceDateTimeOffset)
        //{
        //    return dateTimeOffset <= referenceDateTimeOffset;
        //}

        ///// <summary>
        ///// Determines if the current <see cref="DateTimeOffset"/> falls on a weekday (Monday to Friday).
        ///// </summary>
        ///// <param name="dateTimeOffset">The current <see cref="DateTimeOffset"/> to evaluate.</param>
        ///// <returns><c>true</c> if the current <see cref="DateTimeOffset"/> is a weekday; otherwise, <c>false</c>.</returns>
        //public static bool IsWeekday(this DateTimeOffset dateTimeOffset)
        //{
        //    return dateTimeOffset.DayOfWeek >= DayOfWeek.Monday && dateTimeOffset.DayOfWeek <= DayOfWeek.Friday;
        //}

        ///// <summary>
        ///// Determines if the current <see cref="DateTimeOffset"/> falls on a weekend day (Saturday or Sunday).
        ///// </summary>
        ///// <param name="dateTimeOffset">The current <see cref="DateTimeOffset"/> to evaluate.</param>
        ///// <returns><c>true</c> if the current <see cref="DateTimeOffset"/> is a weekend day; otherwise, <c>false</c>.</returns>
        //public static bool IsWeekend(this DateTimeOffset dateTimeOffset)
        //{
        //    return dateTimeOffset.DayOfWeek == DayOfWeek.Saturday || dateTimeOffset.DayOfWeek == DayOfWeek.Sunday;
        //}

        ///// <summary>
        ///// Returns a new <see cref="DateTimeOffset"/> representing the specified <paramref name="day"/> of the week for the specified <paramref name="dateTimeOffset"/>.
        ///// </summary>
        ///// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/>.</param>
        ///// <param name="day">The target day of the week.</param>
        ///// <param name="firstDayOfWeek">The first day of the week (default is Monday).</param>
        ///// <returns>A new <see cref="DateTimeOffset"/> set to the specified <paramref name="day"/> of the week.</returns>
        //public static DateTimeOffset OnDayOfWeek(this DateTimeOffset dateTimeOffset, DayOfWeek day, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
        //{
        //    int days = (7 + day - firstDayOfWeek) % 7;
        //    return dateTimeOffset
        //        .OnStartOfWeek(firstDayOfWeek)
        //        .AddDays(days)
        //        .At(dateTimeOffset.TimeOfDay);
        //}

        ///// <summary>
        ///// Returns a new <see cref="DateTimeOffset"/> representing the end of the month at 23:59:59.999 of the original <paramref name="dateTimeOffset"/>.
        ///// </summary>
        ///// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        ///// <returns>A new <see cref="DateTimeOffset"/> set to the end of the month.</returns>
        //public static DateTimeOffset OnEndOfMonth(this DateTimeOffset dateTimeOffset)
        //{
        //    int lastDayOfMonth = DateTimeOffset.DaysInMonth(dateTimeOffset.Year, dateTimeOffset.Month);
        //    return dateTimeOffset
        //        .ChangeDay(lastDayOfMonth)
        //        .AtEndOfDay();
        //}

        ///// <summary>
        ///// Returns a new <see cref="DateTimeOffset"/> representing the end of the quarter for the specified <paramref name="dateTimeOffset"/>.
        ///// Quarters are numbered from 1 to 4.
        ///// </summary>
        ///// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/>.</param>
        ///// <returns>A new <see cref="DateTimeOffset"/> set to the end of the quarter.</returns>
        //public static DateTimeOffset OnEndOfQuarter(this DateTimeOffset dateTimeOffset)
        //{
        //    int currentQuarter = dateTimeOffset.GetCurrentQuarter();
        //    int endOfQuarterMonth = currentQuarter * 3;

        //    return dateTimeOffset
        //        .ChangeMonth(endOfQuarterMonth)
        //        .OnEndOfMonth();
        //}

        ///// <summary>
        ///// Returns a new <see cref="DateTimeOffset"/> representing the end of the week for the specified <paramref name="dateTimeOffset"/>.
        ///// </summary>
        ///// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/>.</param>
        ///// <param name="firstDayOfWeek">The first day of the week (default is Monday).</param>
        ///// <returns>A new <see cref="DateTimeOffset"/> set to the end of the week.</returns>
        //public static DateTimeOffset OnEndOfWeek(this DateTimeOffset dateTimeOffset, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
        //{
        //    int diff = (7 + dateTimeOffset.DayOfWeek - firstDayOfWeek) % 7;
        //    return dateTimeOffset
        //        .Date
        //        .AddDays(6 - diff)
        //        .AtEndOfDay();
        //}

        ///// <summary>
        ///// Returns a new <see cref="DateTimeOffset"/> representing the end of the year on 31th December at 23:59:59.999 of the original <paramref name="dateTimeOffset"/>.
        ///// </summary>
        ///// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        ///// <returns>A new <see cref="DateTimeOffset"/> set to the end of the year.</returns>
        //public static DateTimeOffset OnEndOfYear(this DateTimeOffset dateTimeOffset)
        //{
        //    return dateTimeOffset
        //        .ChangeMonth(12)
        //        .OnEndOfMonth();
        //}

        ///// <summary>
        ///// Returns a new <see cref="DateTimeOffset"/> representing the start of the month at 00:00:00.000 of the original <paramref name="dateTimeOffset"/>.
        ///// </summary>
        ///// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        ///// <returns>A new <see cref="DateTimeOffset"/> set to the start of the month.</returns>
        //public static DateTimeOffset OnStartOfMonth(this DateTimeOffset dateTimeOffset)
        //{
        //    return dateTimeOffset
        //        .ChangeDay(1)
        //        .AtStartOfDay();
        //}

        ///// <summary>
        ///// Returns a new <see cref="DateTimeOffset"/> representing the start of the quarter for the specified <paramref name="dateTimeOffset"/>.
        ///// Quarters are numbered from 1 to 4.
        ///// </summary>
        ///// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/>.</param>
        ///// <returns>A new <see cref="DateTimeOffset"/> set to the start of the quarter.</returns>
        //public static DateTimeOffset OnStartOfQuarter(this DateTimeOffset dateTimeOffset)
        //{
        //    int currentQuarter = dateTimeOffset.GetCurrentQuarter();
        //    int startOfQuarterMonth = (currentQuarter - 1) * 3 + 1;

        //    return dateTimeOffset
        //        .ChangeMonth(startOfQuarterMonth)
        //        .OnStartOfMonth();
        //}

        ///// <summary>
        ///// Returns a new <see cref="DateTimeOffset"/> representing the start of the week for the specified <paramref name="dateTimeOffset"/>.
        ///// </summary>
        ///// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/>.</param>
        ///// <param name="firstDayOfWeek">The first day of the week (default is Monday).</param>
        ///// <returns>A new <see cref="DateTimeOffset"/> set to the start of the week.</returns>
        //public static DateTimeOffset OnStartOfWeek(this DateTimeOffset dateTimeOffset, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
        //{
        //    int diff = (7 + dateTimeOffset.DayOfWeek - firstDayOfWeek) % 7;
        //    return dateTimeOffset.Date.AddDays(-diff);
        //}

        ///// <summary>
        ///// Returns a new <see cref="DateTimeOffset"/> representing the start of the year on 1st January at 00:00:00.000 of the original <paramref name="dateTimeOffset"/>.
        ///// </summary>
        ///// <param name="dateTimeOffset">The original <see cref="DateTimeOffset"/> to adjust.</param>
        ///// <returns>A new <see cref="DateTimeOffset"/> set to the start of the year.</returns>
        //public static DateTimeOffset OnStartOfYear(this DateTimeOffset dateTimeOffset)
        //{
        //    return dateTimeOffset
        //        .ChangeMonth(1)
        //        .OnStartOfMonth();
        //}
    }
}