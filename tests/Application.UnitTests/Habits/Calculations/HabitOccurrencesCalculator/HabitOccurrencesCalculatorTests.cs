using Application.Frequency.DayOfWeekToDayOfWeekFrequencyMapper;
using Application.Habits.Calculations;
using Application.Habits.Calculations.HabitOccurrencesCalculator;
using Domain.Habit;
using Domain.Habit.Enums;
using Domain.Habit.ValueObjects;
using FluentAssertions;

namespace Application.UnitTests.Habits.Calculations;

public class HabitOccurrencesCalculatorTests
{
    private readonly HabitOccurrencesCalculator _sut;

    public HabitOccurrencesCalculatorTests()
    {
        var mapper = new DayOfWeekToDayOfWeekFrequencyMapper();
        var dateTimeOffsetIncrementer = new DateTimeOffsetIncrementer();
        _sut = new HabitOccurrencesCalculator(mapper, dateTimeOffsetIncrementer);
    }
    
    [Theory]
    [HabitOccurrencesCalculatorTestsData]
    public void ShouldHabitOccurOnSpecifiedDate_GivenHabitAndTargetDate_ShouldReturnExpectedResult(Habit habit, DateTimeOffset targetDate, bool expectedResult)
    {
        var result = _sut.ShouldHabitOccurOnSpecifiedDate(habit, targetDate);
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void ShouldHabitOccurOnSpecifiedDate_WithDayOfWeekFrequency_ReturnsTrueOnMatchingDays()
    {
        // Arrange
        var sunday = GetSundayDateTimeOffset();
        var monday = sunday.AddDays(1);
        var mondays = new[] { monday, monday.AddDays(7), monday.AddDays(14) };
        var habit = new Habit
        {
            Description = "Default Description",
            Id = new HabitId(Guid.NewGuid()),
            FrequencyCount = FrequencyCount.From(1),
            FrequencyTimeUnit = TimeUnit.Year,
            StartDate = sunday,
            ToDoItemDescription = "Default ToDo",
            DefaultStartTime = default,
            DefaultEndTime = default,
            DayOfWeekFrequency = DayOfWeekFrequency.OnMondays
        };
        foreach (var mondayDate in mondays)
        {
            var result = _sut.ShouldHabitOccurOnSpecifiedDate(habit, mondayDate);
            result.Should().BeTrue();
        }
    }
    
    private static DateTimeOffset GetSundayDateTimeOffset()
    {
        return new DateTimeOffset(2023, 12, 3, 0, 0, 0, TimeSpan.Zero);
    }
}
