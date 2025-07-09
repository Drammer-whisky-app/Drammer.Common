using Drammer.Common.Extensions;

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

    [Fact]
    public void GetMinDate_CurrentValueIsMinDate_ShouldReturnMinDate()
    {
        // arrange
        var minDate = (DateTime?)DateTime.MinValue;

        // act
        var result = minDate.GetMinDate(DateTime.Now);

        // assert
        result.Should().Be(DateTime.MinValue);
    }

    [Fact]
    public void GetMinDate_GivenValueIsMinDate_ShouldReturnMinDate()
    {
        // arrange
        var maxDate = (DateTime?)DateTime.MaxValue;
        var now = DateTime.Now;

        // act
        var result = maxDate.GetMinDate(now);

        // assert
        result.Should().Be(now);
    }

    [Fact]
    public void GetMaxDate_CurrentValueIsMaxDate_ShouldReturnMaxDate()
    {
        // arrange
        var minDate = (DateTime?)DateTime.MinValue;
        var now = DateTime.Now;

        // act
        var result = minDate.GetMaxDate(now);

        // assert
        result.Should().Be(now);
    }

    [Fact]
    public void GetMaxDate_GivenValueIsMaxDate_ShouldReturnMaxDate()
    {
        // arrange
        var maxDate = (DateTime?)DateTime.MaxValue;
        var now = DateTime.Now;

        // act
        var result = maxDate.GetMaxDate(now);

        // assert
        result.Should().Be(maxDate);
    }
}