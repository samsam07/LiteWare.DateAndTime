using System;

namespace LiteWare.DateAndTime
{
    internal static class RelativeDateTimeParser
    {
        public static RelativeDateTime Parse(string literal)
        {
            if (string.IsNullOrWhiteSpace(literal))
            {
                throw new ArgumentNullException(nameof(literal), "The provided string literal is null, empty or white space.");
            }

            int iCurrent = 0;
            int length = literal.Length;

            RelativeDateTime parsedRelativeDateTime = new RelativeDateTime();
            while (iCurrent < length)
            {
                // Determine if value is fixed or relative

                char sign = literal[iCurrent];
                bool isFixedValue;
                if (sign == '+' || sign == '-')
                {
                    isFixedValue = false;
                    iCurrent++;
                }
                else
                {
                    isFixedValue = true;
                    sign = '\0';
                }

                // Extract value

                int? value = null;
                while (iCurrent < length && char.IsDigit(literal[iCurrent]))
                {
                    value = (value ?? 0) * 10 + (literal[iCurrent] - '0');
                    iCurrent++;
                }

                // Extract value type

                char valueType = '\0';
                if (iCurrent < length && char.IsLetter(literal[iCurrent]))
                {
                    valueType = literal[iCurrent];
                }

                // Process extracted values
                if (value.HasValue && valueType != '\0')
                {
                    ProcessExtractedValues(parsedRelativeDateTime, sign, value.Value, isFixedValue, valueType.ToString());
                }

                iCurrent++;
            }

            return parsedRelativeDateTime;
        }

        private static void ProcessExtractedValues(RelativeDateTime relativeDateTime, char sign, int value, bool isFixedValue, string valueType)
        {
            if (sign == '-')
            {
                value = -value;
            }

            switch (valueType)
            {
                case RelativeDateTime.YearSymbol:
                    relativeDateTime.YearValue = value;
                    relativeDateTime.IsYearValueFixed = isFixedValue;
                    break;

                case RelativeDateTime.MonthSymbol:
                    relativeDateTime.MonthValue = value;
                    relativeDateTime.IsMonthValueFixed = isFixedValue;
                    break;

                case RelativeDateTime.DaySymbol:
                    relativeDateTime.DayValue = value;
                    relativeDateTime.IsDayValueFixed = isFixedValue;
                    break;

                case RelativeDateTime.HourSymbol:
                    relativeDateTime.HourValue = value;
                    relativeDateTime.IsHourValueFixed = isFixedValue;
                    break;

                case RelativeDateTime.Minuteymbol:
                    relativeDateTime.MinuteValue = value;
                    relativeDateTime.IsMinuteValueFixed = isFixedValue;
                    break;

                case RelativeDateTime.SecondSymbol:
                    relativeDateTime.SecondValue = value;
                    relativeDateTime.IsSecondValueFixed = isFixedValue;
                    break;

                case RelativeDateTime.MillisecondSymbol:
                    relativeDateTime.MillisecondValue = value;
                    relativeDateTime.IsMillisecondValueFixed = isFixedValue;
                    break;

                default:
                    throw new NotSupportedException($"RelativeDateTime value-type '{valueType}' is not supported.");
            }
        }
    }
}