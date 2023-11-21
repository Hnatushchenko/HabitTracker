using FluentValidation;

namespace Application.Habits.Create;

public sealed class CreateHabitCommandValidator : AbstractValidator<CreateHabitCommand>
{
    public CreateHabitCommandValidator()
    {
        RuleFor(createHabitCommand => createHabitCommand.FrequencyCount).GreaterThan(0);
        RuleFor(createHabitCommand => createHabitCommand.DayOfWeekFrequency).IsInEnum();
        RuleFor(createHabitCommand => createHabitCommand.TimeUnit).IsInEnum();
        RuleFor(createHabitCommand => createHabitCommand.DefaultStartTime)
            .LessThanOrEqualTo(createHabitCommand => createHabitCommand.DefaultEndTime);
    }
}