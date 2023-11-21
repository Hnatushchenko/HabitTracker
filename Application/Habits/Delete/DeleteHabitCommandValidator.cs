using FluentValidation;

namespace Application.Habits.Delete;

public sealed class DeleteHabitCommandValidator : AbstractValidator<DeleteHabitCommand>
{
    public DeleteHabitCommandValidator()
    {
        RuleFor(deleteHabitCommand => deleteHabitCommand.HabitId.Value).NotEmpty();
    }
}