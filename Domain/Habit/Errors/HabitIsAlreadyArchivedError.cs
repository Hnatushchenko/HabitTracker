using Domain.Habit.ValueObjects;

namespace Domain.Habit.Errors;

public class HabitIsAlreadyArchivedError
{
    public HabitIsAlreadyArchivedError(HabitId habitId)
    {
        Message = $"The habit with ID = {habitId.Value} is currently archived and therefore it cannot be archived again.";
    }
    
    public string Message { get; }
}
