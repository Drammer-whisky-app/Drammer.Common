using System.Globalization;
using Drammer.Common.ComponentModel;

namespace Drammer.Common.Tests.ComponentModel;

public sealed class EnumExtensionsTests
{
    [Fact]
    public void DisplayName_ReturnsEN()
    {
        // arrange
        var value1 = TestEnum1.Value1;
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

        // act
        var result = value1.DisplayName();

        // assert
        result.Should().Be("value1 EN");
    }

    [Fact]
    public void DisplayName_ReturnsNL()
    {
        // arrange
        var value2 = TestEnum1.Value2;
        Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("nl-NL");

        // act
        var result = value2.DisplayName();

        // assert
        result.Should().Be("value2 NL");
    }

    [Fact]
    public void DisplayName_NoResource_ReturnValue()
    {
        // arrange
        var value3 = TestEnum1.Value3;
        Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("nl-NL");

        // act
        var result = value3.DisplayName();

        // assert
        result.Should().Be("value3!");
    }

    [Fact]
    public void DisplayName_NoAttribute_ReturnValue()
    {
        // arrange
        var value4 = TestEnum1.Value4;
        Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("nl-NL");

        // act
        var result = value4.DisplayName();

        // assert
        result.Should().Be("Value4");
    }

    private enum TestEnum1
    {
        [LocalizedDisplayName("value1", NameResourceType = typeof(Resources.Test))]
        Value1,

        [LocalizedDisplayName("value2", NameResourceType = typeof(Resources.Test))]
        Value2,

        [LocalizedDisplayName("value3!")]
        Value3,

        Value4,
    }
}