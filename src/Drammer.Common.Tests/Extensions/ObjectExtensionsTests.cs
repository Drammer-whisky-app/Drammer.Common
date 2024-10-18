using Drammer.Common.Extensions;

namespace Drammer.Common.Tests.Extensions;

public sealed class ObjectExtensionsTests
{
    private readonly IFixture _fixture = new Fixture();

    [Fact]
    public void IsNotNull_WhenValueIsNull_ThenReturnsFalse()
    {
        // Arrange
        object? val = null;

        // Act
        var result = val.IsNotNull();

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsNotNull_WhenValueIsNotNull_ThenReturnsTrue()
    {
        // Arrange
        object val = new ();

        // Act
        var result = val.IsNotNull();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsNull_WhenValueIsNull_ThenReturnsTrue()
    {
        // Arrange
        object? val = null;

        // Act
        var result = val.IsNull();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsNull_WhenValueIsNotNull_ThenReturnsFalse()
    {
        // Arrange
        object val = new ();

        // Act
        var result = val.IsNull();

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(-1, false)]
    [InlineData(1, false)]
    public void IsDefault_Int(int value, bool isDefault)
    {
        // Act
        var result = value.IsDefault();

        // Assert
        result.Should().Be(isDefault);
    }

    [Theory]
    [InlineData("", true)]
    [InlineData(" ", false)]
    [InlineData("a", false)]
    public void IsDefault_String(string value, bool isDefault)
    {
        // Act
        var result = value.IsDefault();

        // Assert
        result.Should().Be(isDefault);
    }

    [Fact]
    public void WithValue_ValueSet_ReturnsValue()
    {
        // arrange
        var model = _fixture.Create<TestModel>();
        var intValue = _fixture.Create<int>();
        var stringValue = _fixture.Create<string>();
        var enumValue = _fixture.Create<TestEnum>();
        var dateTimeValue = _fixture.Create<DateTime>();

        // act
        model = model
            .WithValue(x => x.Id, intValue)
            .WithValue(x => x.Value, stringValue)
            .WithValue(x => x.NullableId, intValue)
            .WithValue(x => x.NullableValue, stringValue)
            .WithValue(x => x.NullableEnumValue, enumValue)
            .WithValue(x => x.NullableDateTimeValue, dateTimeValue);

        // assert
        model.Id.Should().Be(intValue);
        model.Value.Should().Be(stringValue);
        model.NullableId.Should().Be(intValue);
        model.NullableValue.Should().Be(stringValue);
        model.NullableEnumValue.Should().Be(enumValue);
        model.NullableDateTimeValue.Should().Be(dateTimeValue);
    }

    [Fact]
    public void WithNullableValue_NonNullableValuesSet_ReturnsValue()
    {
        // arrange
        var model = _fixture.Create<TestModel>();
        var intValue = _fixture.Create<int>();
        var stringValue = _fixture.Create<string>();
        var enumValue = _fixture.Create<TestEnum>();
        var dateTimeValue = _fixture.Create<DateTime>();

        // act
        model = model
            .WithNullableValue(x => x.NullableId, intValue)
            .WithNullableValue(x => x.NullableValue, stringValue)
            .WithNullableValue(x => x.NullableEnumValue, enumValue)
            .WithNullableValue(x => x.NullableDateTimeValue, dateTimeValue);

        // assert
        model.NullableId.Should().Be(intValue);
        model.NullableValue.Should().Be(stringValue);
        model.NullableEnumValue.Should().Be(enumValue);
        model.NullableDateTimeValue.Should().Be(dateTimeValue);
    }

    [Fact]
    public void WithNullableValue_NonNullableValuesSetToNull_ReturnsInitialValue()
    {
        // arrange
        var model = _fixture.Create<TestModel>();
        var intValue = (int?)null;
        var stringValue = (string?)null;
        var enumValue = (TestEnum?)null;
        var dateTimeValue = (DateTime?)null;

        // act
        model = model
            .WithNullableValue(x => x.NullableId, intValue)
            .WithNullableValue(x => x.NullableValue, stringValue)
            .WithNullableValue(x => x.NullableEnumValue, enumValue)
            .WithNullableValue(x => x.NullableDateTimeValue, dateTimeValue);

        // assert
        model.NullableId.Should().Be(model.NullableId);
        model.NullableValue.Should().Be(model.NullableValue);
        model.NullableEnumValue.Should().Be(model.NullableEnumValue);
        model.NullableDateTimeValue.Should().Be(model.NullableDateTimeValue);
    }

    [Fact]
    public void WithValue_NonNullableValuesSetToNull_ReturnsInitialValue()
    {
        // arrange
        var model = _fixture.Create<TestModel>();
        var intValue = (int?)null;
        var stringValue = (string?)null;
        var enumValue = (TestEnum?)null;
        var dateTimeValue = (DateTime?)null;

        // act
        model = model
            .WithValue(x => x.NullableId, intValue)
            .WithValue(x => x.NullableValue, stringValue)
            .WithValue(x => x.NullableEnumValue, enumValue)
            .WithValue(x => x.NullableDateTimeValue, dateTimeValue);

        // assert
        model.NullableId.Should().Be(model.NullableId);
        model.NullableValue.Should().Be(model.NullableValue);
        model.NullableEnumValue.Should().Be(model.NullableEnumValue);
        model.NullableDateTimeValue.Should().Be(model.NullableDateTimeValue);
    }

    [Fact]
    public void WithValue_WithDefaultValues_ConvertToNullOrDefault()
    {
        // arrange
        var model = _fixture.Create<TestModel>();

        // act
        model = model
            .WithValue(x => x.Id, default)
            .WithValue(x => x.Value, string.Empty)
            .WithValue(x => x.NullableId, default(int))
            .WithValue(x => x.NullableValue, string.Empty)
            .WithValue(x => x.EnumValue, default)
            .WithValue(x => x.NullableEnumValue, default(TestEnum))
            .WithValue(x => x.DateTimeValue, default)
            .WithValue(x => x.NullableDateTimeValue, default(DateTime));

        // assert
        model.Id.Should().Be(default);
        model.Value.Should().BeNull();
        model.NullableId.Should().BeNull();
        model.NullableValue.Should().BeNull();
        model.EnumValue.Should().Be(default);
        model.NullableEnumValue.Should().Be(default); // enum should not convert to null when default
        model.DateTimeValue.Should().Be(default);
        model.NullableDateTimeValue.Should().BeNull();
    }

    private class TestModel
    {
        public int Id { get; set; }

        public int? NullableId { get; set; }

        public string Value { get; set; } = string.Empty;

        public string? NullableValue { get; set; }

        public TestEnum EnumValue { get; set; }

        public TestEnum? NullableEnumValue { get; set; }

        public DateTime DateTimeValue { get; set; }

        public DateTime? NullableDateTimeValue { get; set; }
    }

    private enum TestEnum
    {
        Value1,
        Value2
    }
}