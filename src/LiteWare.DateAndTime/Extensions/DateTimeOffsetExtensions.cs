using System;

namespace LiteWare.DateAndTime.Extensions
{
    // Inspired from https://edgeguides.rubyonrails.org/active_support_core_extensions.html#extensions-to-date
    // Change methods inspired from https://stackoverflow.com/a/41805608/5240378

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
            TimeSpan timeZoneOffset = GetTimeZoneOffsetFromKind(kind, dateTimeOffset.Offset);
            return dateTimeOffset.At(time, timeZoneOffset);
        }

        private static TimeSpan GetTimeZoneOffsetFromKind(DateTimeKind kind, TimeSpan defaultTimeOffset)
        {
            switch (kind)
            {
                case DateTimeKind.Unspecified:
                    return defaultTimeOffset;

                case DateTimeKind.Utc:
                    return TimeZoneInfo.Utc.BaseUtcOffset;

                case DateTimeKind.Local:
                    return TimeZoneInfo.Local.BaseUtcOffset;

                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, $"Unsupported {nameof(DateTimeKind)} value.");
            }
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

        public static int GetCurrentQuarter(this DateTimeOffset dateTimeOffset, TimeSpan timeZoneOffset)
        {
            int month = dateTimeOffset.ToOffset(timeZoneOffset).Month;
            return (month - 1) / 3 + 1;
        }

        public static int GetCurrentQuarter(this DateTimeOffset dateTimeOffset, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            TimeSpan timeZoneOffset = GetTimeZoneOffsetFromKind(kind, dateTimeOffset.Offset);
            return dateTimeOffset.GetCurrentQuarter(timeZoneOffset);
        }

        //public static bool IsAfter(this DateTimeOffset dateTimeOffset, DateTimeOffset referenceDateTimeOffset, TimeSpan timeZoneOffset)
        //{
        //    return dateTimeOffset.ToOffset(timeZoneOffset) > referenceDateTimeOffset.ToOffset???;
        //}

        public static bool IsAfter(this DateTimeOffset dateTimeOffset, DateTimeOffset referenceDateTimeOffset, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            TimeSpan timeZoneOffset = GetTimeZoneOffsetFromKind(kind, dateTimeOffset.Offset);
            return dateTimeOffset.IsAfter(referenceDateTimeOffset, timeZoneOffset);
        }

        public static bool IsAfter(this DateTimeOffset dateTimeOffset, DateTime referenceDateTime, TimeSpan timeZoneOffset)
        {
            return dateTimeOffset.ToOffset(timeZoneOffset) > referenceDateTimeOffset;
        }

        //public static bool IsAfter(this DateTimeOffset dateTimeOffset, TimeSpan referenceTimeOfDay)

        //public static bool IsBefore(this DateTimeOffset dateTimeOffset, DateTimeOffset referenceDateTimeOffset)
        //{
        //    return dateTimeOffset < referenceDateTimeOffset;
        //}

        //public static bool IsBefore(this DateTimeOffset dateTimeOffset, TimeSpan referenceTimeOfDay)

        //public static bool IsBetween(this DateTimeOffset dateTimeOffset, DateTimeOffset startDateTimeOffset, DateTimeOffset endDateTimeOffset, bool inclusiveComparison = true)
        //{
        //    if (inclusiveComparison)
        //    {
        //        return dateTimeOffset >= startDateTimeOffset && dateTimeOffset <= endDateTimeOffset;
        //    }

        //    return dateTimeOffset > startDateTimeOffset && dateTimeOffset < endDateTimeOffset;
        //}

        //public static bool IsBetween(this DateTimeOffset dateTimeOffset, TimeSpan startDateTimeOfDay, TimeSpan endDateTimeOfDay, bool inclusiveComparison = true)

        public static bool IsLeapYear(this DateTimeOffset dateTimeOffset, TimeSpan timeZoneOffset)
        {
            int year  = dateTimeOffset.ToOffset(timeZoneOffset).Year;
            return DateTime.IsLeapYear(year);
        }

        public static bool IsLeapYear(this DateTimeOffset dateTimeOffset, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            TimeSpan timeZoneOffset = GetTimeZoneOffsetFromKind(kind, dateTimeOffset.Offset);
            return dateTimeOffset.IsLeapYear(timeZoneOffset);
        }

        //public static bool IsOnOrAfter(this DateTimeOffset dateTimeOffset, DateTimeOffset referenceDateTimeOffset)
        //{
        //    return dateTimeOffset >= referenceDateTimeOffset;
        //}

        //public static bool IsAtOrAfter(this DateTimeOffset dateTimeOffset, TimeSpan referenceTimeOfDay)

        //public static bool IsOnOrBefore(this DateTimeOffset dateTimeOffset, DateTimeOffset referenceDateTimeOffset)
        //{
        //    return dateTimeOffset <= referenceDateTimeOffset;
        //}

        //public static bool IsAtOrBefore(this DateTimeOffset dateTimeOffset, TimeSpan referenceTimeOfDay)

        public static bool IsWeekday(this DateTimeOffset dateTimeOffset, TimeSpan timeZoneOffset)
        {
            DayOfWeek adjustedDayOfWeek = dateTimeOffset.ToOffset(timeZoneOffset).DayOfWeek;
            return adjustedDayOfWeek >= DayOfWeek.Monday && adjustedDayOfWeek <= DayOfWeek.Friday;
        }

        public static bool IsWeekday(this DateTimeOffset dateTimeOffset, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            TimeSpan timeZoneOffset = GetTimeZoneOffsetFromKind(kind, dateTimeOffset.Offset);
            return dateTimeOffset.IsWeekday(timeZoneOffset);
        }

        public static bool IsWeekend(this DateTimeOffset dateTimeOffset, TimeSpan timeZoneOffset)
        {
            DayOfWeek adjustedDayOfWeek = dateTimeOffset.ToOffset(timeZoneOffset).DayOfWeek;
            return adjustedDayOfWeek == DayOfWeek.Saturday || adjustedDayOfWeek == DayOfWeek.Sunday;
        }

        public static bool IsWeekend(this DateTimeOffset dateTimeOffset, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            TimeSpan timeZoneOffset = GetTimeZoneOffsetFromKind(kind, dateTimeOffset.Offset);
            return dateTimeOffset.IsWeekend(timeZoneOffset);
        }

        public static DateTimeOffset OnEndOfMonth(this DateTimeOffset dateTimeOffset, TimeSpan timeZoneOffset)
        {
            int lastDayOfMonth = DateTime.DaysInMonth(dateTimeOffset.Year, dateTimeOffset.Month);
            return dateTimeOffset
                .ChangeDay(lastDayOfMonth)
                .AtEndOfDay(timeZoneOffset);
        }

        public static DateTimeOffset OnEndOfMonth(this DateTimeOffset dateTimeOffset, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            TimeSpan timeZoneOffset = GetTimeZoneOffsetFromKind(kind, dateTimeOffset.Offset);
            return dateTimeOffset.OnEndOfMonth(timeZoneOffset);
        }

        public static DateTimeOffset OnEndOfQuarter(this DateTimeOffset dateTimeOffset, TimeSpan timeZoneOffset)
        {
            int currentQuarter = dateTimeOffset.GetCurrentQuarter();
            int endOfQuarterMonth = currentQuarter * 3;

            return dateTimeOffset
                .ChangeMonth(endOfQuarterMonth)
                .OnEndOfMonth(timeZoneOffset);
        }

        public static DateTimeOffset OnEndOfQuarter(this DateTimeOffset dateTimeOffset, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            TimeSpan timeZoneOffset = GetTimeZoneOffsetFromKind(kind, dateTimeOffset.Offset);
            return dateTimeOffset.OnEndOfQuarter(timeZoneOffset);
        }

        public static DateTimeOffset OnEndOfWeek(this DateTimeOffset dateTimeOffset, TimeSpan timeZoneOffset, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
        {
            int diff = (7 + dateTimeOffset.DayOfWeek - firstDayOfWeek) % 7;
            return dateTimeOffset
                .AddDays(6 - diff)
                .AtEndOfDay(timeZoneOffset);
        }

        public static DateTimeOffset OnEndOfWeek(this DateTimeOffset dateTimeOffset, DateTimeKind kind = DateTimeKind.Unspecified, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
        {
            TimeSpan timeZoneOffset = GetTimeZoneOffsetFromKind(kind, dateTimeOffset.Offset); 
            return dateTimeOffset.OnEndOfWeek(timeZoneOffset);
        }

        public static DateTimeOffset OnEndOfYear(this DateTimeOffset dateTimeOffset, TimeSpan timeZoneOffset)
        {
            return dateTimeOffset
                .ChangeMonth(12)
                .OnEndOfMonth(timeZoneOffset);
        }

        public static DateTimeOffset OnEndOfYear(this DateTimeOffset dateTimeOffset, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            TimeSpan timeZoneOffset = GetTimeZoneOffsetFromKind(kind, dateTimeOffset.Offset);
            return dateTimeOffset.OnEndOfYear(timeZoneOffset);
        }

        public static DateTimeOffset OnStartOfMonth(this DateTimeOffset dateTimeOffset, TimeSpan timeZoneOffset)
        {
            return dateTimeOffset
                .ChangeDay(1)
                .AtStartOfDay(timeZoneOffset);
        }

        public static DateTimeOffset OnStartOfMonth(this DateTimeOffset dateTimeOffset, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            TimeSpan timeZoneOffset = GetTimeZoneOffsetFromKind(kind, dateTimeOffset.Offset);
            return dateTimeOffset.OnStartOfMonth(timeZoneOffset);
        }

        public static DateTimeOffset OnStartOfQuarter(this DateTimeOffset dateTimeOffset, TimeSpan timeZoneOffset)
        {
            int currentQuarter = dateTimeOffset.GetCurrentQuarter(timeZoneOffset);
            int startOfQuarterMonth = (currentQuarter - 1) * 3 + 1;

            return dateTimeOffset
                .ChangeMonth(startOfQuarterMonth)
                .OnStartOfMonth(timeZoneOffset);
        }

        public static DateTimeOffset OnStartOfQuarter(this DateTimeOffset dateTimeOffset, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            TimeSpan timeZoneOffset = GetTimeZoneOffsetFromKind(kind, dateTimeOffset.Offset);
            return dateTimeOffset.OnStartOfQuarter(timeZoneOffset);
        }

        public static DateTimeOffset OnStartOfWeek(this DateTimeOffset dateTimeOffset, TimeSpan timeZoneOffset, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
        {
            DayOfWeek dayOfWeek = dateTimeOffset.ToOffset(timeZoneOffset).DayOfWeek;
            int diff = (7 + dayOfWeek - firstDayOfWeek) % 7;

            return dateTimeOffset
                .AtStartOfDay(timeZoneOffset)
                .AddDays(-diff);
        }

        public static DateTimeOffset OnStartOfWeek(this DateTimeOffset dateTimeOffset, DateTimeKind kind = DateTimeKind.Unspecified, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
        {
            TimeSpan timeZoneOffset = GetTimeZoneOffsetFromKind(kind, dateTimeOffset.Offset);
            return dateTimeOffset.OnStartOfWeek(timeZoneOffset, firstDayOfWeek);
        }

        public static DateTimeOffset OnStartOfYear(this DateTimeOffset dateTimeOffset, TimeSpan timeZoneOffset)
        {
            return dateTimeOffset
                .ChangeMonth(1)
                .OnStartOfMonth(timeZoneOffset);
        }

        public static DateTimeOffset OnStartOfYear(this DateTimeOffset dateTimeOffset, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            TimeSpan timeZoneOffset = GetTimeZoneOffsetFromKind(kind, dateTimeOffset.Offset);
            return dateTimeOffset.OnStartOfYear(timeZoneOffset);
        }
    }
}