using OneOf;
using OneOf.Types;

namespace Application.BadHabits.AddOccurrence;

public sealed class DuplicateBadHabitOccurrenceError
{
    private DuplicateBadHabitOccurrenceError() {}

    public static DuplicateBadHabitOccurrenceError Instance { get; } = new();
}

[GenerateOneOf]
public sealed partial class SuccessOrDuplicateBadHabitOccurrenceError : OneOfBase<Success, DuplicateBadHabitOccurrenceError>
{
}

