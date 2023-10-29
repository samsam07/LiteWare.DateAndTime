# LiteWare.DateAndTime

[![Nuget](https://img.shields.io/nuget/v/LiteWare.DateAndTime)](https://www.nuget.org/packages/LiteWare.DateAndTime)
[![License](https://img.shields.io/github/license/samsam07/LiteWare.DateAndTime)](https://github.com/samsam07/LiteWare.DateAndTime/blob/master/LICENSE)
![Project Status](https://img.shields.io/badge/Project_Status-Alpha_--_Work_in_progress-orange)

LiteWare.DateAndTime is a library that extends and enhances date and time functionality in C#.

The project is currently in an alpha phase, representing the minimum viable product with ongoing development.

## RelativeDateTime

`RelativeDateTime` allows you to work with relative date and time values in a flexible and intuitive manner. It provides the ability to manipulate date and time components (year, month, day, hour, minute, second, and millisecond) in both absolute and relative terms. This makes it a powerful tool for dynamic date and time calculations, allowing you to represent values as either fixed or relative, and seamlessly parse date and time expressions.

### Usage

#### Example: Calculating yesterday's start of day at midnight

``` csharp
RelativeDateTime relativeDateTime = RelativeDateTime.Parse("-1d @ 0H 0m 0s");
// relativeDateTime = "-1d @ 0H 0m 0s"; // Another way of building RelativeDateTime
DateTime yesterdayStartOfDay = relativeDateTime.Evaluate();
```

In this code, we are calculating yestarday's start of day at midnight.

In expressions, signed values ("+" or "-") are interpreted as relative date/time values, while those without signs are interpreted as absolute date/time values. In the example, "-1d" represents the previous day (a relative date), and "0H 0m 0s" specifies the absolute time of midnight.

The possible date and time value types include:

- Year (represented as `"y"`)
- Month (represented as `"M"`)
- Day (represented as `"d"`)
- Hour (represented as `"H"`)
- Minute (represented as `"m"`)
- Second (represented as `"s"`)
- Millisecond (represented as `"f"`)
