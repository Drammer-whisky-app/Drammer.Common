using Drammer.Common.Extensions;
using FluentAssertions;

namespace Drammer.Common.Tests.Extensions;

public sealed class DateTimeExtensionsTests
{
    [Theory]
    [InlineData(2016, 1, 1, 2017)]
    [InlineData(2050, 1, 1, 2023)]
    [InlineData(2020, 2, 29, 2021)]
    public void ChangeYear(int currentYear, int currentMonth, int currentDay, int newYear)
    {
        // arrange
        var dateTime = new DateTime(currentYear, currentMonth, currentDay);

        // act
        var result = dateTime.ChangeYear(newYear);

        // assert
        result.Year.Should().Be(newYear);
    }
}