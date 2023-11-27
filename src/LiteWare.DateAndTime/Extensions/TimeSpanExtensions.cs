using System;

namespace LiteWare.DateAndTime.Extensions
{
    /// <summary>
    /// Provides extension methods for manipulating and modifying <see cref="TimeSpan"/> objects.
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Determines if the current <see cref="TimeSpan"/> is after the specified <paramref name="referenceTimeSpan"/>.
        /// </summary>
        /// <param name="timeSpan">The current <see cref="TimeSpan"/> to compare.</param>
        /// <param name="referenceTimeSpan">The reference <see cref="TimeSpan"/> for comparison.</param>
        /// <returns><c>true</c> if the current <see cref="TimeSpan"/> is after the reference <see cref="TimeSpan"/>; otherwise, <c>false</c>.</returns>
        public static bool IsAfter(this TimeSpan timeSpan, TimeSpan referenceTimeSpan)
        {
            return timeSpan > referenceTimeSpan;
        }

        /// <summary>
        /// Determines if the current <see cref="TimeSpan"/> is before the specified <paramref name="referenceTimeSpan"/>.
        /// </summary>
        /// <param name="timeSpan">The current <see cref="TimeSpan"/> to compare.</param>
        /// <param name="referenceTimeSpan">The reference <see cref="TimeSpan"/> for comparison.</param>
        /// <returns><c>true</c> if the current <see cref="TimeSpan"/> is before the reference <see cref="TimeSpan"/>; otherwise, <c>false</c>.</returns>
        public static bool IsBefore(this TimeSpan timeSpan, TimeSpan referenceTimeSpan)
        {
            return timeSpan < referenceTimeSpan;
        }

        /// <summary>
        /// Determines if the current <see cref="TimeSpan"/> is within the date range specified by <paramref name="startTimeSpan"/> and <paramref name="endTimeSpan"/>.
        /// </summary>
        /// <param name="timeSpan">The current <see cref="TimeSpan"/> to compare.</param>
        /// <param name="startTimeSpan">The start of the date range for comparison.</param>
        /// <param name="endTimeSpan">The end of the date range for comparison.</param>
        /// <param name="inclusiveComparison">A flag indicating whether the comparison is inclusive (default) or exclusive.</param>
        /// <returns><c>true</c> if the current <see cref="TimeSpan"/> is within the specified date range; otherwise, <c>false</c>.</returns>
        public static bool IsBetween(this TimeSpan timeSpan, TimeSpan startTimeSpan, TimeSpan endTimeSpan, bool inclusiveComparison = true)
        {
            if (inclusiveComparison)
            {
                return timeSpan >= startTimeSpan && timeSpan <= endTimeSpan;
            }

            return timeSpan > startTimeSpan && timeSpan < endTimeSpan;
        }

        /// <summary>
        /// Determines if the current <see cref="TimeSpan"/> is on or after the specified <paramref name="referenceTimeSpan"/>.
        /// </summary>
        /// <param name="timeSpan">The current <see cref="TimeSpan"/> to compare.</param>
        /// <param name="referenceTimeSpan">The reference <see cref="TimeSpan"/> for comparison.</param>
        /// <returns><c>true</c> if the current <see cref="TimeSpan"/> is on or after the reference <see cref="TimeSpan"/>; otherwise, <c>false</c>.</returns>
        public static bool IsOnOrAfter(this TimeSpan timeSpan, TimeSpan referenceTimeSpan)
        {
            return timeSpan >= referenceTimeSpan;
        }

        /// <summary>
        /// Determines if the current <see cref="TimeSpan"/> is on or before the specified <paramref name="referenceTimeSpan"/>.
        /// </summary>
        /// <param name="timeSpan">The current <see cref="TimeSpan"/> to compare.</param>
        /// <param name="referenceTimeSpan">The reference <see cref="TimeSpan"/> for comparison.</param>
        /// <returns><c>true</c> if the current <see cref="TimeSpan"/> is on or before the reference <see cref="TimeSpan"/>; otherwise, <c>false</c>.</returns>
        public static bool IsOnOrBefore(this TimeSpan timeSpan, TimeSpan referenceTimeSpan)
        {
            return timeSpan <= referenceTimeSpan;
        }
    }
}