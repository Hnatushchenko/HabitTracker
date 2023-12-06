using System.Reflection;
using Domain.Habit;
using Domain.Habit.Enums;
using Domain.Habit.ValueObjects;
using Xunit.Sdk;

namespace Application.UnitTests.Habits.Calculations;

public class HabitOccurrencesCalculatorTestsDataAttribute : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        var utcNow = DateTimeOffset.UtcNow;
        var yesterday = utcNow.AddDays(-1);
        yield return new object[]
        {
            CreateDefaultHabitWithStartDate(utcNow),
            utcNow,
            true
        };
        yield return new object[]
        {
            CreateDefaultHabitWithStartDate(utcNow),
            yesterday,
            false
        };
    }

    private static Habit CreateDefaultHabitWithStartDate(DateTimeOffset startDate)
    {
        return new Habit
        {
            Description = "Default Description",
            Id = new HabitId(Guid.NewGuid()),
            FrequencyCount = FrequencyCount.From(1),
            FrequencyTimeUnit = TimeUnit.Day,
            StartDate = startDate,
            ToDoItemDescription = "Default ToDo",
            DefaultStartTime = default,
            DefaultEndTime = default
        };
    }
}
