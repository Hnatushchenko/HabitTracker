using Application.Habits.Calculations;
using Domain.Habit;
using FluentAssertions;

namespace Application.UnitTests.Habits.Calculations;

public class DateTimeOffsetIncrementerTests
{
    private readonly DateTimeOffsetIncrementer _sut = new();

    [Theory]
    [InlineData("2023-01-01", TimeUnit.Day, 1, "2023-01-02")]
    [InlineData("2023-01-01", TimeUnit.Day, 31, "2023-02-01")]
    [InlineData("2023-01-01", TimeUnit.Day, -1, "2022-12-31")]
    [InlineData("2023-01-01", TimeUnit.Week, 1, "2023-01-08")]
    [InlineData("2023-01-01", TimeUnit.Month, 1, "2023-02-01")]
    [InlineData("2023-01-01", TimeUnit.Year, 1, "2024-01-01")]
    public void Increment_ShouldAddCorrectNumberOfUnits_WhenTimeUnitIsValid(
        string initialDateString, TimeUnit timeUnit, int numberOfUnits, string expectedDateString)
    {
        // Arrange
        var initialDate = DateTimeOffset.Parse(initialDateString);
        var expectedDate = DateTimeOffset.Parse(expectedDateString);

        // Act
        var result = _sut.Increment(initialDate, timeUnit, numberOfUnits);

        // Assert
        result.Should().Be(expectedDate);
    }
    
    [Fact]
    public void Increment_ShouldThrowArgumentOutOfRangeException_WhenTimeUnitIsInvalid()
    {
        // Arrange
        var initialDate = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero);
        const TimeUnit invalidTimeUnit = (TimeUnit)(-1);

        // Act
        Action act = () => _sut.Increment(initialDate, invalidTimeUnit, 1);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}