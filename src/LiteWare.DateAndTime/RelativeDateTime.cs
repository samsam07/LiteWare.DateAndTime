using LiteWare.DateAndTime.Extensions;
using System;
using System.Text;

namespace LiteWare.DateAndTime
{
    /// <summary>
    /// Represents a relative instance in time, typically expressed as a date and time of day.
    /// </summary>
    public class RelativeDateTime
    {
        /// <summary>
        /// A string symbol that represents the year component in a relative date and time string literal.
        /// </summary>
        public const string YearSymbol = "y";

        /// <summary>
        /// A string symbol that represents the month component in a relative date and time string literal.
        /// </summary>
        public const string MonthSymbol = "M";

        /// <summary>
        /// A string symbol that represents the day component in a relative date and time string literal.
        /// </summary>
        public const string DaySymbol = "d";

        /// <summary>
        /// A string symbol that represents the hour component in a relative date and time string literal.
        /// </summary>
        public const string HourSymbol = "H";

        /// <summary>
        /// A string symbol that represents the minute component in a relative date and time string literal.
        /// </summary>
        public const string MinuteSymbol = "m";

        /// <summary>
        /// A string symbol that represents the second component in a relative date and time string literal.
        /// </summary>
        public const string SecondSymbol = "s";

        /// <summary>
        /// A string symbol that represents the millisecond component in a relative date and time string literal.
        /// </summary>
        public const string MillisecondSymbol = "f";

        /// <summary>
        /// Gets or sets the year value.
        /// If <see cref="IsYearValueFixed"/> is set to <c>true</c>, this value represents an actual year; otherwise,
        /// this value represents a relative number of years.
        /// </summary>
        public int YearValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="YearValue"/> is interpreted as a fixed actual year or as a relative number of years.
        /// </summary>
        public bool IsYearValueFixed { get; set; }

        /// <summary>
        /// Gets or sets the month value.
        /// If <see cref="IsMonthValueFixed"/> is set to <c>true</c>, this value represents an actual month; otherwise,
        /// this value represents a relative number of months.
        /// </summary>
        public int MonthValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="MonthValue"/> is interpreted as a fixed actual month or as a relative number of months.
        /// </summary>
        public bool IsMonthValueFixed { get; set; }

        /// <summary>
        /// Gets or sets the day value.
        /// If <see cref="IsDayValueFixed"/> is set to <c>true</c>, this value represents an actual day; otherwise,
        /// this value represents a relative number of days.
        /// </summary>
        public int DayValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="DayValue"/> is interpreted as a fixed actual day or as a relative number of days.
        /// </summary>
        public bool IsDayValueFixed { get; set; }

        /// <summary>
        /// Gets or sets the hour value.
        /// If <see cref="IsHourValueFixed"/> is set to <c>true</c>, this value represents an actual hour; otherwise,
        /// this value represents a relative number of hours.
        /// </summary>
        public int HourValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="HourValue"/> is interpreted as a fixed actual hour or as a relative number of hours.
        /// </summary>
        public bool IsHourValueFixed { get; set; }

        /// <summary>
        /// Gets or sets the minute value.
        /// If <see cref="IsMinuteValueFixed"/> is set to <c>true</c>, this value represents an actual minute; otherwise,
        /// this value represents a relative number of minutes.
        /// </summary>
        public int MinuteValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="MinuteValue"/> is interpreted as a fixed actual minute or as a relative number of minutes.
        /// </summary>
        public bool IsMinuteValueFixed { get; set; }

        /// <summary>
        /// Gets or sets the second value.
        /// If <see cref="IsSecondValueFixed"/> is set to <c>true</c>, this value represents an actual second; otherwise,
        /// this value represents a relative number of seconds.
        /// </summary>
        public int SecondValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="SecondValue"/> is interpreted as a fixed actual second or as a relative number of seconds.
        /// </summary>
        public bool IsSecondValueFixed { get; set; }

        /// <summary>
        /// Gets or sets the millisecond value.
        /// If <see cref="IsMillisecondValueFixed"/> is set to <c>true</c>, this value represents an actual millisecond; otherwise,
        /// this value represents a relative number of milliseconds.
        /// </summary>
        public int MillisecondValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="MillisecondValue"/> is interpreted as a fixed actual millisecond or as a relative number of milliseconds.
        /// </summary>
        public bool IsMillisecondValueFixed { get; set; }

        /// <summary>
        /// Parses a string literal to create a <see cref="RelativeDateTime"/> object.
        /// </summary>
        /// <param name="literal">The string literal to parse.</param>
        /// <returns>A <see cref="RelativeDateTime"/> object representing the parsed value.</returns>
        public static RelativeDateTime Parse(string literal)
        {
            return RelativeDateTimeParser.Parse(literal);
        }

        // TODO: TryParse

        /// <summary>
        /// Evaluates the relative date and time to calculate an absolute <see cref="DateTime"/> value based on the current date and time.
        /// </summary>
        /// <returns>The evaluated <see cref="DateTime"/> value.</returns>
        public DateTime Evaluate()
        {
            return Evaluate(DateTime.Now);
        }

        /// <summary>
        /// Evaluates the relative date and time to calculate an absolute <see cref="DateTime"/> value based on the provided <paramref name="dateTimeReference"/>.
        /// </summary>
        /// <param name="dateTimeReference">The reference <see cref="DateTime"/> for the evaluation.</param>
        /// <returns>The evaluated <see cref="DateTime"/> value.</returns>
        public DateTime Evaluate(DateTime dateTimeReference)
        {
            if (IsYearValueFixed)
            {
                dateTimeReference = dateTimeReference.ChangeYear(YearValue);
            }
            else if (YearValue != 0)
            {
                dateTimeReference = dateTimeReference.AddYears(YearValue);
            }

            if (IsMonthValueFixed)
            {
                dateTimeReference = dateTimeReference.ChangeMonth(MonthValue);
            }
            else if (MonthValue != 0)
            {
                dateTimeReference = dateTimeReference.AddMonths(MonthValue);
            }

            if (IsDayValueFixed)
            {
                dateTimeReference = dateTimeReference.ChangeDay(DayValue);
            }
            else if (DayValue != 0)
            {
                dateTimeReference = dateTimeReference.AddDays(DayValue);
            }

            if (IsHourValueFixed)
            {
                dateTimeReference = dateTimeReference.ChangeHour(HourValue);
            }
            else if (HourValue != 0)
            {
                dateTimeReference = dateTimeReference.AddHours(HourValue);
            }

            if (IsMinuteValueFixed)
            {
                dateTimeReference = dateTimeReference.ChangeMinute(MinuteValue);
            }
            else if (MinuteValue != 0)
            {
                dateTimeReference = dateTimeReference.AddMinutes(MinuteValue);
            }

            if (IsSecondValueFixed)
            {
                dateTimeReference = dateTimeReference.ChangeSecond(SecondValue);
            }
            else if (SecondValue != 0)
            {
                dateTimeReference = dateTimeReference.AddSeconds(SecondValue);
            }

            if (IsMillisecondValueFixed)
            {
                dateTimeReference = dateTimeReference.ChangeMillisecond(MillisecondValue);
            }
            else if (MillisecondValue != 0)
            {
                dateTimeReference = dateTimeReference.AddMilliseconds(MillisecondValue);
            }

            return dateTimeReference;
        }

        /// <summary>
        /// Returns a string representation of the <see cref="RelativeDateTime"/> object.
        /// </summary>
        /// <returns>A string representation of the <see cref="RelativeDateTime"/> object.</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            Append(YearValue, YearSymbol, IsYearValueFixed);
            Append(MonthValue, MonthSymbol, IsMonthValueFixed);
            Append(DayValue, DaySymbol, IsDayValueFixed);
            Append(HourValue, HourSymbol, IsHourValueFixed);
            Append(MinuteValue, MinuteSymbol, IsMinuteValueFixed);
            Append(SecondValue, SecondSymbol, IsSecondValueFixed);
            Append(MillisecondValue, MillisecondSymbol, IsMillisecondValueFixed);

            return stringBuilder.ToString();

            void Append(int value, string valueSymbol, bool isValueFixed)
            {
                if (isValueFixed)
                {
                    AppendSeparatorIfNeeded();
                    stringBuilder.Append(value);
                    stringBuilder.Append(valueSymbol);
                }
                else if (value != 0)
                {
                    AppendSeparatorIfNeeded();
                    if (value > 0)
                    {
                        stringBuilder.Append('+');
                    }

                    stringBuilder.Append(value);
                    stringBuilder.Append(valueSymbol);
                }
            }

            void AppendSeparatorIfNeeded()
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append(' ');
                }
            }
        }

        /// <summary>
        /// Implicitly converts a string literal to a <see cref="RelativeDateTime"/> object.
        /// </summary>
        /// <param name="literal">The string literal to convert.</param>
        /// <returns>A <see cref="RelativeDateTime"/> object representing the parsed value.</returns>
        public static implicit operator RelativeDateTime(string literal)
        {
            return Parse(literal);
        }
    }
}