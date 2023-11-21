using FluentValidation;

namespace Application.Habits.Update;

public sealed class UpdateHabitDetailsCommandValidator : AbstractValidator<UpdateHabitDetailsCommand>
{
    public UpdateHabitDetailsCommandValidator()
    {
        RuleFor(updateHabitDetailsCommand => updateHabitDetailsCommand.HabitId)
            .NotEmpty();
        RuleFor(updateHabit => updateHabit.DefaultStartTime)
            .LessThanOrEqualTo(updateHabit => updateHabit.DefaultEndTime);
    }
}