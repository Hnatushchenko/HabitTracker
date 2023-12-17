using Domain.Habit.ValueObjects;

namespace Domain.Habit.Errors;

public sealed class HabitIsNotArchivedError
{
    public HabitIsNotArchivedError(HabitId habitId)
    {
        Message = $"The habit with ID = {habitId.Value} is not currently archived and therefore it cannot be unarchived.";
    }
    
    public string Message { get; }
}
